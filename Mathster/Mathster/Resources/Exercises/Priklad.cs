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
            private set => zadani = value;
        }

        public string ZadaniPodsebe
        {
            get => zadaniPodsebe;
            private set => zadaniPodsebe = value;
        }

        public float UzivateluvVstup
        {
            get => uzivateluvVstup;
            set => uzivateluvVstup = value;
        }

        public int Vysledek
        {
            get => vysledek;
            private set => vysledek = value;
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

        public Priklad VygenerujPriklad(byte id, int minCisel, int maxCisel, byte druhPrikladu = 0,
            int minDeleniANasobeni = 2, int maxDeleniANasobeni = 6)
        {
            Random random = new Random();
            int prvniCislo = random.Next(minCisel, maxCisel);
            int druheCislo = random.Next(minCisel, maxCisel);

            if (druheCislo == 0)
            {
                druheCislo = random.Next(1, 5);
            }

            switch (druhPrikladu)
            {
                case 1:
                    return new Priklad(id, druhPrikladu, $"{prvniCislo} + {druheCislo} = ",
                        $" {prvniCislo}\n+{druheCislo}\n—",
                        (byte) prvniCislo.ToString().Length, prvniCislo + druheCislo);
                case 2:
                    return new Priklad(id, druhPrikladu, $"{prvniCislo} - {druheCislo} = ",
                        $" {prvniCislo}\n-{druheCislo}\n—",
                        (byte) prvniCislo.ToString().Length, prvniCislo - druheCislo);
                case 3:
                    druheCislo = random.Next(minDeleniANasobeni, maxDeleniANasobeni);
                    return new Priklad(id, druhPrikladu, $"{prvniCislo} X {druheCislo} = ",
                        $" {prvniCislo}\nX{druheCislo}\n—",
                        (byte) prvniCislo.ToString().Length, prvniCislo * druheCislo);
                case 4:
                    druheCislo = random.Next(minDeleniANasobeni, maxDeleniANasobeni);
                    while (prvniCislo % druheCislo != 0)
                    {
                        prvniCislo = random.Next(minCisel, maxCisel);
                    }

                    return new Priklad(id, druhPrikladu, $"{prvniCislo} ÷ {druheCislo} = ",
                        $" {prvniCislo}\n-{druheCislo}\n—",
                        (byte) prvniCislo.ToString().Length, prvniCislo / druheCislo);
                case 5:
                    string zadani;
                    int vysledek;
                    byte experience;
                    GenerujRovniciScitani(out zadani, out vysledek, out experience);
                    return new Priklad(id, 5, zadani, "", experience, vysledek);
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

        private void GenerujRovniciScitani(out string zadani, out int vysledek, out byte experience)
        {
            Random random = new Random();
            vysledek = random.Next(-3, 5);
            int nasobek = random.Next(2, 5), cisloNavic = random.Next(-20, 21), xNavic = random.Next(-20, 21);
            experience = (byte) nasobek.ToString().Length;
            experience += (byte) +cisloNavic.ToString().Length;
            experience += (byte) xNavic.ToString().Length;

            switch (random.Next(0, 3))
            {
                case 0:
                    if (cisloNavic >= 0)
                    {
                        zadani = $"{nasobek}x +{cisloNavic} = ";
                    }
                    else
                    {
                        zadani = $"{nasobek}x {cisloNavic} = ";
                    }

                    zadani += $"{nasobek * vysledek + cisloNavic}";
                    break;

                case 1:
                    if (xNavic >= 0)
                    {
                        zadani = $"{nasobek + xNavic}x = {nasobek * vysledek} +{xNavic}x";
                    }
                    else
                    {
                        zadani = $"{nasobek + xNavic}x = {nasobek * vysledek} {xNavic}x";
                    }

                    break;

                default:
                    switch (cisloNavic >= 0, xNavic >= 0)
                    {
                        case (true, true):
                            zadani = $"{nasobek + xNavic}x +{cisloNavic} = {vysledek * nasobek + cisloNavic} +{xNavic}x";
                            break;
                        case (true, false):
                            zadani = $"{nasobek + xNavic}x +{cisloNavic} = {vysledek * nasobek + cisloNavic} {xNavic}x";
                            break;
                        case (false, true):
                            zadani = $"{nasobek + xNavic}x {cisloNavic} = {vysledek * nasobek + cisloNavic} +{xNavic}x";
                            break;
                        case (false, false):
                            zadani = $"{nasobek + xNavic}x {cisloNavic} = {vysledek * nasobek + cisloNavic} {xNavic}x";
                            break;
                    }

                    break;
            }
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
            if (zadani.Length >= 13 && !(druhPrikladu == 5))
            {
                return $"{zadani}\n= {UzivateluvVstup}";
            } 
            if (druhPrikladu == 5)
            {
                return $"{zadani}\nx = {uzivateluvVstup}";
            }

            return "";
        }
    }
}