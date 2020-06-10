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
    public partial class DeleniANasobeni : ContentPage
    {
        private byte druhPrikladu;
        private byte velikostCisel;
        public DeleniANasobeni(byte druhPrikladu, byte velikostCisel)
        {
            InitializeComponent();
            this.druhPrikladu = druhPrikladu;
            this.velikostCisel = velikostCisel;

            NizkaButton.Text = AppResource.DoPeti;
            StredniButton.Text = AppResource.DoDeseti;
            VysokaButton.Text = AppResource.DoDvaceti;
            
            if (velikostCisel == 1 && druhPrikladu == 4)
            {
                VysokaButton.IsVisible = false;
                StredniButton.IsVisible = false;
            }
            else if (velikostCisel == 1 && druhPrikladu == 5)
            {
                VysokaButton.IsVisible = false;
            }
            switch (druhPrikladu)
            {
                case 3:
                    Title = AppResource.Nasobeni;
                    ZvolDelitelANasobitelLabel.Text = AppResource.ZvolteNasobitele;
                    break;
                case 4:
                    Title = AppResource.Deleni;
                    ZvolDelitelANasobitelLabel.Text = AppResource.ZvolteDelitele;
                    break;
                case 5:
                    Title = AppResource.Nahodne;
                    ZvolDelitelANasobitelLabel.HeightRequest = 50;
                    ZvolDelitelANasobitelLabel.Text = AppResource.ZvolitNasobitelADelitel;
                    break;
            }
        }

        private async void NizkaButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PocetPrikladu(velikostCisel, druhPrikladu, 1));
        }

        private async void StredniButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PocetPrikladu(velikostCisel, druhPrikladu, 2));
        }

        private async void VysokaButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PocetPrikladu(velikostCisel, druhPrikladu, 3));
        }
    }
}