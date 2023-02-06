using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lost_Colonies
{
    internal class SceneWarp : GCSceneBase
    {
        double Timer;
        private GameState _gameState;

        public SceneWarp()
        {
            _gameState = GCServiceLocator.GetService<GameState>();
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update();

            Timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (Timer >= 3)
            {
                Timer = 0;

                _gameState.SetCurrentPlanet(_gameState.targetPlanet);
                _gameState.SetTargetPlanet(null);

                GCServiceLocator.GetService<GCSceneManager>().StartScene("dashboard");
            }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");
            spriteBatch.DrawString(font, "Warp to " + _gameState.targetPlanet.Name + " ... please wait...", new Vector2(1, 1), Color.White);
        }

        public override void DrawGUI()
        {

        }
    }
}
