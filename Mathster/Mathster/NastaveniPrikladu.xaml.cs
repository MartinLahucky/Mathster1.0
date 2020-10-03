using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;


namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NastaveniPrikladu : ContentPage
    {

        //  proměnné určující nastavení:
        //  (jsou nastavené na default hodnoty - ty které se zobrazí při zapnutí aplikace)
        private int pocetPrikladu = 5;
        private int maxPocetPrikladu = 20;
        //
        private int pocetCifer = 2;
        private int maxPocetCifer = 6;
        //
        private int velikostDelitele = 2;
        private int maxVelikostDelitele = 6;

        public NastaveniPrikladu()
        {
            InitializeComponent();

            pocetPrikladuSlider.Value = pocetPrikladu;
            pocetPrikladuSlider.Maximum = maxPocetPrikladu;
            pocetPrikladuLabel.Text = pocetPrikladu.ToString();
            //
            pocetCiferSlider.Value = pocetCifer;
            pocetCiferSlider.Maximum = maxPocetCifer;
            pocetCiferLabel.Text = pocetCifer.ToString();
            //
            velikostDeliteleSlider.Value = velikostDelitele;
            velikostDeliteleSlider.Maximum = maxVelikostDelitele;
            velikostDeliteleLabel.Text = velikostDelitele.ToString();
        }

        private void pocetPrikladu_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            pocetPrikladu = (int)pocetPrikladuSlider.Value;
            pocetPrikladuLabel.Text = pocetPrikladu.ToString();
        }

        private void pocetCiferSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            pocetCifer = (int)pocetCiferSlider.Value;
            pocetCiferLabel.Text = pocetCifer.ToString();
        }

        private void velikostDeliteleSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            velikostDelitele = (int)velikostDeliteleSlider.Value;    
            velikostDeliteleLabel.Text = velikostDelitele.ToString();
        }
    }
}     