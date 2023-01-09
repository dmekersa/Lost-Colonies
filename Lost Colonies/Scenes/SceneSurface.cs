using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Lost_Colonies
{
    internal class SceneSurface : GCSceneBase
    {
        SurfaceMap surface = new SurfaceMap();
        GCSprite dummySprite;
        const float CAMSPEED = 1f;
        Vector2 Camera;

        public SceneSurface()
        {
            GCTexture texTileset = new GCTexture("gfx/tiles");
            dummySprite = new GCSprite(GCServiceLocator.GetService<SpriteBatch>(), texTileset.texture, 16, 16);
            dummySprite.isCentered = false;
            dummySprite.frame = 0;

            Camera = new Vector2(0, 0);
        }

        public override void Start()
        {
            controlManager.Reset();
            controlManager.SetMethodKey("land", Microsoft.Xna.Framework.Input.Keys.L);
            controlManager.SetMethodKey("orbit", Microsoft.Xna.Framework.Input.Keys.O);
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update();

            GCScreenInfo screenInfo = GCServiceLocator.GetService<GCScreenInfo>();

            MouseState ms = Mouse.GetState();

            if (ms.X > screenInfo.GetScreenSize().X - 5 && Math.Abs(Camera.X) < (SurfaceMap.MAPW * dummySprite.largeurFrame) - screenInfo.GetViewPort().X)
            {
                Camera.X -= CAMSPEED;
            }
            if (ms.X < 5 && Camera.X < 0)
            {
                Camera.X += CAMSPEED;
            }
            if (ms.Y > screenInfo.GetScreenSize().Y - 5 && Math.Abs(Camera.Y) < (SurfaceMap.MAPH * dummySprite.hauteurFrame) - screenInfo.GetViewPort().Y)
            {
                Camera.Y -= CAMSPEED;
            }
            if (ms.Y < 5 && Camera.Y < 0)
            {
                Camera.Y += CAMSPEED;
            }

            if (controlManager.Pressed("land"))
            {
                GCServiceLocator.GetService<GCSceneManager>().SetProperty("land", "mode", "arrival");
                GCServiceLocator.GetService<GCSceneManager>().StartScene("land");
            }
            if (controlManager.Pressed("orbit"))
                GCServiceLocator.GetService<GCSceneManager>().StartScene("dashboard");

            base.Update(gameTime);
        }

        public override void Draw()
        {
            for (int l = 0; l < SurfaceMap.MAPH; l++)
                for (int c = 0; c < SurfaceMap.MAPW; c++)
                {
                    dummySprite.frame = surface.Map[l, c];
                    dummySprite.x = (c * 16) + Camera.X;
                    dummySprite.y = l * 16 + Camera.Y;
                    dummySprite.Draw();
                }

            // GUI
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");
            spriteBatch.DrawString(font, "Surface - [L] Land [D] Back to orbit", new Vector2(1, 1), Color.White);

        }

        public override void DrawGUI()
        {

        }
    }
}
