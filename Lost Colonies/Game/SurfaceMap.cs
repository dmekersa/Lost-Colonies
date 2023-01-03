using System;

namespace Lost_Colonies
{
    public class SurfaceMap
    {
        public int[,] Map;
        public const int MAPW = 512;
        public const int MAPH = 512;
        Random rnd = new Random();

        public SurfaceMap()
        {
            Map = new int[MAPH, MAPW];
            for (int l = 0; l < MAPH; l++)
            {
                for (int c = 0; c < MAPH; c++)
                {
                    if (rnd.Next(6 + 1) >= 4)
                    {
                        Map[l, c] = 1;
                    }
                    else
                    {
                        Map[l, c] = 0;
                    }
                }
            }

        }
    }
}
