using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace FileDetails
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void selectFile_Click(object sender, RoutedEventArgs e)
        {
            // Prepare file picker
            FileOpenPicker fop = new FileOpenPicker();
            fop.FileTypeFilter.Add(".png");
            fop.FileTypeFilter.Add(".jpeg");
            fop.FileTypeFilter.Add(".jpg");
            fop.FileTypeFilter.Add(".bmp");
            fop.FileTypeFilter.Add(".txt");
            fop.FileTypeFilter.Add(".docx");
            fop.FileTypeFilter.Add(".doc");
            fop.FileTypeFilter.Add(".xlsx");
            fop.FileTypeFilter.Add(".xls");
            fop.FileTypeFilter.Add(".mp3");
            fop.FileTypeFilter.Add(".divx");
            fop.FileTypeFilter.Add(".avi");
            // Open file picker
            StorageFile sf=await fop.PickSingleFileAsync();
            // if file is selected
            if (sf != null)
            {
                // Read common details
                String contentType = sf.ContentType;
                detailsText.Text = "Content type: " + sf.ContentType+"\r\n";
                detailsText.Text += "Common details: \r\n";
                BasicProperties bp = await sf.GetBasicPropertiesAsync();
                detailsText.Text+= "Size: "+bp.Size+"\r\n";
                detailsText.Text += "Date: "+bp.ItemDate.ToString()+"\r\n";
                detailsText.Text += "Modified date: " + bp.DateModified.ToString() + "\r\n";
                //Take preview
                StorageItemThumbnail thumb = await sf.GetThumbnailAsync(ThumbnailMode.PicturesView);
                BitmapImage thumbnailImage = new BitmapImage();
                thumbnailImage.SetSource(thumb);
                imagePreview.Source = thumbnailImage;
                
                // if audio type get details
                if (contentType.StartsWith("audio"))
                {
                    MusicProperties mp = await sf.Properties.GetMusicPropertiesAsync();
                    detailsText.Text += "\r\nMusic details: \r\n";
                    detailsText.Text += "Album: " + mp.Album + "\r\n";
                    detailsText.Text += "Title: " + mp.Title + "\r\n";
                    detailsText.Text += "Album artist: " + mp.AlbumArtist + "\r\n";
                    detailsText.Text += "Duration: " + mp.Duration + "\r\n";
                }
                // if image type get details
                if (contentType.StartsWith("image"))
                {
                    ImageProperties ip = await sf.Properties.GetImagePropertiesAsync();
                    detailsText.Text += "\r\nImage details: \r\n";
                    detailsText.Text += "Width: " + ip.Width + "\r\n";
                    detailsText.Text += "Height: " + ip.Height + "\r\n";
                    detailsText.Text += "Latitude: " + ip.Latitude + "\r\n";
                    detailsText.Text += "Longitude: " + ip.Longitude + "\r\n";
                    detailsText.Text += "CameraModel: " + ip.CameraModel + "\r\n";
                    //ip.CameraModel = "Mia camera";
                    //await ip.SavePropertiesAsync();
                    detailsText.Text += "CameraManufacturer: " + ip.CameraManufacturer + "\r\n";

                    //get not common details
                    // more info https://msdn.microsoft.com/en-us/library/windows/desktop/dd561977(v=vs.85).aspx
                    IDictionary<string, object> ret = await ip.RetrievePropertiesAsync(new string[] { "System.Image.BitDepth" });
                    if (ret.Count > 0)
                    {
                        detailsText.Text += "System.Image.BitDepth: " + ret.ElementAt(0).Value + "\r\n";
                    }
                    
                }
                // if document type get details
                if (contentType.StartsWith("application/vnd.openxmlformats-officedocument"))
                {
                    DocumentProperties dp = await sf.Properties.GetDocumentPropertiesAsync();
                    detailsText.Text += "\r\nDocument details: \r\n";
                    String autors = "";
                    foreach(String a in dp.Author.ToList())
                    {
                        autors += a + ",";
                    }
                    detailsText.Text += "Author: " + autors + "\r\n";
                    detailsText.Text += "Comment: " + dp.Comment + "\r\n";
                    detailsText.Text += "Keywords: " + dp.Keywords + "\r\n";
                    detailsText.Text += "Title: " + dp.Title + "\r\n";
                    
                }
                // if video type get details
                if (contentType.StartsWith("video/"))
                {
                    VideoProperties dp = await sf.Properties.GetVideoPropertiesAsync();
                    detailsText.Text += "\r\nVideo details: \r\n";
                    
                    detailsText.Text += "Height: " + dp.Height + "\r\n";
                    detailsText.Text += "Width: " + dp.Width + "\r\n";
                    detailsText.Text += "Bitrate: " + dp.Bitrate + "\r\n";
                    detailsText.Text += "Duration: " + dp.Duration + "\r\n";
                    detailsText.Text += "Title: " + dp.Title + "\r\n";
                    detailsText.Text += "Subtitle: " + dp.Subtitle + "\r\n";
                    
                    
                }

            }
           
        }
    }
}
