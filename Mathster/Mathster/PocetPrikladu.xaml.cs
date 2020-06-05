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
    public partial class PocetPrikladu : ContentPage
    {
        private int min;
        private int max;
        private byte druhPrikladu;
        private int pocetPrikladu = 1;
        private List<Priklad> fronta;
        
        public PocetPrikladu(byte velikostCisel, byte druhPrikladu)
        {
            InitializeComponent();
            this.druhPrikladu = druhPrikladu;
            
            if (druhPrikladu == 1 || druhPrikladu == 2)
            {
                switch (velikostCisel)
                {
                    case 1:
                        min = 0;
                        max = 10;
                        break;
                    case 2:
                        min = 10;
                        max = 100;
                        break;
                    case 3:
                        min = 100;
                        max = 1000;
                        break;
                    case 4:
                        min = 1000;
                        max = 10000;
                        break;
                    case 5:
                        min = 10000;
                        max = 100000;
                        break;
                    case 6:
                        max = 1000000;
                        break;
                    default:
                        min = 1;
                        max = 10;
                        break;
                }
            }
            else
            {
                switch (velikostCisel)
                {
                    case 1:
                        min = 2;
                        max = 6;
                        break;
                    case 2:
                        min = 2;
                        max = 11;
                        break;
                    case 3:
                        min = 2;
                        max = 21;
                        break;
                    default:
                        min = 2;
                        max = 6;
                        break;
                }
            }
        }

        private void Pridat2Button_OnClicked(object sender, EventArgs e)
        {
            if (pocetPrikladu < 20) pocetPrikladu++;
            ZvolitPocetLabel.Text = $"{pocetPrikladu}";
        }

        private void Ubrat2Button_OnClicked(object sender, EventArgs e)
        {
            if (pocetPrikladu > 1) pocetPrikladu--;
            ZvolitPocetLabel.Text = $"{pocetPrikladu}";
        }

        private async void GenerujButton_OnClicked(object sender, EventArgs e)
        {
            fronta = new List<Priklad>();
            for (byte i = 0; i < pocetPrikladu; i++)
            {
                fronta.Add(new Priklad().VygenerujPriklad(i, min, max, druhPrikladu));
            }
            await Navigation.PushAsync(new Priklady(0, fronta));
            // TODO Zapnou do full bety
            // Vynuluvání navigace
            var existingPages = Navigation.NavigationStack.ToList();
            foreach(var page in existingPages)
            {
                Navigation.RemovePage(page);
            }
        }
    }
}