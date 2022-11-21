using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost_Colonies
{
    internal class SceneDashboard : GCSceneBase
    {
        public SceneDashboard()
        {
        }

        public override void Start()
        {
            controlManager.Reset();
            controlManager.SetMethodKey("galaxy", Microsoft.Xna.Framework.Input.Keys.G);
            controlManager.SetMethodKey("explore", Microsoft.Xna.Framework.Input.Keys.E);
            controlManager.SetMethodKey("warp", Microsoft.Xna.Framework.Input.Keys.W);
            base.Start();
        }



        public override void Update(GameTime gameTime)
        {
            controlManager.Update();

            if (controlManager.Pressed("galaxy"))
                GCServiceLocator.GetService<GCSceneManager>().StartScene("galaxy");
            if (controlManager.Pressed("warp"))
                GCServiceLocator.GetService<GCSceneManager>().StartScene("warp");
            if (controlManager.Pressed("explore"))
                GCServiceLocator.GetService<GCSceneManager>().StartScene("explore");

            base.Update(gameTime);
        }

        public override void Draw()
        {
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");
            spriteBatch.DrawString(font, "Dashboard - [G] Galaxy [W] Warp [E] Explore", new Vector2(1, 1), Color.White);
        }

        public override void DrawGUI()
        {

        }
    }
}
