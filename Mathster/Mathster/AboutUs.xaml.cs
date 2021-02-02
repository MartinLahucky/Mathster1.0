using System;
using System.Linq;
using Mathster.Resources.Database_Models;
using Mathster.Resources.Localization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutUs : ContentPage
    {
        public AboutUs()
        {
            InitializeComponent();
            MenuButton.IconImageSource = "menu_icon.png";
            Title = Localization.AboutApp;
            AboutUsStaticLabel.Text = Localization.AboutUs;
            AboutUsLabel.Text = $"{Localization.AboutUsText}\n \n{Localization.DevFrontEndText}\n{Localization.DevBackEndText}";
            AboutAppStaticLabel.Text = Localization.AboutApp;
            AboutAppLabel.Text = Localization.AboutAppText;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            SettingsModel settings = await App.Database.GetSettings();
            BackgroundColor = Color.FromHex(settings.BackgroundHex);
        }
        
        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                Navigation.RemovePage(page);
            }
        }
    }
}