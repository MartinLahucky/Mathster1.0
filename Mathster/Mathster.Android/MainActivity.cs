using System.IO;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Environment = System.Environment;

namespace Mathster.Android
{
    [Activity(Label = "Mathster", Icon = "@drawable/ikona", Theme = "@style/MainTheme.Splash", MainLauncher = true,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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
        }

        // //Double press pro exit 
        // private long lastPress;
        // public override void OnBackPressed()
        // {
        //     // source https://stackoverflow.com/a/27124904/3814729
        //     long currentTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
        //
        //     // source https://stackoverflow.com/a/14006485/3814729
        //     if (currentTime - lastPress > 5000)
        //     {
        //         Toast.MakeText(this, "Press back again to exit", ToastLength.Long).Show();
        //         lastPress = currentTime;
        //     }
        //     else
        //     {
        //         base.OnBackPressed();
        //     }
        // }
    }
}