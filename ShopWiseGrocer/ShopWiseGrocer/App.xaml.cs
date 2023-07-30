using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopWiseGrocer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Place to perform initial setup that needs to be performed each time the app starts.
        }

        protected override void OnSleep()
        {
            // Place to perform tasks when the app is going into the background.
        }

        protected override void OnResume()
        {
            // Place to perform tasks when the app is resuming from the background.
        }
    }
}
