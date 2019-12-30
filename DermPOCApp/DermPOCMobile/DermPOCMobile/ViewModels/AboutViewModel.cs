﻿
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
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
            Title = "Dermatology";
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
                await PickSkinPictureAsync(CropImageIfNeedsAsync);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await Task.CompletedTask;
        }

        private static async Task PickSkinPictureAsync(Func<MediaFile, Task> picturePicked)
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
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await Task.CompletedTask;
        }

    }
}