using SQLite;
using Xamarin.Forms;

namespace Mathster.Helpers.Model
{
    public class SettingsModel
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string BackgroundHex { get; set; }
    }
}