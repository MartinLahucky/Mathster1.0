using Android.Widget;
using Mathster;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;

[assembly: Dependency(typeof(NativeFun))]

namespace Mathster
{
    public class NativeFun : INativeFun
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(AndroidApp.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(AndroidApp.Context, message, ToastLength.Short).Show();
        }

    }
}