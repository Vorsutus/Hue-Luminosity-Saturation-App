using System;
using Xamarin.Forms;
using HLSView.View;
using Xamarin.Forms.Xaml;

namespace HLSView
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new HlsView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
