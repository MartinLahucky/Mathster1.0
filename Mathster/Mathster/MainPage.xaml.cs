using System;
using System.Linq;
using Mathster.Resources.Database_Models;
using Mathster.Resources.Helpers;
using Mathster.Resources.Localization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private long lastPress;
        public MainPage()
        {
            InitializeComponent();
            Title = Localization.Menu;

            MenuToolbarButton.IconImageSource = "menu_icon.png";
            SettingsButton.IconImageSource = "settings_icon.png";
            StatsToolbarButton.IconImageSource = "statistics_icon.png";
            AboutToolbarButton.IconImageSource = "info_icon.png";

            AddButton.Text = "+";
            SubButton.Text = "-";
            MulButton.Text = "×";
            DivButton.Text = "÷";
            RandomButton.Text = "?";
            EquationButton.Text = "x=";
            EquationButton.FontSize = 33;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            SettingsModel settings = await App.Database.GetSettings();
            BackgroundColor = Color.FromHex(settings.BackgroundHex);
            DBModel table = await App.Database.GetTable();
            if (string.IsNullOrEmpty(table.Name))
            {
                UserLabel.Text = "Mathster";
            }
            else
            {
                UserLabel.Text = table.Name;
            }
            table.GetLevel(out int level, out double progress, table);
            LevelButton.Text = level.ToString();
            ProgressBar.AnimatedProgress = progress;

        }
        
        private async void SelectExercise(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            switch (button.Text)
            {
                case "?":
                    await Navigation.PushAsync(new ExerciseSettings(0));
                    break;
                
                case "+":
                    await Navigation.PushAsync(new ExerciseSettings(1));
                    break;
                
                case "-":
                    await Navigation.PushAsync(new ExerciseSettings(2));
                    break;
                
                case "×":
                    await Navigation.PushAsync(new ExerciseSettings(3));
                    break;
                
                case "÷":
                    await Navigation.PushAsync(new ExerciseSettings(4));
                    break;

                case "x=":
                    await Navigation.PushAsync(new ExerciseSettings(6));
                    break;
            }
        }

        private async void SelectPage(object sender, EventArgs e)
        {
            try
            {
                ToolbarItem i = (ToolbarItem) sender;
                switch (i.IconImageSource.ToString())
                {
                    case "File: menu_icon.png":
                        await Navigation.PushAsync(new MainPage());
                        var existingPages = Navigation.NavigationStack.ToList();
                        foreach (var page in existingPages)
                        {
                            Navigation.RemovePage(page);
                        }
                        break;
                
                    case "File: settings_icon.png":
                        await Navigation.PushAsync(new Settings());
                        break;

                    case "File: info_icon.png":
                        await Navigation.PushAsync(new AboutUs());
                        break;
                
                    default:
                        await Navigation.PushAsync(new Statistics()); 
                        break;
                }
            }
            catch //(Exception exception)
            {
                await Navigation.PushAsync(new Statistics());
            }
        }
        
        protected override bool OnBackButtonPressed()
        {
            long currentTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;

            if (currentTime - lastPress > 3000)
            {
                DependencyService.Get<INativeFun>().LongAlert(Localization.AlertExit);
                lastPress = currentTime;
                return true;
            }

            base.OnBackButtonPressed();
            return false;
        }
    }
}