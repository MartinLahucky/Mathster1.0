using Android.App;
using Mathster.Android;
using Mathster.Resources.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(Utilities))]
namespace Mathster.Android
{
    public class Utilities : IUtilities
    {
        public void CloseApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}