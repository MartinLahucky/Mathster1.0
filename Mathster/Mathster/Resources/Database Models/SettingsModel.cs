using SQLite;

namespace Mathster.Resources.Database_Models
{
    public class SettingsModel
    {
        [PrimaryKey] public int ID { get; set; }
        public bool DarkMode { get; set; }
        public string BackgroundHex { get; set; }
    }
}