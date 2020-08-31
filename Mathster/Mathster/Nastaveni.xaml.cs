using System;
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

            JmenoEntry.Placeholder = AppResource.ZadejteJmeno;
            
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
            OAplikaciVerze.Text = AppResource.OAplikaciVerze + "1.3";
            
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