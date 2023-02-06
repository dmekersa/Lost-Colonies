using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lost_Colonies
{
    internal class SceneDashboard : GCSceneBase
    {
        private GameState _gameState;

        public SceneDashboard()
        {
            _gameState = GCServiceLocator.GetService<GameState>();
        }

        public override void Focus()
        {
            controlManager.Reset();
            controlManager.SetMethodKey("galaxy", Microsoft.Xna.Framework.Input.Keys.G);
            controlManager.SetMethodKey("explore", Microsoft.Xna.Framework.Input.Keys.E);
            controlManager.SetMethodKey("warp", Microsoft.Xna.Framework.Input.Keys.W);
            base.Focus();
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
            spriteBatch.DrawString(font, "Current Planet: " + _gameState.currentPlanet.Name, new Vector2(1, 16), Color.White);

            if (_gameState.targetPlanet != null)
            {
                spriteBatch.DrawString(font, "Target Planet: " + _gameState.targetPlanet.Name, new Vector2(1, 32), Color.White);
            }
        }

        public override void DrawGUI()
        {

        }
    }
}
