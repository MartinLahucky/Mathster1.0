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
    public partial class ONas : ContentPage
    {
        public ONas()
        {
            InitializeComponent();

            ONasLabel.Text =
                "(1) Základní práva a svobody se zaručují všem bez rozdílu pohlaví, rasy, barvy pleti, jazyka, víry a náboženství, politického či jiného smýšlení, národního nebo sociálního původu, příslušnosti k národnostní nebo etnické menšině, majetku, rodu nebo jiného postavení.";
            OAplikaciLabel.Text =
                "(2) Každý má právo svobodně rozhodovat o své národnosti. Zakazuje se jakékoli ovlivňování tohoto rozhodování a všechny způsoby nátlaku směřující k odnárodňování. (3) Nikomu nesmí být způsobena újma na právech pro uplatňování jeho základních práv a svobod.";
        }
    }
}