using System.IO;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Mathster.Resources.Helpers.Notifications;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Environment = System.Environment;

namespace Mathster.Android
{
    [Activity(Label = "Mathster", Icon = "@drawable/icon", Theme = "@style/MainTheme.Splash", MainLauncher = true,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTop
    )]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.SetTheme(Resource.Style.MainTheme);

            base.OnCreate(savedInstanceState);
            Forms.Init(this, savedInstanceState);

            // Database Declaration
            string fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "mathster_db.sqlite");
            LoadApplication(new App(fullPath));
            CreateNotificationFromIntent(Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            CreateNotificationFromIntent(intent);
        }

        private static void CreateNotificationFromIntent(Intent intent)
        {
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra(AndroidNotificationManager.TitleKey);
                string message = intent.GetStringExtra(AndroidNotificationManager.MessageKey);
                DependencyService.Get<INotificationManager>().ReceiveNotification(title, message);
            }
        }
    }
}