using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost_Colonies
{
    internal class SceneMenu : GCSceneBase
    {
        KeyboardState _oldKB = Keyboard.GetState();

        public override void Update(GameTime gameTime)
        {
            KeyboardState newKB = Keyboard.GetState();

            if (newKB.IsKeyDown(Keys.Space) && ! _oldKB.IsKeyDown(Keys.Space))
            {
                GCServiceLocator.GetService<GCSceneManager>().StartScene("test");
            }

            _oldKB = newKB;

            base.Update(gameTime);
        }


        public override void Draw()
        {
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");
            spriteBatch.DrawString(font, "Menu", new Vector2(1, 1), Color.White);
        }

        public override void DrawGUI()
        {
            
        }
    }
}
