using System.IO;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Environment = System.Environment;

namespace Mathster.Android
{
    [Activity(Label = "Mathster", Icon = "@drawable/ikonaKruh",Theme = "@style/MainTheme.Splash", MainLauncher = true,
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
            string dbName = "mathster_db.sqlite";
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string fullPath = Path.Combine(folderPath, dbName);
            
            
            LoadApplication(new App(fullPath));
        }
    }
}