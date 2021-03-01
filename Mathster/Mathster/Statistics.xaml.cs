using System;
using System.Linq;
using System.Threading.Tasks;
using Mathster.Resources.Database_Models;
using Mathster.Resources.Localization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Statistics : TabbedPage
    {
        private DBModel table;

        public Statistics()
        {
            InitializeComponent();
            Title = Localization.Statistics;

            var task = Task.Run(async () => { table = await App.Database.GetTable(); });
            Task.WaitAll(task);

            LoadPage(Localization.Total, table.TotalExercisesCorrect, table.TotalExercises, "total_icon.png");
            LoadPage(Localization.Addition, table.TotalAddCorrect, table.TotalAdd, "add_icon.png");
            LoadPage(Localization.Subtraction, table.TotalSubCorrect, table.TotalSub, "sub_icon.png");
            LoadPage(Localization.Multiplication, table.TotalMulCorrect, table.TotalMul, "mul_icon.png");
            LoadPage(Localization.Division, table.TotalDivCorrect, table.TotalDiv, "div_icon.png");
            LoadPage(Localization.Ratio, table.TotalAdd, table.TotalSub, table.TotalMul, table.TotalDiv,
                "ratio_icon.png");
            LoadPage(Localization.LinearEquation, table.TotalLinearCorrect, table.TotalLinear, "linear_icon.png");
            LoadPage(Localization.QuadraticEquation, table.TotalQuadraticCorrect, table.TotalQuadratic,
                "quadratic_icon.png");
            LoadPage(Localization.CompleteTheSquare, table.TotalSquareCorrect, table.TotalSquare, "square_icon.png");
        }

        private void LoadPage(string title, int amountCorrect, int amountTotal, string imageSource)
        {
            Children.Add(new StatisticsPage(title, amountCorrect, amountTotal)
                {IconImageSource = imageSource});
        }

        private void LoadPage(string title, int add, int sub, int mul, int div, string imageSource)
        {
            Children.Add(new StatisticsPage(title, add, sub, mul, div)
                {IconImageSource = imageSource});
        }

        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages) Navigation.RemovePage(page);
        }
    }
}