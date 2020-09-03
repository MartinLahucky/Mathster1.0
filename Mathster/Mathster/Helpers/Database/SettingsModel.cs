using SQLite;
using Xamarin.Forms;

namespace Mathster.Helpers.Model
{
    public class SettingsModel
    {
        [PrimaryKey]
        public int ID { get; set; }
        public bool DarkMode { get; set; }
        public Color BarvaPrimarni { get; set; }
        public Color BarvaSekundarni { get; set; }
        public Color BarvaTlacitek { get; set; }
        public Color BarvaTextu { get; set; }
        public Color BarvaPozadi { get; set; }
        public Color BarvaPozadiDarkMode { get; set; }
        public Color BarvaSpravne { get; set; }
        public Color BarvaSpatne { get; set; }
        public Color BarvaInfo { get; set; }
        public Color BarvaPrimarniSvetla { get; set; }
        public Color BarvaSekundarniSvetla { get; set; }
        
        
        
        
    }
}