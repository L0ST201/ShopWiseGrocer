using Android.Content;
using Android.Graphics;
using ShopWiseGrocer.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace ShopWiseGrocer.Droid
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var xamarinFormsColor = (Xamarin.Forms.Color)Application.Current.Resources["Primary"];
                var androidColor = xamarinFormsColor.ToAndroid();
                Control.Background.Mutate().SetColorFilter(new BlendModeColorFilter(androidColor, BlendMode.SrcAtop));
                Control.SetHintTextColor(androidColor);
                Control.SetTextColor(androidColor);
            }
        }
    }
}

