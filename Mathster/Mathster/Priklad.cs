using System;

namespace Mathster
{
    public class Priklad
    {
        private byte ID;
        private int prvniCislo;
        private int druheCislo;
        private int uzivateluvVstup;
        private byte druhPrikladu;
        public Priklad(int prvniCislo, int druheCislo, byte druhPrikladu)
        {
            this.prvniCislo = prvniCislo;
            this.druheCislo = druheCislo;
            this.druhPrikladu = druhPrikladu;
        }
        public Priklad() { }

        public byte Id
        {
            get => ID;
            private set => ID = value;
        }
        
        public int PrvniCislo
        {
            get => prvniCislo;
            private set => prvniCislo = value;
        }

        public int DruheCislo
        {
            get => druheCislo;
            private set => druheCislo = value;
        }

        public int UzivateluvVstup
        {
            get => uzivateluvVstup;
            set => uzivateluvVstup = value;
        }

        public byte DruhPrikladu
        {
            get => druhPrikladu;
            private set => druhPrikladu = value;
        }
        
        public Priklad VygenerujPriklad(byte id, int minCisel, int maxCisel, int minDeleniANasobeni, int maxDeleniANasobeni, byte druhPrikladu)
        {
            Random random = new Random();
            Priklad priklad = null;
            int prvniCislo = random.Next(minCisel, maxCisel);
            int druheCislo = random.Next(minCisel, maxCisel);

            if (druhPrikladu == 1 || druhPrikladu == 2)
            {
                priklad = new Priklad(prvniCislo, druheCislo, druhPrikladu);
            }
            else if (druhPrikladu == 3)
            {
                druheCislo = random.Next(minDeleniANasobeni, maxDeleniANasobeni);
                priklad = new Priklad(prvniCislo, druheCislo, druhPrikladu);
            }
            else if (druhPrikladu == 4)
            {
                druheCislo = random.Next(minDeleniANasobeni, maxDeleniANasobeni);
                while (prvniCislo % druheCislo != 0)
                {
                    prvniCislo = random.Next(minCisel, maxCisel);    
                }
                priklad = new Priklad(prvniCislo, druheCislo, druhPrikladu);
            }
            priklad.Id = id;
            return priklad;
        }
        public Priklad VygenerujNahodnyPriklad(byte id, int minCisel, int maxCisel, int minDeleniANasobeni, int maxDeleniANasobeni)
        {
            Random random = new Random();
            druhPrikladu = Byte.Parse(random.Next(1, 5).ToString());
            return VygenerujPriklad(id, minCisel, maxCisel, minDeleniANasobeni, maxDeleniANasobeni, druhPrikladu);
        }
        public string VratPriklad()
        {
            switch (DruhPrikladu)
            {
                case 1:
                    return $"{PrvniCislo} + {DruheCislo} = {UzivateluvVstup}";
                case 2:
                    return $"{PrvniCislo} - {DruheCislo} = {UzivateluvVstup}";
                case 3:
                    return $"{PrvniCislo} X {DruheCislo} = {UzivateluvVstup}";
                case 4:
                    return $"{PrvniCislo} ÷ {DruheCislo} = {UzivateluvVstup}";
                default:
                    return "";
            }
        } 
    }
}