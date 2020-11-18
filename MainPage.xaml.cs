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

        private async void Write_to_text_file_buffer_Click(object sender, RoutedEventArgs e)
        {
            // Get the sample file
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                await storageFolder.GetFileAsync("sample.txt");

            // Setup the buffer
            var buffer = Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(
                "What fools these mortals be", Windows.Security.Cryptography.BinaryStringEncoding.Utf8);

            // Write to the file
            await Windows.Storage.FileIO.WriteBufferAsync(sampleFile, buffer);

            // Test sample file
            Access_sample_file();
        }

        private async void Write_to_text_file_stream_Click(object sender, RoutedEventArgs e)
        {
            // Get the sample file
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                await storageFolder.GetFileAsync("sample.txt");

            // Open the file
            var stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);

            // Get an output stream
            using (var outputStream = stream.GetOutputStreamAt(0))
            {
                // Write to the output stream
                using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                {
                    dataWriter.WriteString("DataWriter has methods to write to various types, such as DataTimeOffset.");

                    // Save the stream
                    await dataWriter.StoreAsync();

                    // Close the file
                    await outputStream.FlushAsync();
                }
            }
            stream.Dispose(); // Or use the stream variable (see previous code snippet) with a using statement as well.

            // Test sample file
            Access_sample_file();
        }

        private async void Read_text_file_Click(object sender, RoutedEventArgs e)
        {
            // Get the sample file
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                await storageFolder.GetFileAsync("sample.txt");

            // Read sample file
            string text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
        }
    }
}
