using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lost_Colonies
{
    internal class SceneSurface : GCSceneBase
    {
        private Exploration _exploration;

        public SceneSurface()
        {
            _exploration = new Exploration();
        }

        public override void Focus()
        {
            controlManager.Reset();
            controlManager.SetMethodKey("land", Microsoft.Xna.Framework.Input.Keys.L);
            controlManager.SetMethodKey("orbit", Microsoft.Xna.Framework.Input.Keys.O);

            _exploration.Reset();

            base.Focus();
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update();

            _exploration.Update(gameTime);

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
            _exploration.Draw();

            // GUI
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");
            spriteBatch.DrawString(font, "Surface - [L] Land [O] Back to orbit", new Vector2(1, 1), Color.White);

        }

        public override void DrawGUI()
        {

        }
    }
}
