using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost_Colonies
{
    internal class SceneSplash : GCSceneBase
    {
        Texture2D texBlack;
        Texture2D texSplash;
        Single alpha;
        float speed = -0.007f;
        float zoom = 0;
        GCAssetManager assetManager;

        public SceneSplash()
        {
        }

        public override void Load()
        {
            texBlack = contentManager.Load<Texture2D>("whiteBG");
            texSplash = contentManager.Load<Texture2D>("SplashBG");

            base.Load();
        }

        public override void Focus()
        {
            alpha = 1f;

            controlManager.Reset();
            controlManager.SetMethodKey("skip", Microsoft.Xna.Framework.Input.Keys.Space);

            assetManager = GCServiceLocator.GetService<GCAssetManager>();

            string[] fileEntries = Directory.GetFiles("Content/gfx");

            assetManager.PreLoad(fileEntries);

            base.Focus();
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update();

            if (zoom < 0.2)
                zoom += 0.0005f;

            if ((speed<0 && alpha > 0) || (speed > 0 && alpha < 1))
                alpha += speed;

            assetManager.NextPreload();
            if (!assetManager.PreloadDone())
            {
                Debug.WriteLine("pas terminé");
            }

            if (alpha>1f && assetManager.PreloadDone())
            {
                alpha = 1;
                GCServiceLocator.GetService<GCSceneManager>().StartScene("menu");
            }

            if (alpha <= 0)
            {
                speed = -speed;
                alpha = 0;
            }

            if (controlManager.Pressed("skip") && assetManager.PreloadDone())
                GCServiceLocator.GetService<GCSceneManager>().StartScene("menu");

            base.Update(gameTime);
        }

        public override void Draw()
        {
            spriteBatch.Draw(texSplash, new Vector2(texBlack.Width / 2, texBlack.Height / 2), null, Color.White, 0, new Vector2(texBlack.Width/2, texBlack.Height/2), 1+zoom, SpriteEffects.None, 0);
            spriteBatch.Draw(texBlack, new Vector2(0, 0), Color.White * alpha);
        }

        public override void DrawGUI()
        {

        }
    }
}
