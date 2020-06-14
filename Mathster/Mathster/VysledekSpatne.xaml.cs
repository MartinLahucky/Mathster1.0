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
    public partial class VysledekSpatne : ContentPage
    {
        private byte ID;
        private List<Priklad> fronta;
        public VysledekSpatne(byte id, List<Priklad> fronta)
        {
            InitializeComponent();

            ReseniNadLabel.Text = AppResource.SpravneReseni;
            NadSpatnaOdpovedLabel.Text = AppResource.VaseReseni;
            SpatneLabel.Text = AppResource.Spatne;
            DalsiPrikladButton.Text = AppResource.Dalsi;
            MenuButton.Text = AppResource.Menu;
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
            ID = id;
            if (id < (fronta.Count - 1))
            {
                MenuButton.IsVisible = false;
            }
            else
            {
                DalsiPrikladButton.IsVisible = false;
            }
        }

        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
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
    }
}