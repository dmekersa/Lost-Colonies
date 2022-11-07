using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamecodeur;

namespace LD36
{
    class GCTileSprite : GCSprite
    {
        public string currentDirection { get; private set; }
        public string lastDirection { get; private set; }
        public int mapX { get; set; }
        public int mapY { get; set; }
        public bool isStopped { get; private set; }
        public int gid { get; set; }

        public GCTileSprite(SpriteBatch pSpriteBatch, Texture2D pTexture, int pLargeurFrame, int pHauteurFrame, bool pisLoop = true) :
            base( pSpriteBatch,  pTexture,  pLargeurFrame,  pHauteurFrame)
        {
            isCentered = false;
            isStopped = true;
            currentDirection = "";
            gid = -1;
        }

        public void setPositionInMap(int pX, int pY)
        {
            mapX = pX;
            mapY = pY;
            this.x = pX * this.largeurFrame;
            this.y = pY * this.hauteurFrame;
        }

        public void Stop()
        {
            this.x = (float)Math.Floor(x);
            this.y = (float)Math.Floor(y);
            this.Velocity.X = 0;
            this.mapX = (int)this.x / this.largeurFrame;
            this.Velocity.Y = 0;
            this.mapY = (int)this.y / this.hauteurFrame;
            currentDirection = "";
            isStopped = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            float realX;
            float realY;

            if (isCentered)
            {
                realX = x - largeurFrame / 2;
                realY = y - hauteurFrame / 2;
            }
            else
            {
                realX = x;
                realY = y;
            }

            // Teste si on a atteint une intersection de cellule
            if (Math.Abs(this.Velocity.X) > 0)
            {
                if (realX % this.largeurFrame < 0.5)
                {
                    Stop();
                }
            }
            if (Math.Abs(this.Velocity.Y) > 0)
            {
                if (realY % this.hauteurFrame < 0.5)
                {
                    Stop();
                }
            }
        }

        public override void Draw()
        {
            if (this.lastDirection == "left")
            {
                effect = SpriteEffects.FlipHorizontally;
            }
            else if (this.lastDirection == "right")
            {
                effect = SpriteEffects.None;
            }
            base.Draw();
        }

        public void Move(string psDirection)
        {
            if (currentDirection != "") return;
            isStopped = false;

            switch(psDirection)
            {
                case "up":
                    this.Velocity.Y = 1 - speed;
                    break;
                case "right":
                    this.Velocity.X = speed;
                    break;
                case "down":
                    this.Velocity.Y = speed;
                    break;
                case "left":
                    this.Velocity.X = 1 - speed;
                    break;
            }
            currentDirection = psDirection;
            lastDirection = currentDirection;
        }

    }
}
