using Android.Content;
using Mathster;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]

namespace Mathster
{
    // Without this terribleness there will be an ugly line under every entry 
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context) { }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control?.SetBackgroundColor(Color.Transparent);
        }
    }
}