using System;
using Mathster.Helpers.Model;
using Mathster.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RozborVysledku : ContentPage
    {
        public RozborVysledku()
        {
            InitializeComponent();
            MenuToolbarButton.IconImageSource = "round_house_white_18dp.png";
            Title = AppResource.Reseni;
            SpatnaOdpovedLabel.Text = AppResource.VaseReseni;
            SpravneReseniLabel.Text = AppResource.SpravneReseni;
        }

        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
        }
    }
    
}