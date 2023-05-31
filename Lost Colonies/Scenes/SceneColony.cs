using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lost_Colonies
{
    internal class SceneColony : GCSceneBase
    {
        private GameState _gameState;
        private ColonyMap _colonyMap;

        public SceneColony()
        {
            _gameState = GCServiceLocator.GetService<GameState>();
        }

        public override void Focus()
        {
            controlManager.Reset();
            controlManager.SetMethodKey("out", Microsoft.Xna.Framework.Input.Keys.O);
            controlManager.SetMethodKey("mm_up", Microsoft.Xna.Framework.Input.Keys.Up);
            controlManager.SetMethodKey("mm_dn", Microsoft.Xna.Framework.Input.Keys.Down);

            _colonyMap = new ColonyMap(_gameState.currentPlanet.surfaceMap.planetBase);
            _colonyMap.x = 5;
            _colonyMap.y = 20;

            base.Focus();
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update();

            if (controlManager.Pressed("out"))
                GCServiceLocator.GetService<GCSceneManager>().StartScene("wrapup");

            if (controlManager.Pressed("mm_up"))
                _colonyMap.SelectUp();

            if (controlManager.Pressed("mm_dn"))
                _colonyMap.SelectDown();

            _colonyMap.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw()
        {
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");
            spriteBatch.DrawString(font, "Colony - [O] Go out", new Vector2(1, 1), Color.White);

            _colonyMap.Draw();
        }

        public override void DrawGUI()
        {

        }
    }
}
