
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using DermPOCAppML.Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;

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

        string _predictedDisease;
        public string PredictedDisease
        {
            get => _predictedDisease;
            set
            {
                _predictedDisease = value;
                OnPropertyChanged(nameof(PredictedDisease));
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
                await PickProfilePictureAsync(CropImageIfNeedsAsync);

               /* if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    //DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                { 
                    Directory = "Sample",
                    Name = $"test.jpg"
                });

                if (file == null)
                    return;

                

                //await DisplayAlert("File Location", file.Path, "OK");

                DermImage = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await Task.CompletedTask;
        }

        private static async Task PickProfilePictureAsync(Func<MediaFile, Task> picturePicked)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }

            try
            {
                using (MediaFile photo = await CrossMedia.Current.PickPhotoAsync())
                {
                    await picturePicked(photo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task CropImageIfNeedsAsync(MediaFile photo)
        {
            if (photo == null)
            {
                return;
            }

            DermImage =  ImageSource.FromStream(photo.GetStream);
            imagePath = Path.GetFileName(photo.Path);

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
                    PredictedDisease =  result.Prediction;
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