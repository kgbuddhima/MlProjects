﻿
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
using Stormlion.ImageCropper;
using DermPOCMobile.Services;
using System.Threading;
using FFImageLoading;

namespace DermPOCMobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {

        IHttpService service;

        Stream _dermImageStream;
        public Stream DermImageStream
        {
            get => _dermImageStream;
            set
            {
                _dermImageStream = value;
                OnPropertyChanged(nameof(DermImageStream));
            }
        }

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

        string imagePath;
        public string ImagePath
        {
            get => imagePath;
            set
            {
                imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        string _result;
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public AboutViewModel()
        {
            service = DependencyService.Get<IHttpService>();

            Title = "Dermatology";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
            UploadImageCommand = new Command(async()=>await UploadImage());
            PredictCommand = new Command(async () => await PredictAsync());
            CropImageCommand= new Command(async () => await CropImageAsync());
            ResizeImageCommand = new Command(async () => await ResizeImageAsync());
            PredictUsingApiCommand = new Command(async () => await PredictUsingApiAsync());
        }

        public ICommand OpenWebCommand { get; }
        public ICommand UploadImageCommand { get; }
        public ICommand PredictCommand { get; }

        public ICommand PredictUsingApiCommand { get; }

        public ICommand CropImageCommand { get; }

        public ICommand ResizeImageCommand { get; }

        private async Task UploadImage()
        {
            try
            {
                await PickSkinPictureAsync(SetImage);
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

        private async Task SetImage(MediaFile photo)
        {
            if (photo == null)
            {
                return;
            }

            _dermImageStream = photo.GetStream();
            DermImage =  ImageSource.FromFile(photo.Path);
            ImagePath = Path.GetFileName(photo.Path);

        }

        private async Task CropImageAsync()
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

        private async Task PredictUsingApiAsync()
        {
            try
            {
               await service.PredictImageAsync(_dermImageStream.ToByteArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await Task.CompletedTask;
        }

        private async Task ResizeImageAsync()
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

        private async Task PredictAsync()
        {
            try
            {
                var service = DependencyService.Get<IPhotoDetector>();
                if (service == null)
                {
                    Console.WriteLine("Info", "Not implemented the feature on your device.", "OK");
                    return;
                }

                var result = await service.DetectAsync(await ResizeImageAsync(_dermImageStream.ToByteArray()));
                Result = result;
                Console.WriteLine($"It looks like a {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Result = string.Empty;
            }
            await Task.CompletedTask;
        }

        private async Task<Stream> GetStreamFromImageSourceAsync(StreamImageSource imageSource, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (imageSource.Stream != null)
            {
                return await imageSource.Stream(cancellationToken);
            }
            return null;
        }

        private async Task<Stream> ResizeImageAsync(byte[] imageBytes, int width = 224, int height = 224)
        {
            FFImageLoading.Work.TaskParameter result = ImageService.Instance.LoadStream(token =>
            {
                TaskCompletionSource<Stream> tcs = new TaskCompletionSource<Stream>();
                tcs.TrySetResult(new MemoryStream(imageBytes));
                return tcs.Task;
            }).DownSample(width, height);

            return await result.AsJPGStreamAsync();
        }

    }
}