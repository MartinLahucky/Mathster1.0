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
    public partial class VelikostCisel : ContentPage
    {
        private byte velikostCisel = 1;
        private byte druhPrikladu;
        public VelikostCisel(byte vyber)
        {
            InitializeComponent();
            druhPrikladu = vyber;
            ZvolitVelikostLabel.Text = AppResource.Jednociferne;
            Info2Label.Text = AppResource.VelikostCisel;
            DalsiButton.Text = AppResource.Dalsi;
            
            switch (vyber)
            {
                case 1:
                    Title = AppResource.Scitani;
                    break;
                case 2:
                    Title = AppResource.Odecitani;
                    break;
                case 3:
                    Title = AppResource.Nasobeni;
                    break;
                case 4:
                    Title = AppResource.Deleni;
                    break;
                case 5:
                    Title = AppResource.Nahodne;
                    Info2Label.HeightRequest = 80;
                    Info2Label.Text = AppResource.VelikostVsechPrvnichCisel;
                    break;
            }
        }
        private void PridatButton_OnClicked(object sender, EventArgs e)
        {
           if (velikostCisel < 6) velikostCisel++;
            switch (velikostCisel)
            {
                case 1:
                    ZvolitVelikostLabel.Text = AppResource.Jednociferne;
                    break;
                case 2:
                    ZvolitVelikostLabel.Text = AppResource.Dvouciferne;
                    break;
                case 3:
                    ZvolitVelikostLabel.Text = AppResource.Trojciferne;
                    break;
                case 4:
                    ZvolitVelikostLabel.Text = AppResource.Ctyrciferne;
                    break;
                case 5:
                    ZvolitVelikostLabel.Text = AppResource.Peticiferne;
                    break;
                case 6:
                    ZvolitVelikostLabel.Text = AppResource.Seticiferne;
                    break;
            }                
        }
        private void UbratButton_OnClicked(object sender, EventArgs e)
        {
            if (velikostCisel > 1) velikostCisel--;
            switch (velikostCisel)
            {
                case 1:
                    ZvolitVelikostLabel.Text = AppResource.Jednociferne;
                    break;
                case 2:
                    ZvolitVelikostLabel.Text = AppResource.Dvouciferne;
                    break;
                case 3:
                    ZvolitVelikostLabel.Text = AppResource.Trojciferne;
                    break;
                case 4:
                    ZvolitVelikostLabel.Text = AppResource.Ctyrciferne;
                    break;
                case 5:
                    ZvolitVelikostLabel.Text = AppResource.Peticiferne;
                    break;
                case 6:
                    ZvolitVelikostLabel.Text = AppResource.Seticiferne;
                    break;
            }
        }

        private async void DalsiButton_OnClicked(object sender, EventArgs e)
        {
            if (druhPrikladu == 1 || druhPrikladu == 2)
            {
                await Navigation.PushAsync(new PocetPrikladu(velikostCisel, druhPrikladu, velikostCisel));
            }
            else
            {
                await Navigation.PushAsync(new DeleniANasobeni(druhPrikladu, velikostCisel));
            }
        }
    }
}