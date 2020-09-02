using System;
using Mathster.Helpers.Model;
using Mathster.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Statistiky : ContentPage
    {
        public Statistiky()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            DBModel tabulka = await App.Database.GetTable();

            if (tabulka.CelkemScitani > tabulka.CelkemOdcitani && tabulka.CelkemScitani > tabulka.CelkemNasobeni && tabulka.CelkemScitani > tabulka.CelkemDeleni)
            {
                tabulka.DruhNejcastejiPocitanychPrikladu = AppResource.Scitani;
            }
            else if (tabulka.CelkemOdcitani > tabulka.CelkemScitani && tabulka.CelkemOdcitani > tabulka.CelkemNasobeni && tabulka.CelkemOdcitani > tabulka.CelkemDeleni)
            {
                tabulka.DruhNejcastejiPocitanychPrikladu = AppResource.Odecitani;
            }
            else if (tabulka.CelkemNasobeni > tabulka.CelkemScitani && tabulka.CelkemNasobeni > tabulka.CelkemOdcitani && tabulka.CelkemNasobeni > tabulka.CelkemDeleni)
            {
                tabulka.DruhNejcastejiPocitanychPrikladu = AppResource.Nasobeni;
            }
            else if (tabulka.CelkemDeleni > tabulka.CelkemScitani && tabulka.CelkemDeleni > tabulka.CelkemOdcitani && tabulka.CelkemDeleni > tabulka.CelkemNasobeni)
            {
                tabulka.DruhNejcastejiPocitanychPrikladu = AppResource.Deleni;
            }
            Label1.Text = $"{AppResource.CelkemPrikladu} {tabulka.CelkemPrikladu.ToString()}";
            Label2.Text = $"{AppResource.CelkemPrikladuDobre} {tabulka.CelkemPrikladuDobre.ToString()}";
            Label3.Text = $"{AppResource.CelkemScitani} {tabulka.CelkemScitani.ToString()}";
            Label4.Text = $"{AppResource.CelkemScitaniSpravne} {tabulka.CelkemScitaniSpravne.ToString()}";
            Label5.Text = $"{AppResource.CelkemOdcitani} {tabulka.CelkemOdcitani.ToString()}";
            Label6.Text = $"{AppResource.CelkemOdcitaniSpravne} {tabulka.CelkemOdcitaniSpravne.ToString()}";
            Label7.Text = $"{AppResource.CelkemNasobeni} {tabulka.CelkemNasobeni.ToString()}";
            Label8.Text = $"{AppResource.CelkemNasobeniSpravne} {tabulka.CelkemNasobeniSpravne.ToString()}";
            Label9.Text = $"{AppResource.CelkemDeleni} {tabulka.CelkemDeleni.ToString()}";
            Label10.Text = $"{AppResource.CelkemDeleniSpravne} {tabulka.CelkemDeleniSpravne.ToString()}";
            Label11.Text = $"{AppResource.DruhNejcastejiPocitanychPrikladu} {tabulka.DruhNejcastejiPocitanychPrikladu}";
            ResetStatsButton.Text = AppResource.StatReset;

            if ((tabulka.CelkemScitani - tabulka.CelkemScitaniSpravne) > tabulka.CelkemScitani / 2)
            {
                Frame4.BackgroundColor = Color.FromHex("#ffce85");
            }
            if ((tabulka.CelkemOdcitani - tabulka.CelkemOdcitaniSpravne) > tabulka.CelkemOdcitani / 2)
            {
                Frame6.BackgroundColor = Color.FromHex("#ffce85");
            }
            if ((tabulka.CelkemNasobeni - tabulka.CelkemNasobeniSpravne) > tabulka.CelkemNasobeni / 2)
            {
                Frame8.BackgroundColor = Color.FromHex("#ffce85");
            }
            if ((tabulka.CelkemDeleni - tabulka.CelkemDeleniSpravne) > tabulka.CelkemDeleni / 2)
            {
                Frame10.BackgroundColor = Color.FromHex("#ffce85");
            }
        }

        private async void ResetStatsButton_OnClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(AppResource.Upozorneni, AppResource.ResetMessage, AppResource.Ano, AppResource.Ne))
            {
                DBModel tabulkaReset = new DBModel
                {
                    Experience = 0,
                    CelkemPrikladu = 0,
                    CelkemScitani = 0,
                    CelkemScitaniSpravne = 0,
                    CelkemOdcitani = 0,
                    CelkemOdcitaniSpravne = 0,
                    CelkemNasobeni = 0,
                    CelkemNasobeniSpravne = 0,
                    CelkemDeleni = 0,
                    CelkemDeleniSpravne = 0,
                    CelkemPrikladuDobre = 0,
                    Nachozeno = 0,
                    DruhNejcastejiPocitanychPrikladu = String.Empty,
                };
                Label1.Text = $"{AppResource.CelkemPrikladu} {tabulkaReset.CelkemPrikladu.ToString()}";
                Label2.Text = $"{AppResource.CelkemPrikladuDobre} {tabulkaReset.CelkemPrikladuDobre.ToString()}";
                Label3.Text = $"{AppResource.CelkemScitani} {tabulkaReset.CelkemScitani.ToString()}";
                Label4.Text = $"{AppResource.CelkemScitaniSpravne} {tabulkaReset.CelkemScitaniSpravne.ToString()}";
                Label5.Text = $"{AppResource.CelkemOdcitani} {tabulkaReset.CelkemOdcitani.ToString()}";
                Label6.Text = $"{AppResource.CelkemOdcitaniSpravne} {tabulkaReset.CelkemOdcitaniSpravne.ToString()}";
                Label7.Text = $"{AppResource.CelkemNasobeni} {tabulkaReset.CelkemNasobeni.ToString()}";
                Label8.Text = $"{AppResource.CelkemNasobeniSpravne} {tabulkaReset.CelkemNasobeniSpravne.ToString()}";
                Label9.Text = $"{AppResource.CelkemDeleni} {tabulkaReset.CelkemDeleni.ToString()}";
                Label10.Text = $"{AppResource.CelkemDeleniSpravne} {tabulkaReset.CelkemDeleniSpravne.ToString()}";
                Label11.Text = $"{AppResource.DruhNejcastejiPocitanychPrikladu} {tabulkaReset.DruhNejcastejiPocitanychPrikladu}";
                await App.Database.UpdateTable(tabulkaReset);
            }
        }
    }
}