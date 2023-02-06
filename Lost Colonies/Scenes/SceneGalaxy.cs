using Gamecodeur;
using Lost_Colonies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

public class SceneGalaxy : GCSceneBase
{
    private Galaxy _theGalaxy;
    private Texture2D _texSelectMap;
    private Vector2 _posSelectMap;
    private Planet _nearestPlanet = null;
    private GameState _gameState;

    public override void Load()
    {
        _gameState = GCServiceLocator.GetService<GameState>();

        // sélecteur de planète
        _texSelectMap = GCServiceLocator.GetService<ContentManager>().Load<Texture2D>("selectMap");
        _posSelectMap = new Vector2(_gameState.currentPlanet.Position.X, _gameState.currentPlanet.Position.Y);

        Debug.WriteLine("Load Galaxy");

        _theGalaxy = GCServiceLocator.GetService<GameState>().theGalaxy;
    }

    public override void Stop()
    {

    }

    public override void Focus()
    {
        controlManager.Reset();
        controlManager.SetMethodKey("new", Microsoft.Xna.Framework.Input.Keys.Space);

        controlManager.SetMethodKey("right", Microsoft.Xna.Framework.Input.Keys.Right);
        controlManager.SetMethodKey("down", Microsoft.Xna.Framework.Input.Keys.Down);
        controlManager.SetMethodKey("left", Microsoft.Xna.Framework.Input.Keys.Left);
        controlManager.SetMethodKey("up", Microsoft.Xna.Framework.Input.Keys.Up);

        controlManager.SetMethodKey("back", Microsoft.Xna.Framework.Input.Keys.Escape);
        controlManager.SetMethodKey("select", Microsoft.Xna.Framework.Input.Keys.Space);
    }

    public override void Update(GameTime gameTime)
    {
        controlManager.Update();

        if (controlManager.Pressed("back") || controlManager.Pressed("select"))
        {
            GCServiceLocator.GetService<GCSceneManager>().StartScene("dashboard");
        }

        if (controlManager.Pressed("new"))
        {
            _theGalaxy.Generate(256, 256 * 2, 256);
            _posSelectMap = new Vector2(0, 0);
        }

        bool bStick = true;
        if (controlManager.Down("right"))
        {
            _posSelectMap.X++;
            bStick = false;
        }
        if (controlManager.Down("left"))
        {
            _posSelectMap.X--;
            bStick = false;
        }
        if (controlManager.Down("down"))
        {
            _posSelectMap.Y++;
            bStick = false;
        }
        if (controlManager.Down("up"))
        {
            _posSelectMap.Y--;
            bStick = false;
        }

        if (bStick)
        {
            // Rechercher la planete la + proche de la croix
            int minDistance = int.MaxValue;
            Point posSelect = new Point((int)_posSelectMap.X + 1, (int)_posSelectMap.Y + 1);
            foreach (Planet planet in _theGalaxy.planets)
            {
                double distance = Utils.GetDistance((double)posSelect.X, (double)posSelect.Y, (double)planet.Position.X, (double)planet.Position.Y);
                if (distance < minDistance)
                {
                    _nearestPlanet = planet;
                    minDistance = (int)distance;
                }
            }

            if (_nearestPlanet != null)
            {
                _posSelectMap.X = _nearestPlanet.Position.X - 1;
                _posSelectMap.Y = _nearestPlanet.Position.Y - 1;

                if (_nearestPlanet != _gameState.targetPlanet && _nearestPlanet != _gameState.currentPlanet)
                {
                    _gameState.SetTargetPlanet(_nearestPlanet);
                }
            }
        }

        base.Update(gameTime);
    }

    public override void Draw()
    {
        _theGalaxy.Draw(spriteBatch);

        spriteBatch.Draw(_texSelectMap, _posSelectMap, Color.White);


        base.Draw();
    }

    public override void DrawGUI()
    {
        if (_nearestPlanet != null)
        {
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontBig");
            spriteBatch.DrawString(font, _nearestPlanet.Name, new Vector2(5, (_theGalaxy.Height * 2) + 2), Color.White);
        }

    }
}
