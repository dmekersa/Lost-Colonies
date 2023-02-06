using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lost_Colonies
{
    public class GameLost : Game
    {
        private GraphicsDeviceManager _graphics;
        private GCGame MainGame;
        private SpriteBatch _spriteBatch;
        private Rectangle ViewPort = new Rectangle(0, 0, 480, 272);
        private int ScreenWidth;
        private int ScreenHeight;
        private RenderTarget2D _renderTarget;
        private GCAssetManager _assetManager;
        private GameState _gameState;

        public GameLost()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            ScreenWidth = ViewPort.Width * 2;
            ScreenHeight = ViewPort.Height * 2;

            GCServiceLocator.RegisterService<ContentManager>(Content);

            _assetManager = new GCAssetManager();
            GCServiceLocator.RegisterService(_assetManager);

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();


            GCScreenInfo screenInfo = new GCScreenInfo(_graphics, ViewPort);
            GCServiceLocator.RegisterService<GCScreenInfo>(screenInfo);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GCServiceLocator.RegisterService(_spriteBatch);
            GCServiceLocator.RegisterService(GraphicsDevice);

            // Création de la galaxie
            _gameState = new GameState();
            GCServiceLocator.RegisterService<GameState>(_gameState);

            // TODO: use this.Content to load your game content here
            MainGame = new GCGame();

            MainGame.SceneManager.AddScene("splash", new SceneSplash());
            MainGame.SceneManager.AddScene("menu", new SceneMenu());
            MainGame.SceneManager.AddScene("galaxy", new SceneGalaxy());
            MainGame.SceneManager.AddScene("dashboard", new SceneDashboard());
            MainGame.SceneManager.AddScene("warp", new SceneWarp());
            MainGame.SceneManager.AddScene("explore", new SceneExplore());
            MainGame.SceneManager.AddScene("surface", new SceneSurface());
            MainGame.SceneManager.AddScene("land", new SceneLand());
            MainGame.SceneManager.AddScene("colony", new SceneColony());
            MainGame.SceneManager.AddScene("wrapup", new SceneWrapUp());
            //MainGame.SceneManager.AddScene("takeoff", new SceneTakeOff());

            // For tests
            MainGame.SceneManager.AddScene("test", new SceneTest());
            MainGame.SceneManager.AddScene("planetlist", new ScenePlanetList());

            MainGame.SceneManager.StartScene("splash");

            MainGame.FontManager.AddFont("fontSmall");
            MainGame.FontManager.AddFont("fontMedium");
            MainGame.FontManager.AddFont("fontBig");

            // Génération de notre canvas
            _renderTarget = new RenderTarget2D(GraphicsDevice, ViewPort.Width, ViewPort.Height);
        }

        protected override void Update(GameTime gameTime)
        {
            //            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //                Exit();

            // TODO: Add your update logic here
            MainGame.SceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);

            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            MainGame.SceneManager.Draw();

            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            _spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
            //_spriteBatch.Draw(_renderTarget, new Vector2(0,0), Color.White);
            _spriteBatch.End();

            _spriteBatch.Begin();
            MainGame.SceneManager.DrawGUI();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}