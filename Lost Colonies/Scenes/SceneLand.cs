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
    internal class SceneLand : GCSceneBase
    {
        double Timer;

        public SceneLand()
        {
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update();

            Timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (Timer >= 3)
            {
                Timer = 0;
                if (getProperty("mode") == "arrival")
                    GCServiceLocator.GetService<GCSceneManager>().StartScene("colony");
                else
                    GCServiceLocator.GetService<GCSceneManager>().StartScene("dashboard");
            }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");
            string Message = "";
            if (getProperty("mode") == "arrival")
                Message = "Landing";
            else
                Message = "Departure";
            spriteBatch.DrawString(font, Message + "... please wait...", new Vector2(1, 1), Color.White);
        }

        public override void DrawGUI()
        {

        }
    }
}
