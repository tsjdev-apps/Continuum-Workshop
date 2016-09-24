using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.Media.Casting;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VideoPlayer.Universal
{
    public sealed partial class MainPage : Page
    {
        private CastingDevicePicker _devicePicker;

        public MainPage()
        {
            this.InitializeComponent();

            Loaded += MainPageOnLoaded;
        }

        private void MainPageOnLoaded(object sender, RoutedEventArgs e)
        {
            // initialize the picker object
            _devicePicker = new CastingDevicePicker();

            // set the picker to filter to video capable casting devices
            _devicePicker.Filter.SupportsVideo = true;

            // register device selected event
            _devicePicker.CastingDeviceSelected += DevicePickerOnCastingDeviceSelected;
        }

        private async void LoadButtonOnClick(object sender, RoutedEventArgs e)
        {
            // create a new picker
            var filePicker = new FileOpenPicker();

            // add filetype filters
            filePicker.FileTypeFilter.Add(".wmv");
            filePicker.FileTypeFilter.Add(".mp4");

            // set picker start location to video library
            filePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;

            try
            {
                var file = await filePicker.PickSingleFileAsync();

                if (file == null)
                    return;

                // if we got a file, load it into the media lement
                var stream = await file.OpenAsync(FileAccessMode.Read);
                VideoElement.SetSource(stream, file.ContentType);

                // enable cast button
                CastButton.IsEnabled = true;

                // show controls on video element
                VideoElement.AreTransportControlsEnabled = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MainPage.xaml.cs | LoadButtonOnClick | " + ex);

                // disable cast button
                CastButton.IsEnabled = false;

                // hide controls on video element
                VideoElement.AreTransportControlsEnabled = false;
            }
        }

        private void CastButtonOnClick(object sender, RoutedEventArgs e)
        {
            // retrieve location of the casting button
            var transform = CastButton.TransformToVisual(Window.Current.Content);
            var point = transform.TransformPoint(new Point(0, 0));

            // show the picker aboute the casting button
            _devicePicker.Show(new Rect(point.X, point.Y, CastButton.ActualWidth, CastButton.ActualHeight), Placement.Above);
        }
        
        private async void DevicePickerOnCastingDeviceSelected(CastingDevicePicker sender, CastingDeviceSelectedEventArgs args)
        {
            //Casting must occur from the UI thread.  This dispatches the casting calls to the UI thread.
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                // create a casting connection
                var connection = args.SelectedCastingDevice.CreateCastingConnection();

                // register casting events
                connection.ErrorOccurred += ConnectionOnErrorOccurred;
                connection.StateChanged += ConnectionOnStateChanged;

                // cast the content loaded in the media element
                await connection.RequestStartCastingAsync(VideoElement.GetAsCastingSource());
            });
        }

        private void ConnectionOnStateChanged(CastingConnection sender, object args)
        {
            // TODO: Maybe inform the user about the changing of the state
            Debug.WriteLine("MainPage.xaml.cs | ConnectionOnStateChanged | " + sender.State);
        }

        private void ConnectionOnErrorOccurred(CastingConnection sender, CastingConnectionErrorOccurredEventArgs args)
        {
            // TODO: Maybe inform the user about the error
            Debug.WriteLine("MainPage.xaml.cs | ConnectionOnErrorOccurred | " + args.Message);
        }
    }
}
