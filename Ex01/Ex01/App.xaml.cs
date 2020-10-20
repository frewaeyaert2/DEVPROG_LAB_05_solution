using Ex01.Views;
using Plugin.Connectivity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ex01
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage());

            if (CrossConnectivity.Current.IsConnected)
                MainPage = new NavigationPage(new MainPage());
            else
                MainPage = new NoNetworkPage();
        }

        protected override void OnStart()
        {
            base.OnStart();

            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }


        private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            Type currentPage = this.MainPage.GetType();
            if (e.IsConnected && currentPage != typeof(MainPage))
                MainPage = new NavigationPage(new MainPage());
            else if (!e.IsConnected && currentPage != typeof(NoNetworkPage))
                this.MainPage = new NoNetworkPage();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
