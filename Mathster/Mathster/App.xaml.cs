using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Mathster
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;
        public App()
        {
            InitializeComponent();
            MainPage = new MasterMenu();
        }

        public App(string databaseLocation)
        {
            InitializeComponent();
            MainPage = new MasterMenu();

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