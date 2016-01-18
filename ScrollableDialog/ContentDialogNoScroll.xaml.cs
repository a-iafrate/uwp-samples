using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento per la finestra di dialogo del contenuto è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkId=234238

namespace ScrollableDialog
{
    public sealed partial class ContentDialogNoScroll : ContentDialog
    {
        public ContentDialogNoScroll()
        {
            this.InitializeComponent();

            String[] listElements = new String[100]; 
            for(int i = 0; i < 100; i++)
            {
                listElements[i] = i.ToString();
            }
            list.ItemsSource = listElements;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
