using SQLite;

namespace Mathster.Helpers.Model
{
    public class DBModel
    {
        [PrimaryKey, AutoIncrement]
        private int ID { get; set; }
        
        [MaxLength(30), NotNull]
        private string Jmeno { get; set; }
        
        private byte PosledniPocetPrikladu { get; set; }
        
        private byte PosledniUspesnost { get; set; }
        
        private int Experience { get; set; }
        
        private int CelkemPrikladu { get; set; }

        private int CelkemPrikladuDobre { get; set; }

        private float Nachozeno { get; set; }

        private int NejlepsiSerieBezChyby { get; set; }

        private string DruhNejcastejiPocitanychPrikladu { get; set; }
    }
}