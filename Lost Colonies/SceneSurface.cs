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
    internal class SceneSurface : GCSceneBase
    {
        public SceneSurface()
        {
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
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");
            spriteBatch.DrawString(font, "Surface - [L] Land [O] Back to orbit", new Vector2(1, 1), Color.White);
        }

        public override void DrawGUI()
        {

        }
    }
}
