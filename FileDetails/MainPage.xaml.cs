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
            StorageFile sf=await fop.PickSingleFileAsync();
            StorageItemThumbnail thumb=await sf.GetThumbnailAsync(ThumbnailMode.SingleItem);
           BasicProperties bp=await sf.GetBasicPropertiesAsync();
           //VideoProperties vp=await sf.Properties.GetVideoPropertiesAsync();
            DocumentProperties dp = await sf.Properties.GetDocumentPropertiesAsync();
           IDictionary<string,object> ret=await dp.RetrievePropertiesAsync(new string[] { "System.Document.LastAuthor" });
            Debug.WriteLine(ret.Count + "");
            MusicProperties mp = await sf.Properties.GetMusicPropertiesAsync();
            detailsText.Text = sf.ContentType;
           // if(t isinstanceof DocumentProperties)
            {

            }
        }
    }
}
