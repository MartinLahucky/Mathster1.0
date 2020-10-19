﻿using System;
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

        private Circles graf1;
        private Circles graf2;
        private Circles graf3;
        private int scitani, odcitani, nasobeni, deleni, celkemDobre, celkemSpatne; // tady jsou statistiky, které jsou potom nutné 
        public StatistikyGraf()
        {
            scitani = 20;
            odcitani = 30;
            nasobeni = 25;
            deleni = 25;
            celkemDobre = 45;
            celkemSpatne = 55;

            InitializeComponent();
            Title = AppResource.Statistiky;
            ResetStatsButton.Text = AppResource.StatReset;
            MenuToolbarButton.IconImageSource = "round_house_white_18dp.png";

            // to-do pro řehoře:
            // upravit design tak aby nevypadal shit
            // zkontrolovat jestli fungujou hodnoty (a i ten ostatní shit)


            //SkiaSharp
            graf1 = new Circles(180, (info) => new SKPoint((float)info.Width / 2, (float)info.Height / 2));
            graf1.DrawFullProgressBar(SkCanvasView1, "#ffffff", "#000000", 30f, (float)celkemDobre / (float)(celkemSpatne + celkemDobre) * 100f, "#bbbbbb");
        }

        private void TemporarySwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (TemporarySwitch.IsToggled)
            {
                NadpisLabel.Text = "dvě hodnoty";
                PopisImage1.IsVisible = true;
                PopisImage2.IsVisible = true;
                PopisLabel1.IsVisible = false;
                PopisLabel2.IsVisible = false;
                PopisLabel3.IsVisible = false;
                PopisLabel4.IsVisible = false;
                HodnotaLabel3.IsVisible = false;
                HodnotaLabel4.IsVisible = false;
                
            }
            else
            {
                NadpisLabel.Text = "čtyři hodnoty";
                PopisImage1.IsVisible = false;
                PopisImage2.IsVisible = false;
                PopisLabel1.IsVisible = true;
                PopisLabel2.IsVisible = true;
                PopisLabel3.IsVisible = true;
                PopisLabel4.IsVisible = true;
                HodnotaLabel3.IsVisible = true;
                HodnotaLabel4.IsVisible = true;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            SettingsModel tabulkaNastaveni = await App.Database.GetSettings();
            BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
            DBModel tabulka = await App.Database.GetTable();

            




            //if (tabulka.CelkemScitani > tabulka.CelkemOdcitani && tabulka.CelkemScitani > tabulka.CelkemNasobeni && tabulka.CelkemScitani > tabulka.CelkemDeleni)
            //{
            //    tabulka.DruhNejcastejiPocitanychPrikladu = AppResource.Scitani;
            //}
            //else if (tabulka.CelkemOdcitani > tabulka.CelkemScitani && tabulka.CelkemOdcitani > tabulka.CelkemNasobeni && tabulka.CelkemOdcitani > tabulka.CelkemDeleni)
            //{
            //    tabulka.DruhNejcastejiPocitanychPrikladu = AppResource.Odecitani;
            //}
            //else if (tabulka.CelkemNasobeni > tabulka.CelkemScitani && tabulka.CelkemNasobeni > tabulka.CelkemOdcitani && tabulka.CelkemNasobeni > tabulka.CelkemDeleni)
            //{
            //    tabulka.DruhNejcastejiPocitanychPrikladu = AppResource.Nasobeni;
            //}
            //else if (tabulka.CelkemDeleni > tabulka.CelkemScitani && tabulka.CelkemDeleni > tabulka.CelkemOdcitani && tabulka.CelkemDeleni > tabulka.CelkemNasobeni)
            //{
            //    tabulka.DruhNejcastejiPocitanychPrikladu = AppResource.Deleni;
            //}
            //else
            //{
            //    tabulka.DruhNejcastejiPocitanychPrikladu = AppResource.NelzeUrcit;
            //}
            
            //List<ChartEntry> entries = new List<ChartEntry>
            //{
            //    new ChartEntry(tabulka.CelkemPrikladu)
            //    {
            //        Label = AppResource.CelkemPrikladu,
            //        ValueLabel = tabulka.CelkemPrikladu.ToString(),
            //        Color = SKColor.Parse("#FCA54D")
            //    },
            //    new ChartEntry(tabulka.CelkemPrikladuDobre)
            //    {
            //        Label = AppResource.CelkemPrikladuDobre,
            //        ValueLabel = tabulka.CelkemPrikladuDobre.ToString(),
            //        Color = SKColor.Parse("#C9FF50"),
            //    },
                
            //    // new ChartEntry()
            //    // {
            //    //     Label = ,
            //    //     ValueLabel = ,
            //    //     Color = SKColor.Parse(),
            //    // },
                
            //};
            
            //ChartView.Chart = new DonutChart() { Entries = entries};
            
            //// Label3.Text = $"{AppResource.CelkemScitani} {tabulka.CelkemScitani.ToString()}";
            //// Label4.Text = $"{AppResource.CelkemScitaniSpravne} {tabulka.CelkemScitaniSpravne.ToString()}";
            //// Label5.Text = $"{AppResource.CelkemOdcitani} {tabulka.CelkemOdcitani.ToString()}";
            //// Label6.Text = $"{AppResource.CelkemOdcitaniSpravne} {tabulka.CelkemOdcitaniSpravne.ToString()}";
            //// Label7.Text = $"{AppResource.CelkemNasobeni} {tabulka.CelkemNasobeni.ToString()}";
            //// Label8.Text = $"{AppResource.CelkemNasobeniSpravne} {tabulka.CelkemNasobeniSpravne.ToString()}";
            //// Label9.Text = $"{AppResource.CelkemDeleni} {tabulka.CelkemDeleni.ToString()}";
            //// Label10.Text = $"{AppResource.CelkemDeleniSpravne} {tabulka.CelkemDeleniSpravne.ToString()}";
            //// Label11.Text = $"{AppResource.DruhNejcastejiPocitanychPrikladu} {tabulka.DruhNejcastejiPocitanychPrikladu}";
            
            
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
                    CelkemPrikladuDobre = 0,
                    Nachozeno = 0,
                    Jmeno = tabulka.Jmeno,
                    DruhNejcastejiPocitanychPrikladu = String.Empty,
                };
                // Label1.Text = $"{AppResource.CelkemPrikladu} {tabulkaReset.CelkemPrikladu.ToString()}";
                // Label2.Text = $"{AppResource.CelkemPrikladuDobre} {tabulkaReset.CelkemPrikladuDobre.ToString()}";
                // Label3.Text = $"{AppResource.CelkemScitani} {tabulkaReset.CelkemScitani.ToString()}";
                // Label4.Text = $"{AppResource.CelkemScitaniSpravne} {tabulkaReset.CelkemScitaniSpravne.ToString()}";
                // Label5.Text = $"{AppResource.CelkemOdcitani} {tabulkaReset.CelkemOdcitani.ToString()}";
                // Label6.Text = $"{AppResource.CelkemOdcitaniSpravne} {tabulkaReset.CelkemOdcitaniSpravne.ToString()}";
                // Label7.Text = $"{AppResource.CelkemNasobeni} {tabulkaReset.CelkemNasobeni.ToString()}";
                // Label8.Text = $"{AppResource.CelkemNasobeniSpravne} {tabulkaReset.CelkemNasobeniSpravne.ToString()}";
                // Label9.Text = $"{AppResource.CelkemDeleni} {tabulkaReset.CelkemDeleni.ToString()}";
                // Label10.Text = $"{AppResource.CelkemDeleniSpravne} {tabulkaReset.CelkemDeleniSpravne.ToString()}";
                // Label11.Text = $"{AppResource.DruhNejcastejiPocitanychPrikladu} {AppResource.NelzeUrcit}";
                await App.Database.UpdateTable(tabulkaReset);
            }
        }
        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
        }


    }
}