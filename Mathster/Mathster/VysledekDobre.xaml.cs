using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathster.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VysledekDobre : ContentPage
    {
        private byte ID;
        private List<Priklad> fronta;
        public VysledekDobre(byte id, List<Priklad> fronta)
        {
            InitializeComponent();

            SpravneLabel.Text = AppResource.Spravne;
            ReseniNadLabel.Text = AppResource.SpravneReseni;
            PredchoziPrikladButton.Text = AppResource.Predchozi;
            DalsiPrikladButton.Text = AppResource.Dalsi;
            PrehledButton.Text = AppResource.Souhrn;
            Title = AppResource.Reseni;

            if (fronta[id].PrvniCislo >= 10000)
            {
                ReseniLabel.HeightRequest = 75;
            } 
            
            switch (fronta[id].DruhPrikladu)
            {
                case 1:
                    ReseniLabel.Text = $"{fronta[id].PrvniCislo} + {fronta[id].DruheCislo} = {(fronta[id].PrvniCislo + fronta[id].DruheCislo)}";
                    break;
                case 2:
                    ReseniLabel.Text = $"{fronta[id].PrvniCislo} - {fronta[id].DruheCislo} = {(fronta[id].PrvniCislo - fronta[id].DruheCislo)}";
                    break;
                case 3:
                    ReseniLabel.Text = $"{fronta[id].PrvniCislo} * {fronta[id].DruheCislo} = {(fronta[id].PrvniCislo * fronta[id].DruheCislo)}";
                    break;
                case 4:
                    ReseniLabel.Text = $"{fronta[id].PrvniCislo} ÷ {fronta[id].DruheCislo} = {(fronta[id].PrvniCislo / fronta[id].DruheCislo)}";
                    break;
            }

            this.fronta = fronta;
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

        private async void DalsiPrikladButton_OnClicked(object sender, EventArgs e)
        {
            ID++;
            switch (fronta[ID].DruhPrikladu)
            {
                case 1:
                    if (fronta[ID].PrvniCislo + fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta));
                    }
                    break;
                case 2:
                    if (fronta[ID].PrvniCislo - fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta));
                    }
                    break;
                case 3:
                    if (fronta[ID].PrvniCislo * fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta));
                    }
                    break;
                case 4:
                    if (fronta[ID].PrvniCislo / fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta));
                    }
                    break;
            }
        }


        private async void PrehledButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Souhrn(fronta));
        }

        private async void PredchoziPrikladButton_OnClicked(object sender, EventArgs e)
        {
            ID--;
            switch (fronta[ID].DruhPrikladu)
            {
                case 1:
                    if (fronta[ID].PrvniCislo + fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta));
                    }
                    break;
                case 2:
                    if (fronta[ID].PrvniCislo - fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta));
                    }
                    break;
                case 3:
                    if (fronta[ID].PrvniCislo * fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta));
                    }
                    break;
                case 4:
                    if (fronta[ID].PrvniCislo / fronta[ID].DruheCislo == fronta[ID].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(ID, fronta));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(ID, fronta));
                    }
                    break;
            }
        }
    }
}