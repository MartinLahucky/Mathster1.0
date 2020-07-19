using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mathster.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Souhrn : ContentPage
    {
        private List<Priklad> fronta;
        public Souhrn(List<Priklad> fronta)
        {
            InitializeComponent();
            this.fronta = fronta;
            
            Title = AppResource.Souhrn;
            NadDobreLabel.Text = AppResource.Spravne;
            NadSpatneLabel.Text = AppResource.Spatne;
            MenuButton.Text = AppResource.Menu;
            NadVysledkyLabel.Text = AppResource.Vysledky;
            
            List<Vysledek> priklady = new List<Vysledek>();

            byte pocitadloSpravne = 0;
            byte pocitadloSpatne = 0;
            foreach (var priklad in fronta)
            {
                priklady.Add(new Vysledek(priklad.VratPriklad()));
                if (priklad.PrvniCislo >= 10000 || priklad.UzivateluvVstup >= 10000)
                {
                    VysledkyList.RowHeight = 80;
                }
                switch (priklad.DruhPrikladu)
                {
                    case 1:
                        if (priklad.PrvniCislo + priklad.DruheCislo == priklad.UzivateluvVstup)
                        {
                            pocitadloSpravne++;
                        }
                        else
                        {
                            priklady[priklad.Id].BarvaCellu = Color.FromHex("#FFEDBD");
                            pocitadloSpatne++;
                        }
                        break;
                    case 2:
                        if (priklad.PrvniCislo - priklad.DruheCislo == priklad.UzivateluvVstup)
                        {
                            pocitadloSpravne++;
                        }
                        else
                        {
                            priklady[priklad.Id].BarvaCellu = Color.FromHex("#FFEDBD");
                            pocitadloSpatne++;
                        }
                        break;
                    case 3:
                        if (priklad.PrvniCislo * priklad.DruheCislo == priklad.UzivateluvVstup)
                        {
                            pocitadloSpravne++;
                        }
                        else
                        {
                            priklady[priklad.Id].BarvaCellu = Color.FromHex("#FFEDBD");
                            pocitadloSpatne++;
                        }
                        break;
                    case 4:
                        if (priklad.PrvniCislo / priklad.DruheCislo == priklad.UzivateluvVstup)
                        {
                            pocitadloSpravne++;
                        }
                        else
                        {
                            priklady[priklad.Id].BarvaCellu = Color.FromHex("#FFEDBD");
                            pocitadloSpatne++;
                        }
                        break;
                }
            }
            VysledkyList.ItemsSource = priklady;
            DobreButton.Text = pocitadloSpravne.ToString();
            SpatneButton.Text = pocitadloSpatne.ToString();
        }

        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
        }

        private async void VysledkyList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            byte selectedItem = byte.Parse(e.ItemIndex.ToString());
            switch (fronta[selectedItem].DruhPrikladu)
            {
                case 1:
                    if (fronta[selectedItem].PrvniCislo + fronta[selectedItem].DruheCislo == fronta[selectedItem].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(selectedItem, fronta));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(selectedItem, fronta));
                    }
                    break;
                case 2:
                    if (fronta[selectedItem].PrvniCislo - fronta[selectedItem].DruheCislo == fronta[selectedItem].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(selectedItem, fronta));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(selectedItem, fronta));
                    }
                    break;
                case 3:
                    if (fronta[selectedItem].PrvniCislo * fronta[selectedItem].DruheCislo == fronta[selectedItem].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(selectedItem, fronta));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(selectedItem, fronta));
                    }
                    break;
                case 4:
                    if (fronta[selectedItem].PrvniCislo / fronta[selectedItem].DruheCislo == fronta[selectedItem].UzivateluvVstup)
                    {
                        await Navigation.PushAsync(new VysledekDobre(selectedItem, fronta));
                    }
                    else
                    {
                        await Navigation.PushAsync(new VysledekSpatne(selectedItem, fronta));
                    }
                    break;
            }
            Task.Delay(500);
            if (sender is ListView lv) lv.SelectedItem = null;
        }

        private async void DobreButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                int vysledek = 0;
                List<Priklad> prikladyDobre = new List<Priklad>();

                foreach (var priklad in fronta)
                {
                    switch (priklad.DruhPrikladu)
                    {
                        case 1:
                            vysledek = priklad.PrvniCislo + priklad.DruheCislo;
                            if (priklad.UzivateluvVstup == vysledek)
                            {
                                prikladyDobre.Add(priklad);
                            }
                            break;
                        case 2:
                            vysledek = priklad.PrvniCislo - priklad.DruheCislo;
                            if (priklad.UzivateluvVstup == vysledek)
                            {
                                prikladyDobre.Add(priklad);
                            }
                            break;
                        case 3:
                            vysledek = priklad.PrvniCislo * priklad.DruheCislo;
                            if (priklad.UzivateluvVstup == vysledek)
                            {
                                prikladyDobre.Add(priklad);
                            }
                            break;
                        case 4:
                            vysledek = priklad.PrvniCislo / priklad.DruheCislo;
                            if (priklad.UzivateluvVstup == vysledek)
                            {
                                prikladyDobre.Add(priklad);
                            }
                            break;
                    }
                }
                await Navigation.PushAsync(new VysledekDobre(0, prikladyDobre));
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
                int vysledek = 0;
                List<Priklad> prikladySpatne = new List<Priklad>();

                foreach (var priklad in fronta)
                {
                    switch (priklad.DruhPrikladu)
                    {
                        case 1:
                            vysledek = priklad.PrvniCislo + priklad.DruheCislo;
                            if (priklad.UzivateluvVstup != vysledek)
                            {
                                prikladySpatne.Add(priklad);
                            }
                            break;
                        case 2:
                            vysledek = priklad.PrvniCislo - priklad.DruheCislo;
                            if (priklad.UzivateluvVstup != vysledek)
                            {
                                prikladySpatne.Add(priklad);
                            }
                            break;
                        case 3:
                            vysledek = priklad.PrvniCislo * priklad.DruheCislo;
                            if (priklad.UzivateluvVstup != vysledek)
                            {
                                prikladySpatne.Add(priklad);
                            }
                            break;
                        case 4:
                            vysledek = priklad.PrvniCislo / priklad.DruheCislo;
                            if (priklad.UzivateluvVstup != vysledek)
                            {
                                prikladySpatne.Add(priklad);
                            }
                            break;
                    }
                }
                await Navigation.PushAsync(new VysledekSpatne(0, prikladySpatne));
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
        private Color barvaCellu;

        public Vysledek(string textovaPodobaPrikladu)
        {
            this.textovaPodobaPrikladu = textovaPodobaPrikladu;
        }
        public string TextovaPodobaPrikladu
        {
            get => textovaPodobaPrikladu;
            set => textovaPodobaPrikladu = value;
        }
        public Color BarvaCellu
        {
            get => barvaCellu;
            set => barvaCellu = value;
        }
    }
}