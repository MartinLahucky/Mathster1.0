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
    public partial class Priklady : ContentPage
    {
        private Priklad priklad;
        private byte ID;
        private List<Priklad> fronta;
        private bool podsebe;
        private bool podsebeUpraveno;
        private long casZahajeni;

        public Priklady(byte id, List<Priklad> fronta)
        {
            InitializeComponent();
            MenuToolbarButton.IconImageSource = "round_house_white_18dp.png";
            PodSebeButton.IconImageSource = "podsebe.png";
            DalsiButton.Text = ">";
            OdvezdatButton.Text = AppResource.Odevzdat;
            VysledekPropisInput.Text = String.Empty;
            VysledekInput.Text = String.Empty;
            casZahajeni = DateTime.UtcNow.Ticks;

            priklad = fronta[id];

            switch (priklad.DruhPrikladu)
            {
                case 1:
                    Title = $"{AppResource.Scitani} | {id + 1}/{fronta.Count}";
                    break;
                case 2:
                    Title = $"{AppResource.Odecitani} | {id + 1}/{fronta.Count}";
                    break;
                case 3:
                    Title = $"{AppResource.Nasobeni} | {id + 1}/{fronta.Count}";
                    break;
                case 4:
                    Title = $"{AppResource.Deleni} | {id + 1}/{fronta.Count}";
                    break;
                case 5:
                    Title = $"{AppResource.Rovnice} | {id + 1}/{fronta.Count}";
                    PodSebeButton.IconImageSource = "";
                    PodSebeButton.IsEnabled = false;
                    PodSebeButton.Priority = 0;
                    MenuToolbarButton.Priority = 1;
                    break;
            }
            PrikladLabel.Text = $"{priklad.Zadani}";
            this.fronta = fronta;
            ID = id;

            if (id < fronta.Count - 1)
            {
                OdvezdatButton.IsVisible = false;
            }
            else
            {
                DalsiButton.IsVisible = false;
            }
        }

        private async void OdvezdatButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                fronta[ID].UzivateluvVstup = float.Parse(VysledekPropisInput.Text);
                fronta[ID].DelkaPocitani = DateTime.UtcNow.Ticks - casZahajeni;
                await Navigation.PushAsync(new Souhrn(fronta, false));
                var existingPages = Navigation.NavigationStack.ToList();
                foreach (var page in existingPages)
                {
                    Navigation.RemovePage(page);
                }
            }
            catch // (Exception exception)
            {
                // await DisplayAlert(AppResource.Upozorneni, exception.Message, AppResource.Ok);
                await DisplayAlert(AppResource.Upozorneni, AppResource.UpozorneniZadejteCislo, AppResource.Ok);
            }
        }

        private async void DalsiButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                fronta[ID].UzivateluvVstup = float.Parse(VysledekPropisInput.Text);
                fronta[ID].DelkaPocitani = DateTime.UtcNow.Ticks - casZahajeni;
                ID++;
                await Navigation.PushAsync(new Priklady(ID, fronta));
                var existingPages = Navigation.NavigationStack.ToList();
                foreach (var page in existingPages)
                {
                    Navigation.RemovePage(page);
                }
            }
            catch // (Exception exception)
            {
                // await DisplayAlert(AppResource.Upozorneni, exception.Message, AppResource.Ok);
                await DisplayAlert(AppResource.Upozorneni, AppResource.UpozorneniZadejteCislo, AppResource.Ok);
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            SettingsModel tabulkaNastaveni = await App.Database.GetSettings();
            BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
            VysledekInput.Focus();
            if (tabulkaNastaveni.DarkMode)
            {
                PrikladLabel.TextColor = Color.White;
                VysledekPropisInput.TextColor = Color.White;
            }
        }

        private void PodSebeButton_OnClicked(object sender, EventArgs e)
        {
            switch (podsebe)
            {
                case false:
                    SecondLayer.Margin = new Thickness(60, -40, 60, 0);
                    PrikladLabel.Text = priklad.ZadaniPodsebe;
                    VysledekInput.FlowDirection = FlowDirection.RightToLeft;
                    podsebe = true;
                    break;
                case true:
                    SecondLayer.Margin = new Thickness(60, 30, 60, 0);
                    PrikladLabel.Text = priklad.Zadani;
                    VysledekInput.FlowDirection = FlowDirection.LeftToRight;
                    podsebe = false;
                    break;
            }

            VysledekInput.Focus();
        }

        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                Navigation.RemovePage(page);
            }
        }

        private void VysledekInput_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // I have no idea what I'm doing 
            // Vytvoření listu s číslem a navigace v daném čísle 
            List<string> cislo = new List<string>();
            string cisloText = VysledekInput.Text;
            // Kontrola, zda není číslo náhodou prázdné
            if (VysledekInput.Text == "")
            {
                podsebeUpraveno = false;
                VysledekPropisInput.Text = String.Empty;
            }
            else
            {
                try
                {
                    switch (podsebe)
                    {
                        case true:
                            // Načtení zprávného čísla
                            for (int i = 0; i < cisloText.Length; i++)
                            {
                                cislo.Add(cisloText[i].ToString());
                            }

                            // Pokud je číslo záporné, uprav mínus
                            if (cislo[0] == "-")
                            {
                                cislo.Remove("-");
                                cislo.Add("-");
                            }

                            // Výpis první číslice 
                            VysledekPropisInput.Text = cislo[cislo.Count - 1];
                            // Výpis zbytku čísla, pokud je delší než jeden znak 
                            if (cislo.Count != 1)
                            {
                                for (int i = cislo.Count - 2; i >= 0; i--)
                                {
                                    VysledekPropisInput.Text += cislo[i];
                                }
                            }

                            podsebeUpraveno = true;
                            break;

                        case false:
                            // Zamezení převracení čísla při změně režimů
                            if (podsebeUpraveno)
                            {
                                // Přidání posledního nevého znaku e.NewTextValue[e.NewTextValue.Length -1]
                                cisloText = VysledekPropisInput.Text + e.NewTextValue[e.NewTextValue.Length - 1];
                                VysledekInput.Text = cisloText;
                                podsebeUpraveno = false;
                            }

                            // Načtení správného tvaru čísla 
                            for (int i = 0; i < cisloText.Length; i++)
                            {
                                cislo.Add(cisloText[i].ToString());
                            }

                            // Opět výpis, který nechápu jak funguje 
                            VysledekPropisInput.Text = cislo[0];
                            if (cislo.Count != 1)
                            {
                                for (int i = 1; i < cisloText.Length; i++)
                                {
                                    VysledekPropisInput.Text += cislo[i];
                                }
                            }

                            break;
                    }
                }
                catch
                {
                    VysledekPropisInput.Text = String.Empty;
                    VysledekInput.Text = String.Empty;
                }
            }
        }
    }
}