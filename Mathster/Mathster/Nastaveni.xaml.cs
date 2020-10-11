using System;
using System.Linq;
using System.Threading.Tasks;
using Mathster.Helpers.Model;
using Mathster.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Nastaveni : ContentPage
    {
        public Nastaveni()
        {
            InitializeComponent();
            MenuToolbarButton.IconImageSource = "round_house_white_18dp.png";
            Title = AppResource.Nastaveni;
            JmenoEntry.Placeholder = AppResource.ZadejteJmeno;
            JmenoLabel.Text = AppResource.Jmeno;
            DarkModeLabel.Text = AppResource.DarkMode;
            
            Task task = Task.Run(async () =>
            {
                DBModel cteni = await App.Database.GetTable();

                if (cteni.Jmeno == "")
                {
                    JmenoEntry.Text = String.Empty;
                }
                else
                {
                    JmenoEntry.Text = cteni.Jmeno;
                }
            });
            Task.WaitAll(task);
            OAplikaciVerze.Text = AppResource.OAplikaciVerze + "Beta Release 1.4";
        }
        private async void DarkModeSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            SettingsModel tabulkaNastaveni = await App.Database.GetSettings();
            
            if(e.Value)
            {
                tabulkaNastaveni.BackgroundHex = "#262630";
            }
            else
            {
                tabulkaNastaveni.BackgroundHex = "#FAFAFA";
            }
            BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
            await App.Database.UpdateSettings(tabulkaNastaveni);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            SettingsModel tabulkaNastaveni = await App.Database.GetSettings();
            BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
            if (tabulkaNastaveni.BackgroundHex == "#262630")
            {
                DarkModeSwitch.IsToggled = true;
            }
        }
        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            DBModel tabulka = await App.Database.GetTable();
            tabulka.Jmeno = JmenoEntry.Text;
            await App.Database.UpdateTable(tabulka);
            await Navigation.PushAsync(new Menu());
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
        
            DBModel tabulka = await App.Database.GetTable();
            tabulka.Jmeno = JmenoEntry.Text;
            await App.Database.UpdateTable(tabulka);
        }
    }
}