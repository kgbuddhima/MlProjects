using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DermPOCMobile.Services;
using DermPOCMobile.Views;
using Plugin.Media;

namespace DermPOCMobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            CrossMedia.Current.Initialize();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
