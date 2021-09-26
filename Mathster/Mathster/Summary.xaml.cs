﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mathster.Resources.Database_Models;
using Mathster.Resources.Helpers;
using Mathster.Resources.Localization;
using Sterform.Mathster.Exercise;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Summary : ContentPage
    {
        private readonly List<Exercise> correctList, wrongList;
        private readonly Exercise[] queue;
        private ObjectsModel objects;
        private SettingsModel settings;
        private DBModel table;
        private bool transaction;

        public Summary(Exercise[] queue, bool transaction)
        {
            InitializeComponent();
            var task = Task.Run(async () =>
            {
                table = await App.Database.GetTable();
                settings = await App.Database.GetSettings();
                objects = await App.Database.GetObjects();
            });
            Task.WaitAll(task);

            // SVG loaded from DB 
            ObjCorrect.Data = objects.ObjCorrect;
            ObjWrong.Data = objects.ObjWrong;


            MenuToolbarButton.IconImageSource = "menu_icon.png";
            this.queue = queue;
            this.transaction = transaction;

            Title = Localization.Summary;
            MenuButton.Text = Localization.Menu;
            TitleSummaryLabel.Text = Localization.Results;

            var experienceGained = 0;
            correctList = new List<Exercise>();
            wrongList = new List<Exercise>();

            // For list view 
            var exercises = new Result [queue.Length];

            for (var i = 0; i < queue.Length; i++)
            {
                var ex = queue[i];
                var correct = true;
                exercises[i] = new Result(ex.FormatAssigmentUserInput(), settings);

                if (ex.FormatAssigmentUserInput().Length > 15 && ex.ExerciseType == 5 || ex.ExerciseType == 5)
                    ResultList.RowHeight = 80;
                else if (ex.ExerciseType >= 6) ResultList.RowHeight = 110;

                // TODO: udělat metodu (něco na styl "bool wasAnsCorrect(Exercise exercise)"), aby tady nebyl
                // redundantní kód na výpočet toho, jestli byla otázka správně, jelikož se liší to určení u různých typů
                // příkladu
                // (ještě se to opakuje v SummaryDetail.xaml.cs řádek 63, kde je do toho ale zahrnutý i formátování - řádek 74)
                bool ansWasCorrect;
                
                // Exercise type that requires one input from user (+,-,×,÷, lin equations)
                if (ex.ExerciseType <= 5)
                {
                    ansWasCorrect = ex.UserInput == ex.Result;
                }
                // Exercise type requires two inputs from user (quadratic equations, completing the square)
                else
                {
                    // Two inputs that need to be in a specific order (completing the square)
                    if (ex.ExerciseType == 7)
                    {
                        ansWasCorrect = ex.UserInput == ex.Result &&
                                        ex.UserInput2 == ex.Result2;
                    }
                    // Two inputs, can be in whatever order (quadratic equations)
                    else
                    {
                        ansWasCorrect = (ex.UserInput == ex.Result && ex.UserInput2 == ex.Result2) ||
                                        (ex.UserInput == ex.Result2 && ex.UserInput2 == ex.Result);
                    }
                }
                
                if (ansWasCorrect)
                {
                    table.AddGoodStats(ex.ExerciseType, table);
                    exercises[i].Obj = objects.ObjCorrect;
                    exercises[i].ObjColor = new SolidColorBrush(Color.FromHex("#C9FF50"));
                    correctList.Add(ex);
                    exercises[i].Margin = 1;
                }
                else
                {
                    correct = false;
                    table.AddStats(ex.ExerciseType, table);
                    exercises[i].Obj = objects.ObjWrong;
                    exercises[i].ObjColor = new SolidColorBrush(Color.FromHex("#FCA54D"));
                    wrongList.Add(ex);
                    exercises[i].Margin = 8;
                }

                experienceGained += ex.GetExperience(correct);
            }

            ResultList.ItemsSource = exercises;
            table.Experience += experienceGained;
            CorrectCountButton.Text = correctList.Count.ToString();
            WrongCountButton.Text = wrongList.Count.ToString();

            if (transaction) return;
            DependencyService.Get<INativeFun>().ShortAlert($"{Localization.Gained} {experienceGained} Xp");
            Transaction();
        }

        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages) Navigation.RemovePage(page);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BackgroundColor = Color.FromHex(settings.BackgroundHex);
            CorrectCountButton.BackgroundColor = Color.FromHex(settings.BackgroundHex);
            WrongCountButton.BackgroundColor = Color.FromHex(settings.BackgroundHex);
            if (!settings.DarkMode) return;
            TitleSummaryLabel.TextColor = Color.FromHex("#FFFFFF");
            CorrectCountButton.TextColor = Color.FromHex("#FFFFFF");
            WrongCountButton.TextColor = Color.FromHex("#FFFFFF");
        }

        private async void Transaction()
        {
            if (transaction) return;
            await App.Database.UpdateTable(table);
            transaction = true;
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
            var selectedItem = byte.Parse(e.ItemIndex.ToString());
            await Navigation.PushAsync(new SummaryDetail(selectedItem, queue, transaction));
            if (sender is ListView lv) lv.SelectedItem = null;
        }
    }

    public class Result
    {
        public Result(string assignment, SettingsModel settings)
        {
            Assignment = assignment;
            CellColor = Color.FromHex(settings.BackgroundHex);
            TextColor = settings.DarkMode ? Color.FromHex("#FFFFFF") : Color.Black;
        }

        public string Assignment { get; set; }
        public Geometry Obj { get; set; }
        public Color CellColor { get; set; }
        public Brush ObjColor { get; set; }
        public Color TextColor { get; set; }
        public int Margin { get; set; }
    }
}