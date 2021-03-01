using SQLite;
using Xamarin.Forms;

namespace Mathster.Resources.Database_Models
{
    public class SettingsModel
    {
        [PrimaryKey] [AutoIncrement] public int ID { get; set; }
        public OSAppTheme Theme { get; set; }
    }
}