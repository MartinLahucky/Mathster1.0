using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mathster.Resources.Database_Models;
using Mathster.Resources.Exercises;
using Mathster.Resources.Helpers;
using Mathster.Resources.Localization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Summary : ContentPage
    {
        private Exercise[] queue;
        private DBModel table;
        private SettingsModel settings;
        private bool transaction;
        private List<Exercise> correctList;
        private List<Exercise> wrongList;

        public Summary(Exercise[] queue, bool transaction)
        {
            InitializeComponent();
            MenuToolbarButton.IconImageSource = "menu_icon.png";
            this.queue = queue;
            this.transaction = transaction;

            Title = Localization.Summary;
            MenuButton.Text = Localization.Menu;
            TitleSummaryLabel.Text = Localization.Results;

            int experienceGained = 0;
            correctList = new List<Exercise>();
            wrongList = new List<Exercise>();

            Task task = Task.Run(async () =>
            {
                table = await App.Database.GetTable();
                settings = await App.Database.GetSettings();
            });
            Task.WaitAll(task);

            // For list view 
            Result[] exercises = new Result [queue.Length];

            for (int i = 0; i < queue.Length; i++)
            {
                var ex = queue[i];
                bool correct = true;
                exercises[i] = new Result(ex.FormatAssigmentUserInput(), settings);
                
                if (ex.FormatAssigmentUserInput().Length > 15 || ex.ExerciseType == 5)
                {
                    ResultList.RowHeight = 80;
                }
                
                if (ex.Result == ex.UserInput && ex.Result2 == ex.UserInput2 && ex.Result2 == ex.UserInput && ex.Result == ex.UserInput2)
                {
                    table.AddGoodStats(ex.ExerciseType, table);
                    exercises[i].Image = "correct_icon.png";
                    correctList.Add(ex);
                }
                else
                {
                    correct = false;
                    table.AddStats(ex.ExerciseType, table);
                    exercises[i].Image = "wrong_icon.png";
                    wrongList.Add(ex);
                }

                experienceGained += ex.GetExperience(correct);
            }

            ResultList.ItemsSource = exercises;
            table.Experience += experienceGained;
            CorrectCountButton.Text = correctList.Count.ToString();
            WrongCountButton.Text = wrongList.Count.ToString();

            if (!transaction)
            {
                //TODO nejspíš odstranit? 
                DependencyService.Get<INativeFun>().ShortAlert($"{Localization.Gained} {experienceGained} Xp");
                Transaction();
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BackgroundColor = Color.FromHex(settings.BackgroundHex);
            CorrectCountButton.BackgroundColor = Color.FromHex(settings.BackgroundHex);
            WrongCountButton.BackgroundColor = Color.FromHex(settings.BackgroundHex);
            if (settings.DarkMode)
            {
                TitleSummaryLabel.TextColor = Color.FromHex("#FFFFFF");
                CorrectCountButton.TextColor = Color.FromHex("#FFFFFF");
                WrongCountButton.TextColor = Color.FromHex("#FFFFFF");
            }
        }

        private async void Transaction()
        {
            if (!transaction)
            {
                await App.Database.UpdateTable(table);
                transaction = true;
            }
        }

        private async void CorrectCountButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new SummaryDetail(0, queue, transaction, correctList));
            }
            catch
            {
                await DisplayAlert(Localization.Alert, Localization.AlertAllWrong, Localization.Ok);
            }
        }

        private async void WrongCountButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new SummaryDetail(0, queue, transaction, wrongList));
            }
            catch
            {
                await DisplayAlert(Localization.Alert, Localization.AlertAllCorrect, Localization.Ok);
            }
        }

        private async void ResultList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            byte selectedItem = byte.Parse(e.ItemIndex.ToString());
            await Navigation.PushAsync(new SummaryDetail(selectedItem, queue, transaction));
            if (sender is ListView lv) lv.SelectedItem = null;
        }
    }

    public class Result
    {
        public string Assignment { get; set; }
        public string Image { get; set; }
        public Color CellColor { get; set; }
        public Color TextColor { get; set; }

        public Result(string assignment, SettingsModel settings)
        {
            Assignment = assignment;
            CellColor = Color.FromHex(settings.BackgroundHex);
            if (settings.DarkMode)
            {
                TextColor = Color.FromHex("#FFFFFF");
            }
            else
            {
                TextColor = Color.Black;
            }
        }
    }
}