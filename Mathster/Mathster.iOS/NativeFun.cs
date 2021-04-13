using Foundation;
using Mathster.iOS;
using Mathster.Resources.Helpers;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(NativeFun))]

namespace Mathster.iOS
{
    public class NativeFun : INativeFun
    {
        private const double LongDelay = 3.5;
        private const double ShortDelay = 2.0;
        private UIAlertController alert;

        private NSTimer alertDelay;

        public void LongAlert(string message)
        {
            ShowAlert(message, LongDelay);
        }

        public void ShortAlert(string message)
        {
            ShowAlert(message, ShortDelay);
        }

        private void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, obj => { DismissMessage(); });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            if (UIApplication.SharedApplication.KeyWindow.RootViewController != null)
                UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        private void DismissMessage()
        {
            alert?.DismissViewController(true, null);
            alertDelay?.Dispose();
        }
    }
}