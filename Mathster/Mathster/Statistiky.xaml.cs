using Mathster.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mathster.Helpers.Model;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Statistiky : TabbedPage
    {
        public Statistiky()
        {
            InitializeComponent();
            Title = AppResource.Statistiky;
            VykresleniStranky();
        }
        async public void VykresleniStranky()
        {
            DBModel tabulka = await App.Database.GetTable();
            int level, progres;                                   
            tabulka.GetLevel(out level, out progres, tabulka);
            Children.Add(new StatistikyGraf(level, progres) {IconImageSource = "stonks.png", Title = ""});
            Children.Add(new StatistikyGraf(AppResource.Celkem, tabulka.CelkemPrikladuDobre, tabulka.CelkemPrikladu) { IconImageSource = "celkem_ikona.png", Title = "" });
            Children.Add(new StatistikyGraf(AppResource.Scitani, tabulka.CelkemScitaniSpravne, tabulka.CelkemScitani) { IconImageSource = "plus_ikona.png", Title = ""});
            Children.Add(new StatistikyGraf(AppResource.Odecitani, tabulka.CelkemOdcitaniSpravne, tabulka.CelkemOdcitani) { IconImageSource = "minus_ikona.png", Title = ""});
            Children.Add(new StatistikyGraf(AppResource.Nasobeni, tabulka.CelkemNasobeniSpravne, tabulka.CelkemNasobeni) { IconImageSource = "krat_ikona.png", Title = "" });                        
            Children.Add(new StatistikyGraf(AppResource.Deleni, tabulka.CelkemDeleniSpravne, tabulka.CelkemDeleni) { IconImageSource = "deleno_ikona.png", Title = "" });
            Children.Add(new StatistikyGraf(AppResource.Pomer, tabulka.CelkemScitani, tabulka.CelkemOdcitani, tabulka.CelkemNasobeni, tabulka.CelkemDeleni) { IconImageSource = "pomer_ikona.png",Title = "" });
        }
    }
}                                                                      
