using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Mathster.Resources.Localization;
using Sterform.Mathster.Exercise;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SummaryDetail : ContentPage
    {
        private byte id;
        private readonly List<Exercise> list;
        private readonly Exercise[] queue;
        private readonly bool transaction;


        public SummaryDetail(byte id, Exercise[] queue, bool transaction, List<Exercise> list = null)
        {
            InitializeComponent();
            MenuButton.IconImageSource = "menu_icon.png";
            TimeStaticLabel.Text = Localization.CountTime;
            AssignmentStaticLabel.Text = Localization.Assignment;
            CorrectStaticLabel.Text = Localization.SolutionCorrect;
            WrongStaticLabel.Text = Localization.YourSolution;
            Title = $"{Localization.Summary} | {id + 1}/{queue.Length}";

            var task = Task.Run(async () =>
            {
                var objs = await App.Database.GetObjects();
                ObjCorrect.Data = objs.ObjCorrect;
                ObjWrong.Data = objs.ObjWrong;
            });
            Task.WaitAll(task);

            Exercise exercise;

            if (list != null)
            {
                exercise = list[id];
                this.list = list;
                if (id == list.Count - 1) NextButton.IsVisible = false;
            }
            else
            {
                exercise = queue[id];
                if (id == queue.Length - 1) NextButton.IsVisible = false;
            }

            long sec = exercise.CountLenght / TimeSpan.TicksPerSecond, min = sec / 60;
            sec = sec - min * 60;
            TimeLabel.Text = $"{min}min {sec}s";

            AssignmentLabel.Text = exercise.Assignment;
            WrongLabel.Text = exercise.FormatUserInput();
            CorrectLabel.Text = exercise.FormatResult();
            
            // TODO: uvést jakej ExerciseType je jaký cvičení (možná to někde je, ale nemohl jsem to najít), možná něco na styl enum
            bool ansWasCorrect = false;

            // Exercise type that requires one input from user (+,-,×,÷, lin equations)
            if (exercise.ExerciseType <= 5)
            {
                ansWasCorrect = exercise.UserInput == exercise.Result;
            }
            // Exercise type requires two inputs from user (quadratic equations, completing the square)
            else
            {
                // Formatting
                WrongLabel.HeightRequest = 75;
                CorrectLabel.HeightRequest = 75;
                
                // Two inputs that need to be in a specific order (completing the square)
                if (exercise.ExerciseType == 7)
                {
                    ansWasCorrect = exercise.UserInput == exercise.Result &&
                                    exercise.UserInput2 == exercise.Result2;
                }
                // Two inputs, can be in whatever order (quadratic equations)
                else
                {
                    ansWasCorrect = (exercise.UserInput == exercise.Result && exercise.UserInput2 == exercise.Result2) ||
                                    (exercise.UserInput == exercise.Result2 && exercise.UserInput2 == exercise.Result);
                }
            }

            if (ansWasCorrect)
            {
                WrongStaticLabel.IsVisible = false;
                WrongFrame.IsVisible = false;
            }

            this.queue = queue;
            this.transaction = transaction;
            this.id = id;

            if (id == 0) PreviousButton.IsVisible = false;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var settings = await App.Database.GetSettings();
            BackgroundColor = Color.FromHex(settings.BackgroundHex);
            AssignmentLabelFrame.BackgroundColor = Color.FromHex(settings.BackgroundHex);
            CorrectLayout.BackgroundColor = Color.FromHex(settings.BackgroundHex);
            WrongLayout.BackgroundColor = Color.FromHex(settings.BackgroundHex);
            if (!settings.DarkMode) return;
            BackgroundColor = Color.FromHex(settings.BackgroundHex);
            AssignmentLabel.TextColor = Color.White;
            CorrectLabel.TextColor = Color.White;
            WrongLabel.TextColor = Color.White;
        }

        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages) Navigation.RemovePage(page);
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