using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lost_Colonies
{
    public class GameLost : Game
    {
        private GraphicsDeviceManager _graphics;
        private GCGame MainGame;
        private SpriteBatch _spriteBatch;

        public GameLost()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            GCServiceLocator.RegisterService<ContentManager>(Content);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GCServiceLocator.RegisterService<SpriteBatch>(_spriteBatch);

            // TODO: use this.Content to load your game content here
            MainGame = new GCGame();

            MainGame.SceneManager.AddScene("menu", new SceneMenu());
            MainGame.SceneManager.AddScene("test", new SceneTest());
            MainGame.SceneManager.AddScene("planetlist", new ScenePlanetList());

            MainGame.SceneManager.StartScene("planetlist");

            MainGame.FontManager.AddFont("fontSmall");
            MainGame.FontManager.AddFont("fontMedium");
            MainGame.FontManager.AddFont("fontBig");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            MainGame.SceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            MainGame.SceneManager.Draw(gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}