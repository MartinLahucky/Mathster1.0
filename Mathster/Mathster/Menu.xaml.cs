using System;
using Mathster.Helpers.Model;
using Mathster.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            InitializeComponent();
            Title = AppResource.Menu;
            ScitaniButton.TextColor = Color.White;
            OdcitaniButton.TextColor = Color.White;
            NasobeniButton.TextColor = Color.White;
            DeleniButton.TextColor = Color.White;
            NahodneButton.TextColor = Color.White;
            UzivatelLabel.TextColor = Color.White;
            MenuToolbarButton.IconImageSource = "round_house_white_18dp.png";
            SettingsToolbarButton.IconImageSource = "round_settings_white_18dp.png";
            StatsToolbarButton.IconImageSource = "round_person_white_18dp.png";
            AboutToolbarButton.IconImageSource = "outline_info_white_18dp.png";
        }
        
        protected async override void OnAppearing()
        {
            DBModel tabulka = await App.Database.GetTable();
            if (tabulka.Jmeno == null || tabulka.Jmeno == "" || tabulka.Jmeno == String.Empty)
            {
                UzivatelLabel.Text = "Masther Lite";
            }
            else
            {
                UzivatelLabel.Text = tabulka.Jmeno;
            }
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