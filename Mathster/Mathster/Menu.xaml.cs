using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            InitializeComponent();
            Title = "Menu";
        }
        private async void ScitaniButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VelikostCisel(1));
        }
        private async void OdcitaniButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VelikostCisel(2));
        }
        private async void NasobeniButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VelikostCisel(3));
        }

        private async void DeleniButton_OnClicked(object sender, EventArgs e)
        { 
            await Navigation.PushAsync(new VelikostCisel(4));
        }

        private async void NahodneButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VelikostCisel(5));
        }
    }
}