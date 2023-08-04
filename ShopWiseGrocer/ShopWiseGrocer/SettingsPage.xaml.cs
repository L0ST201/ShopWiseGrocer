using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            // Set the BindingContext of the page to the current instance of the SettingsPage class
            this.BindingContext = this;
        }

        // This method is triggered when the switch is toggled
        public void OnToggled(object sender, ToggledEventArgs e)
        {
            IsDarkTheme = e.Value;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OnPropertyChanged(nameof(ThemeModeText)); // Notify that the ThemeModeText has changed
        }
    }
}
