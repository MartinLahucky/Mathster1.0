﻿using System;
using System.Linq;
using Mathster.Resources.Helpers;
using Mathster.Resources.Localization;
// using Mathster.Resources.Images;
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

            var assembly = typeof(MainPage);
            MenuToolbarButton.IconImageSource = ImageSource.FromResource("Mathster.Resources.Images.menu_icon.png", assembly);
            SettingsButton.IconImageSource = ImageSource.FromResource("Mathster.Resources.Images.settings_icon.png", assembly);
            StatsToolbarButton.IconImageSource = ImageSource.FromResource("Mathster.Resources.Images.statistics_icon.png", assembly);
            AboutToolbarButton.IconImageSource = ImageSource.FromResource("Mathster.Resources.Images.info_icon.png", assembly);

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
            var settings = await App.Database.GetSettings();
            BackgroundColor = Color.FromHex(settings.BackgroundHex);
            var table = await App.Database.GetTable();
            if (string.IsNullOrEmpty(table.Name))
                UserLabel.Text = "Mathster";
            else
                UserLabel.Text = table.Name;

            table.GetLevel(out var level, out var progress, table);
            LevelButton.Text = level.ToString();
            ProgressBar.AnimatedProgress = progress;
        }

        private async void SelectExercise(object sender, EventArgs e)
        {
            var button = (Button) sender;
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
                    await Navigation.PushAsync(new ExerciseSettings(5));
                    break;
            }
        }

        private async void SelectPage(object sender, EventArgs e)
        {
            try
            {
                var i = (ToolbarItem) sender;
                switch (i.IconImageSource.ToString())
                {
                    case "File: menu_icon.png":
                        await Navigation.PushAsync(new MainPage());
                        var existingPages = Navigation.NavigationStack.ToList();
                        foreach (var page in existingPages) Navigation.RemovePage(page);

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
            var currentTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;

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