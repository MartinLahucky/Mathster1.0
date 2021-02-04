using System;
using System.Collections.Generic;
using System.Linq;
using Mathster.Resources.Database_Models;
using Mathster.Resources.Exercises;
using Mathster.Resources.Localization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SummaryDetail : ContentPage
    {
        private byte id;
        private Exercise[] queue;
        private List<Exercise> list;
        private bool transaction;


        public SummaryDetail(byte id, Exercise[] queue, bool transaction, List<Exercise> list = null)
        {
            InitializeComponent();
            MenuButton.IconImageSource = "menu_icon.png";
            TimeStaticLabel.Text = Localization.CountTime;
            AssignmentStaticLabel.Text = Localization.Assignment;
            CorrectStaticLabel.Text = Localization.SolutionCorrect;
            WrongStaticLabel.Text = Localization.YourSolution;

            Exercise exercise;

            if (list != null)
            {
                exercise = list[id];
                this.list = list;
                if (id == list.Count - 1)
                {
                    NextButton.IsVisible = false;
                }
            }
            else
            {
                exercise = queue[id];
                if (id == queue.Length - 1)
                {
                    NextButton.IsVisible = false;
                }
            }

            long sec = exercise.CountLenght / TimeSpan.TicksPerSecond, min = sec / 60;
            sec = sec - min * 60;
            TimeLabel.Text = $"{min}min {sec}s";

            if (exercise.Assignment.Length >= 13 || exercise.ExerciseType == 5)
            {
                WrongLabel.HeightRequest = 75;
                CorrectLabel.HeightRequest = 75;
            }
            else if (exercise.ExerciseType >= 6)
            {
                WrongLabel.HeightRequest = 110;
                CorrectLabel.HeightRequest = 110;
            }

            AssignmentLabel.Text = exercise.Assignment;
            WrongLabel.Text = exercise.FormatUserInput();
            CorrectLabel.Text = exercise.FormatResult();

            if (exercise.UserInput == exercise.Result && exercise.UserInput2 == exercise.Result2)
            {
                WrongStaticLabel.IsVisible = false;
                WrongFrame.IsVisible = false;
            }

            this.queue = queue;
            this.transaction = transaction;
            this.id = id;

            if (id == 0)
            {
                PreviousButton.IsVisible = false;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            SettingsModel settings = await App.Database.GetSettings();
            BackgroundColor = Color.FromHex(settings.BackgroundHex);
            AssignmentLabelFrame.BackgroundColor = Color.FromHex(settings.BackgroundHex);
            CorrectLayout.BackgroundColor = Color.FromHex(settings.BackgroundHex);
            WrongLayout.BackgroundColor = Color.FromHex(settings.BackgroundHex);
            if (settings.DarkMode)
            {
                BackgroundColor = Color.FromHex(settings.BackgroundHex);
                AssignmentLabel.TextColor = Color.White;
                CorrectLabel.TextColor = Color.White;
                WrongLabel.TextColor = Color.White;
            }
        }

        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                Navigation.RemovePage(page);
            }
        }

        private async void PreviousButton_OnClicked(object sender, EventArgs e)
        {
            id--;
            await Navigation.PushAsync(new SummaryDetail(id, queue, transaction, list));
        }

        private async void SummaryButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Summary(queue, transaction));
        }

        private async void NextButton_OnClicked(object sender, EventArgs e)
        {
            id++;
            await Navigation.PushAsync(new SummaryDetail(id, queue, transaction, list));
        }
    }
}