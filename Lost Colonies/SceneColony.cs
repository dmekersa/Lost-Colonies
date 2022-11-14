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
    internal class SceneColony : GCSceneBase
    {
        public SceneColony()
        {
        }

        public override void Start()
        {
            controlManager.Reset();
            controlManager.SetMethodKey("out", Microsoft.Xna.Framework.Input.Keys.O);
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update();

            if (controlManager.Pressed("out"))
                GCServiceLocator.GetService<GCSceneManager>().StartScene("wrapup");

            base.Update(gameTime);
        }

        public override void Draw()
        {
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");
            spriteBatch.DrawString(font, "Colony - [O] Go out", new Vector2(1, 1), Color.White);
        }

        public override void DrawGUI()
        {

        }
    }
}
