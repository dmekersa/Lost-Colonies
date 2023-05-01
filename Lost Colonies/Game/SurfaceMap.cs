using Microsoft.Xna.Framework;
using System;

namespace Lost_Colonies
{
    public class SurfaceMap
    {
        public int[,] Map;
        public int[,] Fog;
        public int[,] Bitmask;
        public const int MAPW = 128;
        public const int MAPH = 128;
        public const int TILEW = 16;
        public const int TILEH = 16;
        public PlanetBase planetBase;
        public Point basePosition { get { return planetBase.basePosition; } }
        Random rnd = new Random();

        public SurfaceMap()
        {
            // Génère la surface
            Map = new int[MAPH, MAPW];
            Fog = new int[MAPH, MAPW];
            Bitmask = new int[MAPH, MAPW];
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
                    Fog[l, c] = 1;
                    Bitmask[l, c] = 0;
                }
            }

            // Positionne une base
            planetBase = new PlanetBase();
            planetBase.basePosition = new Point(rnd.Next(2, MAPW - 4), rnd.Next(2, MAPH - 4));
        }

        public bool isInMap(Vector2 pPosition)
        {
            if (pPosition.Y >= 0 && pPosition.Y < MAPH && pPosition.X >= 0 && pPosition.X < MAPW)
                return true;
            return false;
        }

        private bool _getFog(Vector2 pPosition)
        {
            if (isInMap(pPosition))
            {
                return Fog[(int)pPosition.Y, (int)pPosition.X] == 1;
            }
            return false;
        }

        private void _Mask(Vector2 pPosition)
        {
            int mask = 0;
            if (isInMap(pPosition))
            {
                if (Fog[(int)pPosition.Y, (int)pPosition.X] == 1)
                    return;
                if (_getFog(pPosition + new Vector2(0, -1)))
                {
                    mask += 1;
                }
                if (_getFog(pPosition + new Vector2(1, 0)))
                {
                    mask += 2;
                }
                if (_getFog(pPosition + new Vector2(0, 1)))
                {
                    mask += 4;
                }
                if (_getFog(pPosition + new Vector2(-1, 0)))
                {
                    mask += 8;
                }
                Bitmask[(int)pPosition.Y, (int)pPosition.X] = mask;
            }
        }

        private void _Discover(Vector2 pPosition)
        {
            if (isInMap(pPosition))
                Fog[(int)pPosition.Y, (int)pPosition.X] = 0;
            _Mask(pPosition + new Vector2(0, -1));
            _Mask(pPosition + new Vector2(1, -1));
            _Mask(pPosition + new Vector2(1, 0));
            _Mask(pPosition + new Vector2(1, 1));
            _Mask(pPosition + new Vector2(0, 1));
            _Mask(pPosition + new Vector2(-1, 1));
            _Mask(pPosition + new Vector2(-1, 0));
            _Mask(pPosition + new Vector2(-1, -1));
        }

        public void Discover(Vector2 pPosition)
        {
            _Discover(pPosition);
            _Discover(pPosition + new Vector2(0, -1));
            _Discover(pPosition + new Vector2(1, -1));
            _Discover(pPosition + new Vector2(1, 0));
            _Discover(pPosition + new Vector2(1, 1));
            _Discover(pPosition + new Vector2(0, 1));
            _Discover(pPosition + new Vector2(-1, 1));
            _Discover(pPosition + new Vector2(-1, 0));
            _Discover(pPosition + new Vector2(-1, -1));
        }
    }
}
