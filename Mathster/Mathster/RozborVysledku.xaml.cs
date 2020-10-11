using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathster.Helpers.Model;
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
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            SettingsModel tabulkaNastaveni = await App.Database.GetSettings();
            BackgroundColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
                // .TextColor = Color.FromHex(tabulkaNastaveni.BackgroundHex);
        }
    }
    
}