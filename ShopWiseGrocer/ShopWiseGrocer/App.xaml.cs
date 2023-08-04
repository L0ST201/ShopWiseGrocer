using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopWiseGrocer
{
    public partial class App : Application
    {
        private bool isDarkTheme = false;

        public event Action ThemeChanged;

        public bool IsDarkTheme
        {
            get => isDarkTheme;
            set
            {
                if (isDarkTheme != value)
                {
                    isDarkTheme = value;
                    ApplyTheme();
                    ThemeChanged?.Invoke();
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
                Current.Resources["Primary"] = Current.Resources["DarkPrimary"];
                Current.Resources["Accent"] = Current.Resources["DarkAccent"];
                Current.Resources["TextColor"] = Current.Resources["DarkTextColor"];
                Current.Resources["FrameBackgroundColor"] = Current.Resources["DarkFrameBackgroundColor"];
                Current.Resources["AppBackground"] = Current.Resources["DarkAppBackground"];
            }
            else
            {
                Current.Resources["Primary"] = Current.Resources["LightPrimary"];
                Current.Resources["Accent"] = Current.Resources["LightAccent"];
                Current.Resources["TextColor"] = Current.Resources["LightTextColor"];
                Current.Resources["FrameBackgroundColor"] = Current.Resources["LightFrameBackgroundColor"];
                Current.Resources["AppBackground"] = Current.Resources["LightAppBackground"];
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
