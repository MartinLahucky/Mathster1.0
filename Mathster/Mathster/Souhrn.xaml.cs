using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mathster.Resources.Localization;
using Mathster.Resources.Database_Models;
using Mathster.Resources.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Souhrn : ContentPage
    {
        private List<Priklad> fronta;
        private DBModel zapis;
        private SettingsModel tabulkaNastaveni;
        private bool potvrzeniZapisu;
        private List<Priklad> prikladyDobre;
        private List<Priklad> prikladySpatne;
        public Souhrn(List<Priklad> fronta, bool potvrzeniZapisu)
        {
            InitializeComponent();
            MenuToolbarButton.IconImageSource = "round_house_white_18dp.png";
            this.fronta = fronta;
            this.potvrzeniZapisu = potvrzeniZapisu;
            int experienceCount = 0;
            
            Title = AppResource.Souhrn;
            MenuButton.Text = AppResource.Menu;
            NadpisSouhrnLabel.Text = AppResource.Vysledky;

            Task task = Task.Run(async () =>
            {
                zapis = await App.Database.GetTable();
                tabulkaNastaveni = await App.Database.GetSettings();
            });
            Task.WaitAll(task);
            
            List<Vysledek> priklady = new List<Vysledek>();
            List<Priklad> prikladyDobre = new List<Priklad>();
            List<Priklad> prikladySpatne = new List<Priklad>();
            byte pocitadloSpravne = 0;
            byte pocitadloSpatne = 0;
            
            foreach (var priklad in fronta)
            {
                bool spravne = true;
                priklady.Add(new Vysledek(priklad.VratPriklad(), tabulkaNastaveni));
                if (priklad.PrvniCislo >= 1000 || priklad.UzivateluvVstup >= 1000)
                {
                    VysledkyList.RowHeight = 80;
                }
                if (priklad.VratVysledek() == priklad.UzivateluvVstup)
                {
                    pocitadloSpravne++;
                    zapis.AddGoodStats(priklad.DruhPrikladu, zapis);
                    priklady[priklad.Id].StatusPrikladu = "dobre_ikona.png";
                    prikladyDobre.Add(priklad);
                }
                else
                {
                    spravne = false;
                    pocitadloSpatne++;
                    zapis.AddStats(priklad.DruhPrikladu, zapis);
                    priklady[priklad.Id].StatusPrikladu = "spatne_ikona.png";
                    prikladySpatne.Add(priklad);
                }
                experienceCount += priklad.GetExperience(spravne);
            }
            this.prikladyDobre = prikladyDobre;
            this.prikladySpatne = prikladySpatne;
            zapis.Experience += experienceCount;
            VysledkyList.ItemsSource = priklady;
            DobrePocetButton.Text = pocitadloSpravne.ToString();
            SpatnePocetButton.Text = pocitadloSpatne.ToString();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
            DobrePocetButton.BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
            SpatnePocetButton.BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
            if (tabulkaNastaveni.DarkMode)
            {
                NadpisSouhrnLabel.TextColor = Color.FromHex("#FFFFFF");
                DobrePocetButton.TextColor = Color.FromHex("#FFFFFF");
                SpatnePocetButton.TextColor = Color.FromHex("#FFFFFF");
            }
        }
        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            if (!potvrzeniZapisu)
            {
                await App.Database.UpdateTable(zapis);
                potvrzeniZapisu = true;
            }
        }
        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
            var existingPages = Navigation.NavigationStack.ToList();
            foreach(var page in existingPages)
            {
                Navigation.RemovePage(page);
            }   
        }

        private async void VysledkyList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            byte selectedItem = byte.Parse(e.ItemIndex.ToString());
            await Navigation.PushAsync(new RozborVysledku(selectedItem, fronta, fronta, potvrzeniZapisu));
            if (sender is ListView lv) lv.SelectedItem = null;
        }
        private async void DobreButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new RozborVysledku(0, prikladyDobre, fronta, potvrzeniZapisu));
            }
            catch
            {
                await DisplayAlert(AppResource.Upozorneni, AppResource.UpozorneniVseSpatne, AppResource.Ok);
            }
        }
        private async void SpatneButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new RozborVysledku(0, prikladySpatne, fronta, potvrzeniZapisu));
            }
            catch
            {
                await DisplayAlert(AppResource.Upozorneni, AppResource.UpozorneniVseDobre, AppResource.Ok);
            }
        }
    }

    public class Vysledek
    {
        private string textovaPodobaPrikladu;
        private string statusPrikladu;
        private Color barvaCellu;
        private Color barvaTextu;

        public Vysledek(string textovaPodobaPrikladu, SettingsModel tabulkaNataveni)
        {
            this.textovaPodobaPrikladu = textovaPodobaPrikladu;
            barvaCellu = Color.FromHex(tabulkaNataveni.BackgroundHex);
            if (tabulkaNataveni.DarkMode)
            {
                barvaTextu = Color.FromHex("#FFFFFF");
            }
            else
            {
                barvaTextu = Color.Black;
            }
        }
        public string TextovaPodobaPrikladu
        {
            get => textovaPodobaPrikladu;
            set => textovaPodobaPrikladu = value;
        }
        public string StatusPrikladu
        {
            get => statusPrikladu;
            set => statusPrikladu = value;
        }
        public Color BarvaCellu
        {
            get => barvaCellu;
            set => barvaCellu = value;
        }
        public Color BarvaTextu
        {
            get => barvaTextu;
            set => barvaTextu = value;
        }
    }
}