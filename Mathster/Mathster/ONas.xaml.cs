using Mathster.Helpers.Resources;
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

            ONasStaticLabel.Text = AppResource.ONas;
            ONasLabel.Text = $"{AppResource.ONasText}\n \n{AppResource.VyvojGrafika}\n{AppResource.VyvojProgramovani}";
            OAplikaciStaticLabel.Text = AppResource.OAplikaci;
            OAplikaciLabel.Text = AppResource.OAplikaciText;
        }
    }
}