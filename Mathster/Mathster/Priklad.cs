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

        public Priklad VygenerujNahodnyPriklad(byte id, byte velikostScitaniAOdcitani, byte velikostDeleniANasobei)
        {
            int min = 0, max = 0;
            Random random = new Random();
            druhPrikladu = Byte.Parse(random.Next(1, 5).ToString());
            
            if (druhPrikladu == 1 || druhPrikladu == 2)
            {
                switch (velikostScitaniAOdcitani)
                {
                    case 1:
                        min = 1;
                        max = 10;
                        break;
                    case 2:
                        min = 10;
                        max = 100;
                        break;
                    case 3:
                        min = 100;
                        max = 1000;
                        break;
                    case 4:
                        min = 1000;
                        max = 10000;
                        break;
                    case 5:
                        min = 10000;
                        max = 100000;
                        break;
                    case 6:
                        max = 1000000;
                        break;
                    default:
                        min = 1;
                        max = 10;
                        break;
                }
            }
            else
            {
                switch (velikostDeleniANasobei)
                {
                    case 1:
                        min = 2;
                        max = 6;
                        break;
                    case 2:
                        min = 2;
                        max = 11;
                        break;
                    case 3:
                        min = 2;
                        max = 21;
                        break;
                    default:
                        min = 2;
                        max = 6;
                        break;
                }
            }
            return VygenerujPriklad(id, min, max, druhPrikladu);
        }
    }
}