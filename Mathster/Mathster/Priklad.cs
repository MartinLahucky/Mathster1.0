using System;

namespace Mathster
{
    public class Priklad
    {
        private byte ID;
        private int prvniCislo;
        private int druheCislo;
        private float uzivateluvVstup;
        private byte druhPrikladu;
        private long delkaPocitani;
        
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

        public float UzivateluvVstup
        {
            get => uzivateluvVstup;
            set => uzivateluvVstup = value;
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
        public Priklad VygenerujPriklad(byte id, int minCisel, int maxCisel, byte druhPrikladu, int minDeleniANasobeni = 2, int maxDeleniANasobeni = 6)
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
        public int GetExperience(bool spravne)
        {
            if (spravne)
            {
                return (druhPrikladu * (druhPrikladu / 4) + 1) * prvniCislo.ToString().Length * 20;
            }
            return druhPrikladu * (druhPrikladu / 4) + 1;
        }
        public Priklad VygenerujNahodnyPriklad(byte id, int minCisel, int maxCisel, int minDeleniANasobeni = 2, int maxDeleniANasobeni = 6)
        {
            Random random = new Random();
            druhPrikladu = Byte.Parse(random.Next(1, 5).ToString());
            return VygenerujPriklad(id, minCisel, maxCisel,  druhPrikladu, minDeleniANasobeni, maxDeleniANasobeni);
        }
        public int VratVysledek()
        {
            switch (druhPrikladu)
            {
                case 1:
                    return prvniCislo + druheCislo;
                case 2:
                    return prvniCislo - druheCislo;
                case 3:
                    return prvniCislo * druheCislo;
                case 4:
                    return prvniCislo / druheCislo;
                default:
                    return 0;
            }
        }
        public string VratPriklad()
        {
            if (PrvniCislo >= 10000 || DruheCislo >= 10000 || UzivateluvVstup >= 10000)
            {
                switch (DruhPrikladu)
                {
                    case 1:
                        return $"{PrvniCislo} + {DruheCislo} =\n= {UzivateluvVstup}";
                    case 2:
                        return $"{PrvniCislo} - {DruheCislo} =\n= {UzivateluvVstup}";
                    case 3:
                        return $"{PrvniCislo} X {DruheCislo} =\n= {UzivateluvVstup}";
                    case 4:
                        return $"{PrvniCislo} ÷ {DruheCislo} =\n= {UzivateluvVstup}";
                    default:
                        return "";
                }
            }
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