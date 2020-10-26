using System;
using System.Threading.Tasks;
using Mathster.Helpers.Custom_UI;
using Mathster.Helpers.Model;
using Mathster.Helpers.Resources;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        private Circles levelBar;
        public Menu()
        {
            InitializeComponent();
            Title = AppResource.Menu;
            MenuToolbarButton.IconImageSource = "round_house_white_18dp.png";
            SettingsToolbarButton.IconImageSource = "round_settings_white_18dp.png";
            StatsToolbarButton.IconImageSource = "round_person_white_18dp.png";
            AboutToolbarButton.IconImageSource = "outline_info_white_18dp.png";

            ScitaniButton.Text = "+";
            OdcitaniButton.Text = "-";
            NasobeniButton.Text = "×";
            DeleniButton.Text = "÷";
            NahodneButton.Text = "?";
            // //SkiaSharp
            levelBar = new Circles(180,info => new SKPoint((float)info.Width / 2,(float) info.Height / 2));
            levelBar.DrawFullProgressBar(SkCanvasView,"#7F7FFD","#7F7FFD",0, 0, "#7F7FFD");
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            SettingsModel tabulkaNastaveni = await App.Database.GetSettings();
            BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
            UzivatelLabel.TextColor = Color.White;
            DBModel tabulka = await App.Database.GetTable();
            if (tabulka.Jmeno == null || tabulka.Jmeno == "" || tabulka.Jmeno == String.Empty)
            {
                UzivatelLabel.Text = "Mathster Lite";
            }
            else
            {
                UzivatelLabel.Text = tabulka.Jmeno;
            }
            int level, progres;
            tabulka.GetLevel(out level, out progres, tabulka);
            levelButton.Text = level.ToString();
        }
        private async void ScitaniButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NastaveniPrikladu(1));
        }
        private async void OdcitaniButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NastaveniPrikladu(2));
        }
        private async void NasobeniButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NastaveniPrikladu(3));
        }
        private async void DeleniButton_OnClicked(object sender, EventArgs e)
        { 
            await Navigation.PushAsync(new NastaveniPrikladu(4));
        }
        private async void NahodneButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NastaveniPrikladu(5));
        }
        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
        }
        private async void StatsButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Statistiky());
        }
        private async void NastaveniButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Nastaveni());
        }
        private async void AboutButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ONas());
        }
    }
}