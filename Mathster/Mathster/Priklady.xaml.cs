using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Mathster.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Priklady : ContentPage
    {
        private Priklad priklad;
        private byte ID;
        private List<Priklad> fronta;
        private bool podsebe = false;
        public Priklady(byte id, List<Priklad> fronta)
        {
            InitializeComponent();
            DalsiButton.Text = AppResource.Dalsi;
            OdvezdatButton.Text = AppResource.Odevzdat;
            
            priklad = fronta[id];

            switch (priklad.DruhPrikladu)
            {
                case 1:
                    Title = AppResource.Scitani;
                    PrikladLabel.Text = $"{priklad.PrvniCislo} + {priklad.DruheCislo} = ";
                    break;
                case 2:
                    Title = AppResource.Odecitani;
                    PrikladLabel.Text = $"{priklad.PrvniCislo} - {priklad.DruheCislo} = ";
                    break;
                case 3:
                    Title = AppResource.Nasobeni;
                    PrikladLabel.Text = $"{priklad.PrvniCislo} X {priklad.DruheCislo} = ";
                    break;
                case 4:
                    Title = AppResource.Deleni;
                    PrikladLabel.Text = $"{priklad.PrvniCislo} ÷ {priklad.DruheCislo} = ";
                    break;
            }

            this.fronta = fronta;
            ID = id;
            
            if (id < (fronta.Count - 1))
            {
                OdvezdatButton.IsVisible = false;
            }
            else
            {
                DalsiButton.IsVisible = false;
            }
            
        }
        
        private async void OdvezdatButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                fronta[ID].UzivateluvVstup = int.Parse(VysledekInput.Text);
                await Navigation.PushAsync(new Souhrn(fronta));
                var existingPages = Navigation.NavigationStack.ToList();
                foreach(var page in existingPages)
                {
                    Navigation.RemovePage(page);
                }
            }
            catch (Exception exception)
            {
                await DisplayAlert(AppResource.Upozorneni, AppResource.UpozorneniZadejteCislo, AppResource.Ok);
            }
        }

        private async void DalsiButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                fronta[ID].UzivateluvVstup = int.Parse(VysledekInput.Text);
                ID++;
                await Navigation.PushAsync(new Priklady(ID, fronta));
                var existingPages = Navigation.NavigationStack.ToList();
                foreach(var page in existingPages)
                {
                    Navigation.RemovePage(page);
                }
            }
            catch (Exception exception)
            {
                await DisplayAlert(AppResource.Upozorneni, AppResource.UpozorneniZadejteCislo, AppResource.Ok);
            }
        }
        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            VysledekInput.Focus();
        }

        private void PodSebeButton_OnClicked(object sender, EventArgs e)
        {
            switch (podsebe)
            {
                case false:
                    switch (priklad.DruhPrikladu)
                    {
                        case 1:
                            Title = AppResource.Scitani;
                            PrikladLabel.Text = $" {priklad.PrvniCislo}\n+{priklad.DruheCislo}\n—";
                            break;
                        case 2:
                            Title = AppResource.Odecitani;
                            PrikladLabel.Text = $" {priklad.PrvniCislo}\n-{priklad.DruheCislo}\n—";
                            break;
                        case 3:
                            Title = AppResource.Nasobeni;
                            PrikladLabel.Text = $" {priklad.PrvniCislo}\nX{priklad.DruheCislo}\n—";
                            break;
                        case 4:
                            Title = AppResource.Deleni;
                            PrikladLabel.Text = $"{priklad.PrvniCislo}\n—\n{priklad.DruheCislo}";
                            break;
                    }
                    VysledekInput.FlowDirection = FlowDirection.RightToLeft;
                    podsebe = true;
                    break;
                case true:
                    switch (priklad.DruhPrikladu)
                    {
                        case 1:
                            Title = AppResource.Scitani;
                            PrikladLabel.Text = $"{priklad.PrvniCislo} + {priklad.DruheCislo} = ";
                            break;
                        case 2:
                            Title = AppResource.Odecitani;
                            PrikladLabel.Text = $"{priklad.PrvniCislo} - {priklad.DruheCislo} = ";
                            break;
                        case 3:
                            Title = AppResource.Nasobeni;
                            PrikladLabel.Text = $"{priklad.PrvniCislo} X {priklad.DruheCislo} = ";
                            break;
                        case 4:
                            Title = AppResource.Deleni;
                            PrikladLabel.Text = $"{priklad.PrvniCislo} ÷ {priklad.DruheCislo} = ";
                            break;
                    }
                    VysledekInput.FlowDirection = FlowDirection.LeftToRight;
                    podsebe = false;
                    break;
            }
            VysledekInput.Focus();
        }

        private void VysledekInput_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //TODO Entry způsobuje zacyklení, ptořeba udělat Label, který bude pod entry a bude se měnit podle režimu
            if (podsebe)
            {
                try
                {
                    List<string> cislo = new List<string>();
                    List<string> noveCislo = new List<string>();
            
                    string cisloText = VysledekInput.Text;
            
            
                    for (int i = 0; i < cisloText.Length; i++)
                    {
                        cislo.Add(cisloText[i].ToString());
                    }
            
                    noveCislo.Add(cislo[cislo.Count - 1]);

                    if (cislo.Count != 1)
                    {
                        for (int i = cislo.Count - 2; i >= 0; i--)
                        {
                            noveCislo.Add((cislo[i]));
                        }
                    }

                    VysledekInput.Text = String.Empty;
                    
                    for (int i = 0; i < noveCislo.Count; i++)
                    {
                        VysledekInput.Text += noveCislo[i];
                    }
                    
                    podsebe = false;
                }
                catch
                {
                    VysledekInput.Text = String.Empty;

                    podsebe = false;
                }
                podsebe = true;
            }
            else
            {
                podsebe = false;
            } 
        }
    }
}