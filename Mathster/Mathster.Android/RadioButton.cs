using Android.Content;
using Android.Content.Res;
using Mathster;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(RadioButton), typeof(CustomRadioButtonRenderer))]

namespace Mathster
{
    public class CustomRadioButtonRenderer : RadioButtonRenderer
    {
        public CustomRadioButtonRenderer(Context context) : base(context) { }
        // TODO Repair Render
        protected override void OnElementChanged(ElementChangedEventArgs<RadioButton> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.ButtonTintList = ColorStateList.ValueOf(Color.White);
            }
        }
    }
}