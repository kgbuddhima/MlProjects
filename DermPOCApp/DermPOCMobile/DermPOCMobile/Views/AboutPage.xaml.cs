using DermPOCMobile.ViewModels;
using FFImageLoading.Forms;
using Stormlion.ImageCropper;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
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
                                ((AboutViewModel)BindingContext).ImagePath = imageFile;                                
                                ((AboutViewModel)BindingContext).PredictUsingApiCommand.Execute(null);
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