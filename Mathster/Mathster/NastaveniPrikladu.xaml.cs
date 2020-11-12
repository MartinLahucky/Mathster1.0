using System;
using System.Collections.Generic;
using Mathster.Helpers.Model;
using Mathster.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NastaveniPrikladu : ContentPage
    {
        private byte druhPrikladu;
        //  proměnné určující nastavení:
        //  (jsou nastavené na default hodnoty - ty které se zobrazí při zapnutí aplikace)
        private int pocetPrikladu = 5;
        private int maxPocetPrikladu = 20;
        //
        private int pocetCifer = 2;
        private int maxPocetCifer = 6;
        //
        private int velikostDelitele = 2;
        private int maxVelikostDelitele = 3;

        public NastaveniPrikladu(byte druhPrikladu)
        {
            InitializeComponent();
            this.druhPrikladu = druhPrikladu;
            DalsiButton.Text = AppResource.Dalsi;
            nazev1Label.Text = AppResource.ZvolitPocetPrikladu;
            nazev2Label.Text = AppResource.VelikostCisel;
            nazev3Label.Text = AppResource.ZvolitNasobitelADelitel;
            MenuToolbarButton.IconImageSource = "round_house_white_18dp.png";
            
            pocetPrikladuSlider.Value = pocetPrikladu;
            pocetPrikladuSlider.Maximum = maxPocetPrikladu;
            pocetPrikladuSlider.Minimum = 1;
            pocetPrikladuLabel.Text = pocetPrikladu.ToString();
            
            pocetCiferSlider.Value = pocetCifer;
            pocetCiferSlider.Maximum = maxPocetCifer;
            pocetCiferSlider.Minimum = 1;
            pocetCiferLabel.Text = pocetCifer.ToString();
            
            velikostDeliteleSlider.Value = velikostDelitele;
            velikostDeliteleSlider.Maximum = maxVelikostDelitele;
            velikostDeliteleSlider.Minimum = 1;
            velikostDeliteleSlider.IsEnabled = false;
            
            switch (druhPrikladu)
            {
                case 1:
                    Title = AppResource.Scitani;
                    DeleniANasbeniFrame.IsVisible = false;
                    break;
                case 2:
                    Title = AppResource.Odecitani;
                    DeleniANasbeniFrame.IsVisible = false;
                    break;
                case 3:
                    Title = AppResource.Nasobeni;
                    break;
                case 4:
                    Title = AppResource.Deleni;
                    break;
                case 5:
                    Title = AppResource.Nahodne;
                    break;
            }
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            SettingsModel tabulkaNastaveni = await App.Database.GetSettings();
            BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
            Frame1.BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
            Frame2.BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
            Frame3.BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
            if (tabulkaNastaveni.DarkMode)
            {
                pocetPrikladuLabel.TextColor = Color.White;
                pocetCiferLabel.TextColor = Color.White;
                velikostDeliteleLabel.TextColor = Color.White;
            } 
        }
        private void pocetPrikladu_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            pocetPrikladu = (int)pocetPrikladuSlider.Value;
            pocetPrikladuLabel.Text = pocetPrikladu.ToString();
        }

        private void pocetCiferSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            pocetCifer = (int)pocetCiferSlider.Value;
            if (pocetCifer == 1)
            {
                velikostDeliteleSlider.Value = 1;
                velikostDeliteleSlider.IsEnabled = false;
            }
            else
            {
                velikostDeliteleSlider.IsEnabled = true;
            }
            pocetCiferLabel.Text = pocetCifer.ToString();
        }

        private void velikostDeliteleSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            velikostDelitele = (int)velikostDeliteleSlider.Value;
            switch (velikostDelitele)
            {
                case 1:
                    velikostDeliteleLabel.Text = 5.ToString();
                    break;
                case 2:
                    velikostDeliteleLabel.Text = 10.ToString();
                    break;
                case 3:
                    velikostDeliteleLabel.Text = 20.ToString();
                    break;
            }
        }
        private async void DalsiButton_OnClicked(object sender, EventArgs e)
        {
            int minCisel;
            int maxCisel;
            byte minDeleniANasobeni = 2;
            byte maxDeleniANasobeni = 6;
            byte pocetPrikladu = (byte)pocetPrikladuSlider.Value;
            
            switch ((int)pocetCiferSlider.Value)
            {
                case 1:
                    minCisel = 1;
                    maxCisel = 10;
                    break;
                case 2:
                    minCisel = 10;
                    maxCisel = 100;
                    break;
                case 3:
                    minCisel = 100;
                    maxCisel = 1000;
                    break;
                case 4:
                    minCisel = 1000;
                    maxCisel = 10000;
                    break;
                case 5:
                    minCisel = 10000;
                    maxCisel = 100000;
                    break;
                case 6:
                    minCisel = 100000;
                    maxCisel = 1000000;
                    break;
                default:
                    minCisel = 1;
                    maxCisel = 10;
                    break;
            }
            
            if (druhPrikladu == 3 || druhPrikladu == 4 || druhPrikladu == 5)
            {
                switch ((int)velikostDeliteleSlider.Value)
                {
                    case 1:
                        minDeleniANasobeni = 2;
                        maxDeleniANasobeni = 6;
                        break;
                    case 2:
                        minDeleniANasobeni = 2;
                        maxDeleniANasobeni = 11;
                        break;
                    case 3:
                        minDeleniANasobeni = 2;
                        maxDeleniANasobeni = 21;
                        break;
                    default:
                        minDeleniANasobeni = 2;
                        maxDeleniANasobeni = 6;
                        break;
                }
            }
            // TODO Předělat na pole
            List<Priklad> fronta = new List<Priklad>();

            if (druhPrikladu == 1 || druhPrikladu == 2)
            {
                for (byte i = 0; i < pocetPrikladu; i++)
                {
                    fronta.Add(new Priklad().VygenerujPriklad(i, minCisel, maxCisel, druhPrikladu, minDeleniANasobeni, maxDeleniANasobeni));
                }
            }
            else if (druhPrikladu == 3 || druhPrikladu == 4)
            {
                for (byte i = 0; i < pocetPrikladu; i++)
                {
                    fronta.Add(new Priklad().VygenerujPriklad(i, minCisel, maxCisel, druhPrikladu, minDeleniANasobeni, maxDeleniANasobeni));
                }
            }
            else
            {
                for (byte i = 0; i < pocetPrikladu; i++)
                {
                    fronta.Add(new Priklad().VygenerujNahodnyPriklad(i, minCisel, maxCisel, minDeleniANasobeni, maxDeleniANasobeni));
                }
            }
            await Navigation.PushAsync(new Priklady(0, fronta));
        }
        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
        }
    }
}     