using System;
using System.Collections.Generic;
using System.Linq;
using Mathster.Helpers.Model;
using Mathster.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RozborVysledku : ContentPage
    {
        private byte ID;
        private List<Priklad> fronta;
        private List<Priklad> frontaVse;
        private bool potvrzeniZapisu;
        public RozborVysledku(byte id, List<Priklad> fronta, List<Priklad> frontaVse, bool potvrzeniZapisu)
        {
            InitializeComponent();
            MenuToolbarButton.IconImageSource = "round_house_white_18dp.png";
            Title = $"{AppResource.Reseni} | {fronta[id].Id + 1}/{frontaVse.Count}";
            ReseniNadLabel.Text = AppResource.SpravneReseni;
            NadSpatnaOdpovedLabel.Text = AppResource.VaseReseni;
            NadZadaniLabel.Text = AppResource.Zadani;

            if (fronta[id].PrvniCislo >= 10000)
            {
                SpatnaOdpovedLabel.HeightRequest = 75;
                ReseniLabel.HeightRequest = 75;
            }
            switch (fronta[id].DruhPrikladu)
            {
                case 1:
                    ZadaniLabel.Text =
                        $"{fronta[id].PrvniCislo} + {fronta[id].DruheCislo} = ";
                    SpatnaOdpovedLabel.Text = $"{fronta[id].UzivateluvVstup}";
                    ReseniLabel.Text = $"{fronta[id].PrvniCislo + fronta[id].DruheCislo}";
                    if (fronta[id].UzivateluvVstup == fronta[id].PrvniCislo + fronta[id].DruheCislo)
                    {
                        NadSpatnaOdpovedLabel.IsVisible = false;
                        SpatneFrame.IsVisible = false;
                    }
                    break;
                case 2:
                    ZadaniLabel.Text =
                        $"{fronta[id].PrvniCislo} - {fronta[id].DruheCislo} = ";
                    SpatnaOdpovedLabel.Text = $"{fronta[id].UzivateluvVstup}";
                    ReseniLabel.Text = $"{fronta[id].PrvniCislo - fronta[id].DruheCislo}";
                    if (fronta[id].UzivateluvVstup == fronta[id].PrvniCislo - fronta[id].DruheCislo)
                    {
                        NadSpatnaOdpovedLabel.IsVisible = false;
                        SpatneFrame.IsVisible = false;
                    }
                    break;
                case 3:
                    ZadaniLabel.Text =
                        $"{fronta[id].PrvniCislo} * {fronta[id].DruheCislo} = ";
                    SpatnaOdpovedLabel.Text = $"{fronta[id].UzivateluvVstup}";
                    ReseniLabel.Text = $"{fronta[id].PrvniCislo * fronta[id].DruheCislo}";
                    if (fronta[id].UzivateluvVstup == fronta[id].PrvniCislo * fronta[id].DruheCislo)
                    {
                        NadSpatnaOdpovedLabel.IsVisible = false;
                        SpatneFrame.IsVisible = false;
                    }
                    break;
                case 4:
                    ZadaniLabel.Text =
                        $"{fronta[id].PrvniCislo} ÷ {fronta[id].DruheCislo} = ";
                    SpatnaOdpovedLabel.Text = $"{fronta[id].UzivateluvVstup}";
                    ReseniLabel.Text = $"{fronta[id].PrvniCislo / fronta[id].DruheCislo}";
                    if (fronta[id].UzivateluvVstup == fronta[id].PrvniCislo / fronta[id].DruheCislo)
                    {
                        NadSpatnaOdpovedLabel.IsVisible = false;
                        SpatneFrame.IsVisible = false;
                    }
                    break;
            }
            this.fronta = fronta;
            this.frontaVse = frontaVse;
            this.potvrzeniZapisu = potvrzeniZapisu;
            ID = id;
            if (id == fronta.Count - 1)
            {
                DalsiPrikladButton.IsVisible = false;
            }
            if (id == 0)
            {
                PredchoziPrikladButton.IsVisible = false;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            SettingsModel tabulkaNastaveni = await App.Database.GetSettings();
            ReseniLabelLayout.BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
            SpatnaOdpovedLayout.BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
            ZadaniLabelFrame.BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
            if (tabulkaNastaveni.DarkMode)
            {
                BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
                ZadaniLabel.TextColor = Color.White;
                SpatnaOdpovedLabel.TextColor = Color.White;
                ReseniLabel.TextColor = Color.White;
            }
        }

        private async void PrehledButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Souhrn(frontaVse, true));
        }

        private async void DalsiPrikladButton_OnClicked(object sender, EventArgs e)
        {
            ID++;
            await Navigation.PushAsync(new RozborVysledku(ID, fronta, frontaVse, potvrzeniZapisu));
        }

        private async void PredchoziPrikladButton_OnClicked(object sender, EventArgs e)
        {
            ID--;
            await Navigation.PushAsync(new RozborVysledku(ID, fronta, frontaVse, potvrzeniZapisu));
        }
        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
            var existingPages = Navigation.NavigationStack.ToList();
            foreach(var page in existingPages)
            {
                Navigation.RemovePage(page);
            } 
        }
    }  
}