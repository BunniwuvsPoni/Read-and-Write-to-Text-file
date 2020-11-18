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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Read_and_Write_to_Text_file
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Create_text_file_Click(object sender, RoutedEventArgs e)
        {
            // Create sample file; replace if exists.
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                await storageFolder.CreateFileAsync("sample.txt",
                    Windows.Storage.CreationCollisionOption.ReplaceExisting);

            // Test sample file
            Access_sample_file();
        }

        private async void Write_to_text_file_Click(object sender, RoutedEventArgs e)
        {
            // Get the sample file
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                await storageFolder.GetFileAsync("sample.txt");

            // Write text to the sample file
            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, "Swift as a shadow");

            // Test sample file
            Access_sample_file();
        }

        private async void Access_sample_file()
        {
            // Get sample file location
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;

            // Launches the folder for easier testing
            await Windows.System.Launcher.LaunchFolderAsync(storageFolder);
        }
    }
}
