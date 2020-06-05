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
    public partial class ScitaniAOdcitani : ContentPage
    {
private byte velikostCisel = 1;
        private byte druhPrikladu;
        public ScitaniAOdcitani(byte vyber)
        {
            InitializeComponent();
            druhPrikladu = vyber;

            switch (vyber)
            {
                case 1:
                    Title = "Sčítání";
                    break;
                case 2:
                    Title = "Odečítání";
                    break;
                case 5:
                    Title = "Náhodné";
                    Info2Label.HeightRequest = 60;
                    Info2Label.Text = "Zvolte velikost čísel pro sčítání a odčítání";
                     
                    break;
            }
        }
        private void PridatButton_OnClicked(object sender, EventArgs e)
        {
           if (velikostCisel < 6) velikostCisel++;
            switch (velikostCisel)
            {
                case 1:
                    ZvolitVelikostLabel.Text = "Jednociferné";
                    break;
                case 2:
                    ZvolitVelikostLabel.Text = "Dvouciferné";
                    break;
                case 3:
                    ZvolitVelikostLabel.Text = "Trojciferné";
                    break;
                case 4:
                    ZvolitVelikostLabel.Text = "Čtyřciferné";
                    break;
                case 5:
                    ZvolitVelikostLabel.Text = "Pěticiferné";
                    break;
                case 6:
                    ZvolitVelikostLabel.Text = "Šesticiferné";
                    break;
            }                
        }
        private void UbratButton_OnClicked(object sender, EventArgs e)
        {
            if (velikostCisel > 1) velikostCisel--;
            switch (velikostCisel)
            {
                case 1:
                    ZvolitVelikostLabel.Text = "Jednociferné";
                    break;
                case 2:
                    ZvolitVelikostLabel.Text = "Dvouciferné";
                    break;
                case 3:
                    ZvolitVelikostLabel.Text = "Trojciferné";
                    break;
                case 4:
                    ZvolitVelikostLabel.Text = "Čtyřciferné";
                    break;
                case 5:
                    ZvolitVelikostLabel.Text = "Pěticiferné";
                    break;
                case 6:
                    ZvolitVelikostLabel.Text = "Šesticiferné";
                    break;
            }
        }

        private async void DalsiButton_OnClicked(object sender, EventArgs e)
        {
            switch (druhPrikladu)
            {
                case 5:
                    await Navigation.PushAsync(new DeleniANasobeni(5, velikostCisel));
                    break;
                default:
                    await Navigation.PushAsync(new PocetPrikladu(velikostCisel, druhPrikladu));
                    break;
            }
        }
    }
}