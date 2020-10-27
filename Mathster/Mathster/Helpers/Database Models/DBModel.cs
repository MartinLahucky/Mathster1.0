using System;
using SQLite;

namespace Mathster.Helpers.Model
{
    public class DBModel
    {
        [PrimaryKey]
        public int ID { get; set; }
        [MaxLength(25)]
        public string Jmeno { get; set; }
        public int Experience { get; set; }
        public int CelkemPrikladu { get; set; }
        public int CelkemScitani { get; set; }
        public int CelkemScitaniSpravne { get; set; }
        public int CelkemOdcitani { get; set; }
        public int CelkemOdcitaniSpravne { get; set; }
        public int CelkemNasobeni { get; set; }
        public int CelkemNasobeniSpravne { get; set; }
        public int CelkemDeleni { get; set; }
        public int CelkemDeleniSpravne { get; set; }
        public int CelkemPrikladuDobre { get; set; }
        public void GetLevel(out int level, out double progres, DBModel tabulka)
        {
            level = (int) Math.Sqrt(tabulka.Experience) / 20;
            progres = Math.Sqrt(tabulka.Experience) / 20 - level;
        }
    }
}