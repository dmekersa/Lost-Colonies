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
    internal class SceneTakeOff : GCSceneBase
    {
        double Timer;

        public SceneTakeOff()
        {
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update();

            Timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (Timer >= 3)
            {
                Timer = 0;
                GCServiceLocator.GetService<GCSceneManager>().StartScene("surface");
            }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");
            spriteBatch.DrawString(font, "Take off... please wait...", new Vector2(1, 1), Color.White);
        }

        public override void DrawGUI()
        {

        }
    }
}
