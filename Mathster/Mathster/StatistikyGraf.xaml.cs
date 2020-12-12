using System;
using Mathster.Helpers.Custom_UI;
using Mathster.Helpers.Model;
using Mathster.Helpers.Resources;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatistikyGraf : ContentPage
    {
        private Circles graf;
        public StatistikyGraf(string Nadpis, int hodnotaSpravne, int hodnotaCelkem)
        {
            InitializeComponent();
            Title = AppResource.Statistiky;
            NadpisLabel.Text = Nadpis;
            ResetStatsButton.Text = AppResource.StatReset;
            celkovyPocetLabel.Text = hodnotaCelkem.ToString();
            //Zobrazení legendy
            HodnotaLabel1.Text = hodnotaSpravne.ToString();
            HodnotaLabel2.Text = (hodnotaCelkem - hodnotaSpravne).ToString();
            frame3.IsVisible = false;
            frame4.IsVisible = false;
            PopisImage1.IsVisible = true;
            PopisImage2.IsVisible = true;
            PopisLabel1.IsVisible = false;
            PopisLabel2.IsVisible = false;
            PopisLabel3.IsVisible = false;
            PopisLabel4.IsVisible = false;
            HodnotaLabel3.IsVisible = false;
            HodnotaLabel4.IsVisible = false;
            //SkiaSharp
            if (hodnotaCelkem == 0) hodnotaCelkem = 1;
            graf = new Circles(180, (info) => new SKPoint((float)info.Width / 2, (float)info.Height / 2));
            graf.DrawFullProgressBar(SkCanvasView1, "#7F7FFD", "#FCA54D", 40f, (float)hodnotaSpravne / (float)(hodnotaCelkem) * 100f, "#C9FF50");
        }

        public StatistikyGraf(string Nadpis, int Scitani, int Odecitani, int Nasobeni, int Deleni)
        {
            InitializeComponent();
            Title = AppResource.Statistiky;
            NadpisLabel.Text = Nadpis;
            ResetStatsButton.Text = AppResource.StatReset;
            celkovyPocetLabel.Text = (Scitani + Odecitani + Nasobeni + Deleni).ToString();
            //Zobrazení legendy
            HodnotaLabel1.Text = Scitani.ToString();
            HodnotaLabel2.Text = Odecitani.ToString();
            HodnotaLabel3.Text = Nasobeni.ToString();
            HodnotaLabel4.Text = Deleni.ToString();
            PopisImage1.IsVisible = false;
            PopisImage2.IsVisible = false;
            PopisLabel1.IsVisible = true;
            PopisLabel2.IsVisible = true;
            PopisLabel3.IsVisible = true;
            PopisLabel4.IsVisible = true;
            HodnotaLabel3.IsVisible = true;
            HodnotaLabel4.IsVisible = true;
            //SkiaSharp
            graf = new Circles(180, (info) => new SKPoint((float)info.Width / 2, (float)info.Height / 2));
            graf.DrawChart(SkCanvasView1, "#7F7FFD", "#FCA54D", 40f, Nasobeni, Deleni, Odecitani, Scitani + Odecitani + Nasobeni + Deleni, "#C9FF50", "#262630", "#FFFFFF");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            SettingsModel tabulkaNastaveni = await App.Database.GetSettings();
            BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
        }

        private async void ResetStatsButton_OnClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(AppResource.Upozorneni, AppResource.ResetMessage, AppResource.Ano, AppResource.Ne))
            {
                DBModel tabulka = await App.Database.GetTable();
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
                    CelkemPrikladuSpravne = 0,
                    Jmeno = tabulka.Jmeno,
                };
                await App.Database.UpdateTable(tabulkaReset);
                await Navigation.PushAsync(new Menu());
            }
        }
    }
}