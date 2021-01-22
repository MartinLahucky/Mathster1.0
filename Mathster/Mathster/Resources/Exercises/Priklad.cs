using System;

namespace Mathster.Resources.Exercises
{
    public class Priklad
    {
        private byte id;
        private string zadani;
        private string zadaniPodsebe;
        private float uzivateluvVstup;
        private int vysledek;
        private byte experience;
        private byte druhPrikladu;
        private long delkaPocitani;

        public Priklad(byte id, byte druhPrikladu, string zadani, string zadaniPodsebe, byte experience, int vysledek)
        {
            this.id = id;
            this.druhPrikladu = druhPrikladu;
            this.zadani = zadani;
            this.zadaniPodsebe = zadaniPodsebe;
            this.experience = experience;
            this.vysledek = vysledek;
        }

        public Priklad()
        {
        }

        public byte Id
        {
            get => id;
            private set => id = value;
        }

        public string Zadani
        {
            get => zadani;
            set => zadani = value;
        }

        public string ZadaniPodsebe
        {
            get => zadaniPodsebe;
            set => zadaniPodsebe = value;
        }

        public float UzivateluvVstup
        {
            get => uzivateluvVstup;
            set => uzivateluvVstup = value;
        }

        public int Vysledek
        {
            get => vysledek;
            set => vysledek = value;
        }
        public byte DruhPrikladu
        {
            get => druhPrikladu;
            private set => druhPrikladu = value;
        }

        public long DelkaPocitani
        {
            get => delkaPocitani;
            set => delkaPocitani = value;
        }

        public Priklad VygenerujPriklad(byte id, int minCisel, int maxCisel, byte druhPrikladu,
            int minDeleniANasobeni = 2, int maxDeleniANasobeni = 6)
        {
            Random random = new Random();
            int prvniCislo = random.Next(minCisel, maxCisel);
            int druheCislo = random.Next(minCisel, maxCisel);

            switch (druhPrikladu)
            {
                case 1:
                    return new Priklad(id, druhPrikladu, $"{prvniCislo} + {druheCislo} = ", $" {prvniCislo}\n+{druheCislo}\n—",
                        (byte) prvniCislo.ToString().Length, prvniCislo + druheCislo);
                case 2:
                    return new Priklad(id,  druhPrikladu,$"{prvniCislo} - {druheCislo} = ", $" {prvniCislo}\n-{druheCislo}\n—",
                        (byte) prvniCislo.ToString().Length, prvniCislo - druheCislo);
                case 3:
                    druheCislo = random.Next(minDeleniANasobeni, maxDeleniANasobeni);
                    return new Priklad(id, druhPrikladu, $"{prvniCislo} X {druheCislo} = ", $" {prvniCislo}\nX{druheCislo}\n—",
                        (byte) prvniCislo.ToString().Length, prvniCislo * druheCislo);
                case 4:
                    druheCislo = random.Next(minDeleniANasobeni, maxDeleniANasobeni);
                    while (prvniCislo % druheCislo != 0)
                    {
                        prvniCislo = random.Next(minCisel, maxCisel);
                    }

                    return new Priklad(id, druhPrikladu, $"{prvniCislo} ÷ {druheCislo} = ", $" {prvniCislo}\n-{druheCislo}\n—",
                        (byte) prvniCislo.ToString().Length, prvniCislo / druheCislo);
                case 5:
                    return null;
                default:
                    return null;
            }
        }

        public int GetExperience(bool spravne)
        {
            if (spravne)
            {
                return (druhPrikladu * (druhPrikladu / 4) + 1) * experience * 20;
            }

            return druhPrikladu * (druhPrikladu / 4) + 1;
        }

        public Priklad VygenerujNahodnyPriklad(byte id, int minCisel, int maxCisel, int minDeleniANasobeni = 2,
            int maxDeleniANasobeni = 6)
        {
            Random random = new Random();
            druhPrikladu = Byte.Parse(random.Next(1, 5).ToString());
            return VygenerujPriklad(id, minCisel, maxCisel, druhPrikladu, minDeleniANasobeni, maxDeleniANasobeni);
        }
        
        public string VratPriklad()
        {
            
            //TODO dořešit velikosti všeho
            // if (PrvniCislo >= 10000 || DruheCislo >= 10000 || UzivateluvVstup >= 10000)
            // {
            //     return $"{zadani}\n= {UzivateluvVstup}";
            // }

            return $"{zadani}{uzivateluvVstup}";
        }
    }
}