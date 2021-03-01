using Android.Content;
using Mathster.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(RadioButton), typeof(CustomRadioButtonRenderer))]

namespace Mathster.Android
{
    public class CustomRadioButtonRenderer : RadioButtonRenderer
    {
        public CustomRadioButtonRenderer(Context context) : base(context)
        {
        }

        // TODO Repair Render
        protected override void OnElementChanged(ElementChangedEventArgs<RadioButton> e)
        {
            base.OnElementChanged(e);
            Control?.SetBackgroundColor(Color.White);
        }
    }
}