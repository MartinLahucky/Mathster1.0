using System;
using Mathster.Helpers.Model;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrvniScreen : ContentPage
    {
        public PrvniScreen()
        {
            InitializeComponent();
        }

        private void PokracovatButton_OnClicked(object sender, EventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.Table<DBModel>().First();
                connection.Update(new DBModel()
                {
                    Jmeno = JmenoEntry.Text,
                    PrvniSpusteni = false
                });
            }
        }
    }
}