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
        
        public Priklad VygenerujPriklad(byte id, int min, int max, byte druhPrikladu)
        {
            Random random = new Random();
            Priklad priklad = null;
            int prvniCislo = random.Next(min, max);
            int druheCislo = random.Next(min, max);
            
            switch (druhPrikladu)
            {
                case 1:
                    priklad = new Priklad(prvniCislo, druheCislo, druhPrikladu);
                    break;
                case 2:
                    priklad = new Priklad(prvniCislo, druheCislo, druhPrikladu);
                    break;
                case 3:
                    prvniCislo = random.Next(1000);
                    priklad = new Priklad(prvniCislo, druheCislo, druhPrikladu);
                    break;
                case 4:
                    prvniCislo = random.Next(10000);
                    while (prvniCislo % druheCislo != 0)
                    {
                        prvniCislo = random.Next(10000);    
                    }
                    priklad = new Priklad(prvniCislo, druheCislo, druhPrikladu);
                    break;
            }
            priklad.Id = id;
            return priklad;
        }
    }
}