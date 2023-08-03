using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopWiseGrocer
{
    public partial class App : Application
    {
        private bool isDarkTheme = false;
        public bool IsDarkTheme
        {
            get => isDarkTheme;
            set
            {
                if (isDarkTheme != value)
                {
                    isDarkTheme = value;
                    ApplyTheme();
                }
            }
        }


        public App()
        {
            InitializeComponent();
            var dummy = typeof(SQLite.SQLiteConnection);

            MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());
        }

        private void ApplyTheme()
        {
            if (IsDarkTheme)
            {
                Resources["Primary"] = Resources["DarkPrimary"];
                Resources["Accent"] = Resources["DarkAccent"];
                Resources["TextColor"] = Resources["DarkTextColor"];
                Resources["FrameBackgroundColor"] = Resources["DarkFrameBackgroundColor"];
                Resources["AppBackground"] = Resources["DarkAppBackground"];
            }
            else
            {
                Resources["Primary"] = Resources["LightPrimary"];
                Resources["Accent"] = Resources["LightAccent"];
                Resources["TextColor"] = Resources["LightTextColor"];
                Resources["FrameBackgroundColor"] = Resources["LightFrameBackgroundColor"];
                Resources["AppBackground"] = Resources["LightAppBackground"];
            }
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
