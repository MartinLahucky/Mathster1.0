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
        public int CelkemPrikladuSpravne { get; set; }
        public void GetLevel(out int level, out double progres, DBModel tabulka)
        {
            level = (int) Math.Sqrt(tabulka.Experience) / 20;
            progres = Math.Sqrt(tabulka.Experience) / 20 - level;
        }

        public void AddGoodStats(byte druhPrikladu, DBModel tabulka)
        {
            switch (druhPrikladu)
            {
                case 1:
                    tabulka.CelkemScitaniSpravne++;
                    break;
                case 2:
                    tabulka.CelkemOdcitaniSpravne++;
                    break;
                case 3:
                    tabulka.CelkemNasobeniSpravne++;
                    break;
                case 4:
                    tabulka.CelkemDeleniSpravne++;
                    break;
            }
            tabulka.CelkemPrikladuSpravne++;
            AddStats(druhPrikladu, tabulka);
        }
        public void AddStats(byte druhPrikladu, DBModel tabulka)
        {
            switch (druhPrikladu)
            {
                case 1:
                    tabulka.CelkemScitani++;
                    break;
                case 2:
                    tabulka.CelkemOdcitani++;
                    break;
                case 3:
                    tabulka.CelkemNasobeni++;
                    break;
                case 4:
                    tabulka.CelkemDeleni++;
                    break;
            }
            tabulka.CelkemPrikladu++;
        }
    }
}