using System;
using System.Collections.Generic;
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
        public RozborVysledku(byte id, List<Priklad> fronta, List<Priklad> frontaVse)
        {
            InitializeComponent();
            MenuToolbarButton.IconImageSource = "round_house_white_18dp.png";
            ReseniNadLabel.Text = AppResource.SpravneReseni;
            NadSpatnaOdpovedLabel.Text = AppResource.VaseReseni;
            //SpatneLabel.Text = AppResource.Spatne;
           // PredchoziPrikladButton.Text = AppResource.Predchozi;
           // DalsiPrikladButton.Text = AppResource.Dalsi;
           // PrehledButton.Text = AppResource.Souhrn;
            Title = AppResource.Reseni;
            
            if (fronta[id].PrvniCislo >= 10000)
            {
                SpatnaOdpovedLabel.HeightRequest = 75;
                ReseniLabel.HeightRequest = 75;
            } 
            
            switch (fronta[id].DruhPrikladu)
            {
                case 1:
                    SpatnaOdpovedLabel.Text =
                        $"{fronta[id].PrvniCislo} + {fronta[id].DruheCislo} = {fronta[id].UzivateluvVstup}";
                    ReseniLabel.Text = $"{fronta[id].PrvniCislo} + {fronta[id].DruheCislo} = {(fronta[id].PrvniCislo + fronta[id].DruheCislo)}";
                    break;
                case 2:
                    SpatnaOdpovedLabel.Text =
                        $"{fronta[id].PrvniCislo} - {fronta[id].DruheCislo} = {fronta[id].UzivateluvVstup}";
                    ReseniLabel.Text = $"{fronta[id].PrvniCislo} - {fronta[id].DruheCislo} = {(fronta[id].PrvniCislo - fronta[id].DruheCislo)}";
                    break;
                case 3:
                    SpatnaOdpovedLabel.Text =
                        $"{fronta[id].PrvniCislo} * {fronta[id].DruheCislo} = {fronta[id].UzivateluvVstup}";
                    ReseniLabel.Text = $"{fronta[id].PrvniCislo} * {fronta[id].DruheCislo} = {(fronta[id].PrvniCislo * fronta[id].DruheCislo)}";
                    break;
                case 4:
                    SpatnaOdpovedLabel.Text =
                        $"{fronta[id].PrvniCislo} ÷ {fronta[id].DruheCislo} = {fronta[id].UzivateluvVstup}";
                    ReseniLabel.Text = $"{fronta[id].PrvniCislo} ÷ {fronta[id].DruheCislo} = {(fronta[id].PrvniCislo / fronta[id].DruheCislo)}";
                    break;
            }

            this.fronta = fronta;
            this.frontaVse = frontaVse;
            ID = id;
            if (id == (fronta.Count - 1))
            {
                DalsiPrikladButton.IsVisible = false;
            }
            if (id == 0)
            {
                PredchoziPrikladButton.IsVisible = false;
            }
        }

        private async void PrehledButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Souhrn(frontaVse));
        }

        private async void DalsiPrikladButton_OnClicked(object sender, EventArgs e)
        {
            ID++;
            switch (fronta[ID].DruhPrikladu)
            {
                case 1:
                    if (fronta[ID].PrvniCislo + fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta, frontaVse));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta, frontaVse));
                    }
                    break;
                case 2:
                    if (fronta[ID].PrvniCislo - fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta, frontaVse));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta, frontaVse));
                    }
                    break;
                case 3:
                    if (fronta[ID].PrvniCislo * fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta, frontaVse));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta, frontaVse));
                    }
                    break;
                case 4:
                    if (fronta[ID].PrvniCislo / fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta, frontaVse));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta, frontaVse));
                    }
                    break;
            }
        }

        private async void PredchoziPrikladButton_OnClicked(object sender, EventArgs e)
        {
            ID--;
            switch (fronta[ID].DruhPrikladu)
            {
                case 1:
                    if (fronta[ID].PrvniCislo + fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta, frontaVse));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta, frontaVse));
                    }

                    break;
                case 2:
                    if (fronta[ID].PrvniCislo - fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta, frontaVse));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta, frontaVse));
                    }

                    break;
                case 3:
                    if (fronta[ID].PrvniCislo * fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta, frontaVse));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta, frontaVse));
                    }

                    break;
                case 4:
                    if (fronta[ID].PrvniCislo / fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta, frontaVse));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta, frontaVse));
                    }

                    break;
            }
        }
        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
        }
    }  
}