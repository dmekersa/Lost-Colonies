using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Lost_Colonies
{
    internal class SurfaceShip : SurfaceSprite
    {
        public Vector2 MapDestination;
        public bool isArrived;

        public SurfaceShip(SpriteBatch pSpriteBatch, Texture2D pTexture, int pLargeurFrame = 0, int pHauteurFrame = 0) : base(pSpriteBatch, pTexture, pLargeurFrame, pHauteurFrame)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (MapPosition != MapDestination)
            {
                isArrived = false;
                float angle = (float)Utils.GetAngle(MapPosition, MapDestination);
                rotation = angle;

                float dist = (float)Utils.GetDistance(MapPosition, MapDestination);

                float vx = 1f * (float)Math.Cos(angle);
                float vy = 1f * (float)Math.Sin(angle);

                MapPosition.X += vx;
                MapPosition.Y += vy;

                if (Math.Abs(dist) < 2)
                {
                    MapPosition.X = MapDestination.X;
                    MapPosition.Y = MapDestination.Y;
                }
            }
            else
            {
                isArrived = true;
            }

            base.Update(gameTime);
        }
    }
}
