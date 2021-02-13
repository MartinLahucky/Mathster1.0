using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;
using Java.Lang;
using Mathster.Android;
using Mathster.Resources.Helpers.Notifications;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;
using String = Java.Lang.String;

[assembly: Dependency(typeof(AndroidNotificationManager))]

namespace Mathster.Android
{
    [Service]
    public class AndroidNotificationManager : Service, INotificationManager
    {
        private const string ChannelId = "default";
        private const string ChannelName = "Default";
        private const string ChannelDescription = "The default channel for notifications.";

        public const string TitleKey = "title";
        public const string MessageKey = "message";

        private bool channelInitialized;
        private int notificationId;
        private int pendingIntentId;

        NotificationManager manager;

        public event EventHandler NotificationReceived;

        public static AndroidNotificationManager Instance { get; private set; }

        private Intent Intent { get; set; }

        public override IBinder OnBind(Intent intent)
        {
            Intent = intent;
            return null;
        }

        public void StartService(string title, string message, DateTime? notifyTime = null, long? repeatTime = null)
        {
            var intent = new Intent(AndroidApp.Context, typeof(AndroidNotificationManager));
            SendNotification(title, message, notifyTime, repeatTime);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                AndroidApp.Context.StartForegroundService(intent);
            }
            else
            {
                AndroidApp.Context.StartService(intent);
            }
        }
        
        public void SendNotification(string title, string message, DateTime? notifyTime = null, long? repeatTime = null)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            if (notifyTime != null)
            {
                Intent intent = new Intent(AndroidApp.Context, typeof(AlarmHandler));
                intent.PutExtra(TitleKey, title);
                intent.PutExtra(MessageKey, message);
                Instance = this; // Without this notifications with delay won't work 
                long triggerTime = GetNotifyTime(notifyTime.Value);
                AlarmManager alarmManager = AndroidApp.Context.GetSystemService(AlarmService) as AlarmManager;
                if (repeatTime != null)
                {
                    if (triggerTime < JavaSystem.CurrentTimeMillis())
                    {
                        triggerTime += repeatTime.Value;
                    }

                    PendingIntent pendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, pendingIntentId++,
                        intent, PendingIntentFlags.Immutable);
                    alarmManager?.SetRepeating(AlarmType.RtcWakeup, triggerTime, repeatTime.Value, pendingIntent);
                }
                else
                {
                    PendingIntent pendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, pendingIntentId++,
                        intent, PendingIntentFlags.CancelCurrent);
                    alarmManager?.Set(AlarmType.RtcWakeup, triggerTime, pendingIntent);
                }
            }
            else
            {
                Show(title, message);
            }
        }

        public void ReceiveNotification(string title, string message)
        {
            var args = new NotificationEventArgs
            {
                Title = title,
                Message = message,
            };
            NotificationReceived?.Invoke(null, args);
        }

        public void Show(string title, string message)
        {
            Intent intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, message);
            // Starts up the activity (app) 
            PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, pendingIntentId++, intent,
                PendingIntentFlags.UpdateCurrent);
            // Building the notifictaion
            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, ChannelId)
                .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                .SetContentIntent(pendingIntent) // Start up this activity when the user clicks the intent
                .SetContentTitle(title) // Set the title
                .SetContentText(message) // The message to display
                .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources,
                    Resource.Drawable.icon)) //Big icon  
                .SetSmallIcon(Resource.Drawable.icon) // This is the icon to display
                .SetDefaults((int) NotificationDefaults.Sound | // This sets sound and
                             (int) NotificationDefaults.Vibrate); // vibrations to what phones uses right now (default) 

            Notification notification = builder.Build();
            manager.Notify(notificationId++, notification);
        }

        private void CreateNotificationChannel()
        {
            manager = (NotificationManager) AndroidApp.Context.GetSystemService(NotificationService);

            // Notification channels are new in API 26 (and not a part of the support library)
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new String(ChannelName);
                var channel = new NotificationChannel(ChannelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = ChannelDescription
                };
                manager?.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }

        private long GetNotifyTime(DateTime notifyTime)
        {
            DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
            double epochDiff = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
            long utcAlarmTime = utcTime.AddSeconds(-epochDiff).Ticks / 10000;
            return utcAlarmTime; // milliseconds
        }
    }
}