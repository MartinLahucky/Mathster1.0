using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeleniANasobeni : ContentPage
    {
        private byte druhPrikladu;
        private byte velikostScitaniAOdcitani;
        public DeleniANasobeni(byte druhPrikladu, byte velikostScitaniAOdcitani = 1)
        {
            InitializeComponent();
            this.druhPrikladu = druhPrikladu;
            this.velikostScitaniAOdcitani = velikostScitaniAOdcitani;
            
            switch (druhPrikladu)
            {
                case 3:
                    ZvolDelitelANasobitelLabel.Text = "Zvolte násobitele";
                    break;
                case 4:
                    ZvolDelitelANasobitelLabel.Text = "Zvolte dělitele";
                    break;         
                case 5:
                    Title = "Náhodné";
                    ZvolDelitelANasobitelLabel.HeightRequest = 30;
                    ZvolDelitelANasobitelLabel.Text = "Zvolte dělitele a násobitel";
                    break;
            }
        }

        private async void NizkaButton_OnClicked(object sender, EventArgs e)
        {
            switch (druhPrikladu)
            {
                case 5:
                    await Navigation.PushAsync(new PocetPrikladu(1, druhPrikladu, velikostScitaniAOdcitani));
                    break;
                default:
                    await Navigation.PushAsync(new PocetPrikladu(1, druhPrikladu));
                    break;
            }
        }

        private async void StredniButton_OnClicked(object sender, EventArgs e)
        {
            switch (druhPrikladu)
            {
                case 5:
                    await Navigation.PushAsync(new PocetPrikladu(2, druhPrikladu, velikostScitaniAOdcitani));
                    break;
                default:
                    await Navigation.PushAsync(new PocetPrikladu(2, druhPrikladu));
                    break;
            }
        }

        private async void VysokaButton_OnClicked(object sender, EventArgs e)
        {
            switch (druhPrikladu)
            {
                case 5:
                    await Navigation.PushAsync(new PocetPrikladu(3, druhPrikladu, velikostScitaniAOdcitani));
                    break;
                default:
                    await Navigation.PushAsync(new PocetPrikladu(3, druhPrikladu));
                    break;
            }
        }
    }
}