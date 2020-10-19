using Mathster.Helpers.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Statistiky : TabbedPage
    {
        public Statistiky()
        {
            InitializeComponent();
            Title = AppResource.Statistiky;
            Children.Add(new StatistikyGraf() {Title=AppResource.Celkem});
            Children.Add(new StatistikyGraf() {Title=AppResource.Scitani});
            Children.Add(new StatistikyGraf() {Title=AppResource.Odecitani});
            Children.Add(new StatistikyGraf() {Title=AppResource.Nasobeni});
            Children.Add(new StatistikyGraf() {Title=AppResource.Deleni});
            Children.Add(new StatistikyGraf() {Title=AppResource.Pomer});
        }
    }
}