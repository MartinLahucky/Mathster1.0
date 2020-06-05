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
        public DeleniANasobeni(byte druhPrikladu)
        {
            InitializeComponent();
            this.druhPrikladu = druhPrikladu;

            switch (druhPrikladu)
            {
                case 3:
                    ZvolDelitelANasobitelLabel.Text = "Zvolte násobitele";
                    break;
                case 4:
                    ZvolDelitelANasobitelLabel.Text = "Zvolte dělitele";
                    break;                    
            }
        }

        private async void NizkaButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PocetPrikladu(1, druhPrikladu));
        }

        private async void StredniButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PocetPrikladu(2, druhPrikladu));
        }

        private async void VysokaButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PocetPrikladu(3, druhPrikladu));
        }
    }
}