using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopWiseGrocer
{
    public partial class App : Application
    {
        private bool isDarkTheme = false;

        private Color PrimaryLightColor;
        private Color AccentLightColor;
        private Color TextColorLightColor;
        private Color FrameBackgroundColorLightColor;
        private Color AppBackgroundLightColor;

        private Color PrimaryDarkColor;
        private Color AccentDarkColor;
        private Color TextColorDarkColor;
        private Color FrameBackgroundColorDarkColor;
        private Color AppBackgroundDarkColor;

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

            // Extract light theme colors
            PrimaryLightColor = (Color)Current.Resources["Primary"];
            AccentLightColor = (Color)Current.Resources["Accent"];
            TextColorLightColor = (Color)Current.Resources["TextColor"];
            FrameBackgroundColorLightColor = (Color)Current.Resources["FrameBackgroundColor"];
            AppBackgroundLightColor = (Color)Current.Resources["AppBackground"];

            // Extract dark theme colors
            PrimaryDarkColor = (Color)Current.Resources["DarkPrimary"];
            AccentDarkColor = (Color)Current.Resources["DarkAccent"];
            TextColorDarkColor = (Color)Current.Resources["DarkTextColor"];
            FrameBackgroundColorDarkColor = (Color)Current.Resources["DarkFrameBackgroundColor"];
            AppBackgroundDarkColor = (Color)Current.Resources["DarkAppBackground"];

            MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());
        }

        private void ApplyTheme()
        {
            if (IsDarkTheme)
            {
                Current.Resources["Primary"] = PrimaryDarkColor;
                Current.Resources["Accent"] = AccentDarkColor;
                Current.Resources["TextColor"] = TextColorDarkColor;
                Current.Resources["FrameBackgroundColor"] = FrameBackgroundColorDarkColor;
                Current.Resources["AppBackground"] = AppBackgroundDarkColor;
            }
            else
            {
                Current.Resources["Primary"] = PrimaryLightColor;
                Current.Resources["Accent"] = AccentLightColor;
                Current.Resources["TextColor"] = TextColorLightColor;
                Current.Resources["FrameBackgroundColor"] = FrameBackgroundColorLightColor;
                Current.Resources["AppBackground"] = AppBackgroundLightColor;
            }
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
