using Plugin.Media;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using DermPOCAppML.Model;

namespace DermPOCMobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        ImageSource _dermImage;
        public ImageSource DermImage
        {
            get => _dermImage;
            set
            {
                _dermImage = value;
                OnPropertyChanged(nameof(DermImage));
            }
        }

        string imagePath = string.Empty;

        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
            UploadImageCommand = new Command(async()=>await UploadImage());
            PredictCommand = new Command(async () => await PredictAsync());
        }

        public ICommand OpenWebCommand { get; }
        public ICommand UploadImageCommand { get; }
        public ICommand PredictCommand { get; }

        private async Task UploadImage()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    //DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file == null)
                    return;

                imagePath = file.Path;

                //await DisplayAlert("File Location", file.Path, "OK");

                DermImage = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await Task.CompletedTask;
        }

        private async Task PredictAsync() 
        {
            try
            {
                // Add input data
                var input = new ModelInput();
                input.ImageSource = imagePath;

                // Load model and predict output of sample data
                ModelOutput result = ConsumeModel.Predict(input);
                if (result != null)
                { 
                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await Task.CompletedTask;
        }

    }
}