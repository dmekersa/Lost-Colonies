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
    internal class SceneWrapUp : GCSceneBase
    {
        public SceneWrapUp()
        {
        }

        public override void Focus()
        {
            controlManager.Reset();
            controlManager.SetMethodKey("surface", Microsoft.Xna.Framework.Input.Keys.S);
            controlManager.SetMethodKey("orbit", Microsoft.Xna.Framework.Input.Keys.O);
            base.Focus();
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update();

            if (controlManager.Pressed("surface"))
            {
                GCServiceLocator.GetService<GCSceneManager>().SetProperty("land", "mode", "departure");
                GCServiceLocator.GetService<GCSceneManager>().StartScene("land");
            }
            if (controlManager.Pressed("orbit"))
            {
                GCServiceLocator.GetService<GCSceneManager>().StartScene("dashboard");
            }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");
            spriteBatch.DrawString(font, "Wrap up - [S] Back to surface [O] Back to orbit", new Vector2(1, 1), Color.White);
        }

        public override void DrawGUI()
        {

        }
    }
}
