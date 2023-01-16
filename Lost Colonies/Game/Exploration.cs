using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Lost_Colonies
{
    class Exploration : GCGameObject
    {
        private SurfaceShip _surfaceShip;

        private SurfaceMap surface = new SurfaceMap();

        GCSprite dummySprite;
        const float CAMSPEED = 2f;
        Vector2 Camera;

        MouseState oldms;

        public Exploration()
        {
            GCScreenInfo screenInfo = GCServiceLocator.GetService<GCScreenInfo>();

            GCTexture texTileset = new GCTexture("gfx/tiles");
            dummySprite = new GCSprite(spriteBatch, texTileset.texture, 16, 16);
            dummySprite.isCentered = false;
            dummySprite.frame = 0;

            _surfaceShip = new SurfaceShip(spriteBatch, texTileset.texture, 16, 16);
            _surfaceShip.isCentered = true;
            _surfaceShip.frame = 2;
            //_surfaceShip.MapPosition.X = (float)Math.Floor(SurfaceMap.MAPW / 2.0) * 16;
            //_surfaceShip.MapPosition.Y = (float)Math.Floor(SurfaceMap.MAPH / 2.0) * 16;

            //Camera = new Vector2(-_surfaceShip.MapPosition.X, -_surfaceShip.MapPosition.Y);
            Camera = new Vector2();

            //Camera.X += screenInfo.GetViewPort().X / 2;
            //Camera.Y += screenInfo.GetViewPort().Y / 2;

        }

        public override void Update(GameTime gameTime)
        {

            GCScreenInfo screenInfo = GCServiceLocator.GetService<GCScreenInfo>();

            MouseState ms = Mouse.GetState();
            Vector2 msPos = new Vector2(ms.X / screenInfo.RatioX, ms.Y / screenInfo.RatioY);

            if (ms.LeftButton == ButtonState.Pressed && oldms.LeftButton == ButtonState.Released)
            {
                int col = (int)(msPos.X - Camera.X) / 16;
                int row = (int)(msPos.Y - Camera.Y) / 16;

                _surfaceShip.MapDestination.X = (col * 16) + 16 / 2;
                _surfaceShip.MapDestination.Y = (row * 16) + 16 / 2;
            }

            float distX = (screenInfo.GetViewPort().X / 2) - _surfaceShip.x;
            float distY = (screenInfo.GetViewPort().Y / 2) - _surfaceShip.y;

            float angle = (float)Utils.GetAngle(Camera, new Vector2(Camera.X + distX, Camera.Y + distY));

            float dist = (float)Utils.GetDistance(Camera, new Vector2(Camera.X + distX, Camera.Y + distY));

            float vx = 1f * (float)Math.Cos(angle);
            float vy = 1f * (float)Math.Sin(angle);

            if (Math.Abs(dist) > 5)
            {
                if (vx < 0 && Math.Abs(Camera.X) < (SurfaceMap.MAPW * dummySprite.largeurFrame) - screenInfo.GetViewPort().X)
                    Camera.X += vx;

                if (vy > 0 && Camera.X < 0)
                    Camera.X += vx;

                if (vy < 0 && Math.Abs(Camera.Y) < (SurfaceMap.MAPH * dummySprite.hauteurFrame) - screenInfo.GetViewPort().Y)
                    Camera.Y += vy;

                if (vy > 0 && Camera.Y < 0)
                    Camera.Y += vy;
            }

            /*
            //if ((ms.X > screenInfo.GetScreenSize().X - 5 || _surfaceShip.x > screenInfo.GetViewPort().X / 2) && Math.Abs(Camera.X) < (SurfaceMap.MAPW * dummySprite.largeurFrame) - screenInfo.GetViewPort().X)
            if ((distX < 5) && Math.Abs(Camera.X) < (SurfaceMap.MAPW * dummySprite.largeurFrame) - screenInfo.GetViewPort().X)
            {
                Camera.X -= CAMSPEED;
            }
            //if ((ms.X < 5 || _surfaceShip.x < screenInfo.GetViewPort().X / 2) && Camera.X < 0)
            if ((distX > 5) && Camera.X < 0)
            {
                Camera.X += CAMSPEED;
            }
            //if ((ms.Y > screenInfo.GetScreenSize().Y - 5 || _surfaceShip.y > screenInfo.GetViewPort().Y / 2) && Math.Abs(Camera.Y) < (SurfaceMap.MAPH * dummySprite.hauteurFrame) - screenInfo.GetViewPort().Y)
            if ((distY < 5) && Math.Abs(Camera.Y) < (SurfaceMap.MAPH * dummySprite.hauteurFrame) - screenInfo.GetViewPort().Y)
            {
                Camera.Y -= CAMSPEED;
            }
            //if ((ms.Y < 5 || _surfaceShip.y < screenInfo.GetViewPort().Y / 2) && Camera.Y < 0)
            if ((distY > 5) && Camera.Y < 0)
            {
                Camera.Y += CAMSPEED;
            }
            */

            _surfaceShip.Update(Camera, gameTime);

            //_surfaceShip.MapPosition.X += 0.5f;
            //_surfaceShip.MapPosition.Y += 0.5f;

            oldms = ms;

            base.Update(gameTime);
        }

        public override void Draw()
        {
            for (int l = 0; l < SurfaceMap.MAPH; l++)
                for (int c = 0; c < SurfaceMap.MAPW; c++)
                {
                    dummySprite.frame = surface.Map[l, c];
                    dummySprite.x = (int)((c * 16) + Camera.X);
                    dummySprite.y = (int)(l * 16 + Camera.Y);
                    dummySprite.Draw();
                }

            _surfaceShip.Draw();

            base.Draw();
        }
    }
}
