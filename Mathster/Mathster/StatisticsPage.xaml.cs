using System;
using Mathster.Resources.Custom_UI;
using Mathster.Resources.Database_Models;
using Mathster.Resources.Localization;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsPage : ContentPage
    {
        private Circles chart;

        public StatisticsPage(string title, int amountCorrect, int amountTotal)
        {
            InitializeComponent();
            TitleLabel.Text = title;
            ResetStatsButton.Text = Localization.ResetStatistics;
            TotalOperationCounterLabel.Text = $"{amountTotal}";
            AddCountLabel.Text = $"{amountCorrect}";
            SubCountLabel.Text = $"{amountTotal - amountCorrect}";

            AddLabel.IsVisible = false;
            SubLabel.IsVisible = false;

            if (amountTotal == 0) amountTotal = 1;
            chart = new Circles(180, (info) => new SKPoint((float) info.Width / 2, (float) info.Height / 2));
            chart.DrawFullProgressBar(Chart, "#7F7FFD", "#FCA54D", 40f,
                amountCorrect / (float) (amountTotal) * 100f, "#C9FF50");
        }

        public StatisticsPage(string title, int add, int sub, int mul, int div)
        {
            InitializeComponent();
            TitleLabel.Text = title;
            ResetStatsButton.Text = Localization.ResetStatistics;
            TotalOperationCounterLabel.Text = $"{add + sub + mul + div}";
            AddCountLabel.Text = $"{add}";
            SubCountLabel.Text = $"{sub}";
            MulCountLabel.Text = $"{mul}";
            DivCountLabel.Text = $"{div}";

            MulLayout.IsVisible = true;
            DivLayout.IsVisible = true;
            AddImage.IsVisible = false;
            SubImage.IsVisible = false;

            chart = new Circles(180, (info) => new SKPoint((float) info.Width / 2, (float) info.Height / 2));
            chart.DrawChart(Chart, "#7F7FFD", "#FCA54D", 40f, mul, div, sub,
                add + sub + mul + div, "#C9FF50", "#262630", "#FFFFFF");
        }

        private async void ResetStatsButton_OnClicked(object sender, EventArgs eventArgs)
        {
            if (await DisplayAlert(Localization.Alert, Localization.AlertResetStatistics, Localization.Yes,
                Localization.No))
            {
                DBModel table = await App.Database.GetTable();
                DBModel tableReset = new DBModel
                {
                    Name = table.Name,
                    Experience = 0,
                    TotalExercises = 0,
                    TotalAdd = 0,
                    TotalAddCorrect = 0,
                    TotalSub = 0,
                    TotalSubCorrect = 0,
                    TotalMul = 0,
                    TotalMulCorrect = 0,
                    TotalDiv = 0,
                    TotalDivCorrect = 0,
                    TotalExercisesCorrect = 0,
                };
                await App.Database.UpdateTable(tableReset);
                await Navigation.PushAsync(new MainPage());
            }
        }
    }
}