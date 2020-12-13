using System;
using System.Linq;
using Mathster.Resources.Localization;
using Mathster.Resources.Database_Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ONas : ContentPage
    {
        public ONas()
        {
            InitializeComponent();
            MenuToolbarButton.IconImageSource = "round_house_white_18dp.png";
            Title = AppResource.ONas;
            ONasStaticLabel.Text = AppResource.ONas;
            ONasLabel.Text = $"{AppResource.ONasText}\n \n{AppResource.VyvojGrafika}\n{AppResource.VyvojProgramovani}";
            OAplikaciStaticLabel.Text = AppResource.OAplikaci;
            OAplikaciLabel.Text = AppResource.OAplikaciText;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            SettingsModel tabulkaNastaveni = await App.Database.GetSettings();
            BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
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
    }
}