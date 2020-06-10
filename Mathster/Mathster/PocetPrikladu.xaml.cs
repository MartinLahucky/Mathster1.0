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
    public partial class PocetPrikladu : ContentPage
    {
        private int minCisel;
        private int maxCisel;
        private byte minDeleniANasobeni;
        private byte maxDeleniANasobeni;
        private byte druhPrikladu;
        private int pocetPrikladu = 1;
        private byte velikostCisel;
        private List<Priklad> fronta;
        
        public PocetPrikladu(byte velikostCisel, byte druhPrikladu, byte VelikostDeleniANasobeni)
        {
            InitializeComponent();
            this.druhPrikladu = druhPrikladu;
            
            switch (druhPrikladu)
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
                    break;
            }

            Info2Label.Text = AppResource.ZvolitPocetPrikladu;
            GenerujButton.Text = AppResource.Start;
            
            switch (velikostCisel)
            {
                case 1:
                    minCisel = 1;
                    maxCisel = 10;
                    break;
                case 2:
                    minCisel = 10;
                    maxCisel = 100;
                    break;
                case 3:
                    minCisel = 100;
                    maxCisel = 1000;
                    break;
                case 4:
                    minCisel = 1000;
                    maxCisel = 10000;
                    break;
                case 5:
                    minCisel = 10000;
                    maxCisel = 100000;
                    break;
                case 6:
                    minCisel = 100000;
                    maxCisel = 1000000;
                    break;
                default:
                    minCisel = 1;
                    maxCisel = 10;
                    break;
            }
            if (druhPrikladu == 3 || druhPrikladu == 4 || druhPrikladu == 5)
            {
                switch (VelikostDeleniANasobeni)
                {
                    case 1:
                        minDeleniANasobeni = 2;
                        maxDeleniANasobeni = 6;
                        break;
                    case 2:
                        minDeleniANasobeni = 2;
                        maxDeleniANasobeni = 11;
                        break;
                    case 3:
                        minDeleniANasobeni = 2;
                        maxDeleniANasobeni = 21;
                        break;
                    default:
                        minDeleniANasobeni = 2;
                        maxDeleniANasobeni = 6;
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

            if (druhPrikladu == 1 || druhPrikladu == 2)
            {
                for (byte i = 0; i < pocetPrikladu; i++)
                {
                    fronta.Add(new Priklad().VygenerujPriklad(i, minCisel, maxCisel, minDeleniANasobeni, maxDeleniANasobeni, druhPrikladu));
                }
            }
            else if (druhPrikladu == 3 || druhPrikladu == 4)
            {
                for (byte i = 0; i < pocetPrikladu; i++)
                {
                    fronta.Add(new Priklad().VygenerujPriklad(i, minCisel, maxCisel, minDeleniANasobeni, maxDeleniANasobeni, druhPrikladu));
                }
            }
            else
            {
                for (byte i = 0; i < pocetPrikladu; i++)
                {
                    fronta.Add(new Priklad().VygenerujNahodnyPriklad(i, minCisel, maxCisel, minDeleniANasobeni, maxDeleniANasobeni));
                }
            }
            await Navigation.PushAsync(new Priklady(0, fronta));
        }
    }
}