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
        private SettingsModel settings;
        
        public Statistics()
        {
            InitializeComponent();
            Title = Localization.Statistics;
            
            Task task = Task.Run(async () =>
            {
                table = await App.Database.GetTable();
                settings = await App.Database.GetSettings();
            });
            Task.WaitAll(task);
            
            LoadPage(Localization.Total, table.TotalExercisesCorrect, table.TotalExercises, "total_icon.png", Color.FromHex(settings.BackgroundHex));
            LoadPage(Localization.Addition, table.TotalAddCorrect, table.TotalAdd, "add_icon.png", Color.FromHex(settings.BackgroundHex));
            LoadPage(Localization.Subtraction, table.TotalSubCorrect, table.TotalSub, "sub_icon.png", Color.FromHex(settings.BackgroundHex));
            LoadPage(Localization.Multiplication, table.TotalMulCorrect, table.TotalMul, "mul_icon.png", Color.FromHex(settings.BackgroundHex));
            LoadPage(Localization.Division, table.TotalDivCorrect, table.TotalDiv, "div_icon.png", Color.FromHex(settings.BackgroundHex));
            LoadPage(Localization.Ratio, table.TotalAdd, table.TotalSub, table.TotalMul, table.TotalDiv, "ratio_icon.png", Color.FromHex(settings.BackgroundHex));
        }

        private void LoadPage(string title, int amountCorrect, int amountTotal, string imageSource, Color color)
        {
            Children.Add(new StatisticsPage(title, amountCorrect, amountTotal){IconImageSource = imageSource, BackgroundColor = color});
        }
        private void LoadPage(string title, int add, int sub, int mul, int div, string imageSource, Color color)
        {
            Children.Add(new StatisticsPage(title, add, sub, mul, div){IconImageSource = imageSource, BackgroundColor = color});
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
    }
}