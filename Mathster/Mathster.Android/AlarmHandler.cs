using Android.Content;
using Android.OS;

namespace Mathster.Android
{
    [BroadcastReceiver(Enabled = true, Label = "Local Notifications Broadcast Receiver")]
    public class AlarmHandler : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent?.Extras != null)
            {
                // Wake up when not in app 
                var pm = PowerManager.FromContext(context);
                var wakeLock = pm?.NewWakeLock(WakeLockFlags.Partial, "GCM Broadcast Reciever Tag");
                wakeLock?.Acquire();
                // Notification process
                var title = intent.GetStringExtra(AndroidNotificationManager.TitleKey);
                var message = intent.GetStringExtra(AndroidNotificationManager.MessageKey);
                AndroidNotificationManager.Instance.Show(title, message);
                // Release the device 
                wakeLock?.Release();
            }
        }
    }
}