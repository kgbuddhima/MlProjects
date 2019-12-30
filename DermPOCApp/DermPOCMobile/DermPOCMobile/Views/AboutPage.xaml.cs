using DermPOCMobile.ViewModels;
using Stormlion.ImageCropper;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DermPOCMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void btnCrop_Clicked(object sender, EventArgs e)
        {
            try
            {
                string imagepath = ((AboutViewModel)BindingContext).ImagePath;
                new ImageCropper()
                {
                    Success = (imageFile) =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            imagepath = imageFile;
                            ImageView.Source = ImageSource.FromFile(imageFile);

                            if (imagepath != null)
                            {

                            }

                        });
                    }
                }.Show(this);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}