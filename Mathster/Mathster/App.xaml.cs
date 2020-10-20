using Mathster.Helpers.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Mathster
{
    public partial class App : Application
    {
        private static DatabaseController database;
        public static string DatabaseLocation = string.Empty;

        public static DatabaseController Database
        {
            get
            {
                if (database == null)
                {
                    database = new DatabaseController();
                }
                return database;
            }
        }
        
        public App(string databaseLocation)
        {
            InitializeComponent();
            MainPage = new NavigationPage(new RozborVysledku())
            {
                BarTextColor = Color.FromHex("#C9FF50"),
            };
            DatabaseLocation = databaseLocation;
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
        protected override void OnResume() 
        {
            // Handle when your app resumes
        }
    }
}