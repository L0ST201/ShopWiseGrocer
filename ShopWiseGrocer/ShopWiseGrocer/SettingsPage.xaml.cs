using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace ShopWiseGrocer
{
    public partial class SettingsPage : ContentPage, INotifyPropertyChanged
    {
        public bool IsDarkTheme
        {
            get => ((App)Application.Current).IsDarkTheme;
            set
            {
                if (((App)Application.Current).IsDarkTheme != value)
                {
                    ((App)Application.Current).IsDarkTheme = value;
                    OnPropertyChanged(nameof(IsDarkTheme));
                    OnPropertyChanged(nameof(ThemeModeText));
                }
            }
        }

        public string ThemeModeText => IsDarkTheme ? "Dark Mode" : "Light Mode";

        public SettingsPage()
        {
            InitializeComponent();
            this.BindingContext = this;

            LanguagePicker.ItemsSource = new List<string>
            {
                "English",
                "Spanish",
                "French",
            };

            string currentLanguage = Preferences.Get("AppLanguage", "English");
            LanguagePicker.SelectedIndex = LanguagePicker.ItemsSource.IndexOf(currentLanguage);

            string fontSizeStr = Preferences.Get("AppFontSize", "16");
            double currentFontSize;

            if (!double.TryParse(fontSizeStr, out currentFontSize))
            {
                currentFontSize = 16;
            }

            FontSizeSlider.Value = currentFontSize;
        }
        public void OnToggled(object sender, ToggledEventArgs e)
        {
            IsDarkTheme = e.Value;
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                string selectedLanguage = (string)picker.ItemsSource[selectedIndex];

                HandleLanguageChange(selectedLanguage);
            }
        }

        void HandleLanguageChange(string language)
        {
            Preferences.Set("AppLanguage", language);
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            Preferences.Set("AppFontSize", e.NewValue.ToString());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OnPropertyChanged(nameof(ThemeModeText));
        }
    }
}
