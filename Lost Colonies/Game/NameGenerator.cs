using System;
using System.Diagnostics;

namespace Lost_Colonies
{
    public class NameGenerator
    {
        private const string PAIRS = "..LEXEGEZACEBISOUSESARMAINDIREA.ERATENBERALAVETIEDORQUANTEISRION";
        private Random rnd;

        public NameGenerator()
        {

        }

        private string GenPlanetName(Random pRnd)
        {
            string name = "";

            int nbPairs = PAIRS.Length / 2;

            for (int n = 0; n < 4; n++)
            {
                int numPair = pRnd.Next(nbPairs);

                string pair = PAIRS.Substring(numPair * 2, 2);

                name += pair;
            }

            // Remplace les . par des vides
            name = name.Replace(".", "");

            return name;
        }

        public string GetPlanetName(int pSeed)
        {
            string name = "";
            rnd = new Random(pSeed);

            while (name.Length < 2)
            {
                name = GenPlanetName(rnd);
                if (name.Length < 2)
                    Debug.WriteLine("erreur nom trop court");
            }
            return name;
        }
    }
}
