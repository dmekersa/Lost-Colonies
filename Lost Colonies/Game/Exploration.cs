using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Lost_Colonies
{
    class Exploration : GCGameObject
    {
        private SurfaceShip _surfaceShip;
        private SurfaceColony _surfaceColony;

        private SurfaceMap _surface;
        private Minimap _minimap;

        private GCSprite _sprTiles;
        private GCSprite _sprDestination;

        private const float _CAMSPEED = 2f;
        private Vector2 _Camera;

        private MouseState _oldms;

        private Point _mapDestination;

        private GCTimer _timerBlink;
        private bool _bDestinationVisible;

        private GCScreenInfo _screenInfo;
        private GameState _gameState;

        public Exploration()
        {
            _gameState = GCServiceLocator.GetService<GameState>();
            _screenInfo = GCServiceLocator.GetService<GCScreenInfo>();

            GCTexture texTileset = new GCTexture("gfx/tiles");
            _sprTiles = new GCSprite(spriteBatch, texTileset.texture, 16, 16);
            _sprTiles.isCentered = false;
            _sprTiles.frame = 0;

            _surfaceShip = new SurfaceShip(spriteBatch, texTileset.texture, 16, 16);
            _surfaceShip.isCentered = true;
            _surfaceShip.frame = 2;

            _surfaceColony = new SurfaceColony(spriteBatch, texTileset.texture, 32, 32);
            _surfaceColony.isCentered = true;
            _surfaceColony.frame = 8;

            _sprDestination = new GCSprite(spriteBatch, texTileset.texture, 16, 16);
            _sprDestination.isCentered = false;
            _sprDestination.frame = 3;

            _Camera = new Vector2();

            _timerBlink = new GCTimer(0.2f);
        }

        public void Reset()
        {
            _Camera = new Vector2();

            /*
            _surfaceShip.Position = new Vector2(_surfaceShip.largeurFrame, _surfaceShip.hauteurFrame);
            _surfaceShip.MapDestination = _surfaceShip.Position;
            _surfaceShip.MapPosition = _surfaceShip.Position;
            */

            _surfaceShip.Position = new Vector2(((SurfaceMap.MAPW / 2) * SurfaceMap.TILEW) + _surfaceShip.largeurFrame / 2, ((SurfaceMap.MAPH / 2) * SurfaceMap.TILEH) + _surfaceShip.hauteurFrame / 2);
            _surfaceShip.MapDestination = _surfaceShip.Position;
            _surfaceShip.MapPosition = _surfaceShip.Position;

            SurfaceMap sm = _gameState.currentPlanet.surfaceMap;
            _surfaceColony.Position = new Vector2(sm.basePosition.X * SurfaceMap.TILEW, sm.basePosition.Y * SurfaceMap.TILEH);
            _surfaceColony.MapPosition = _surfaceColony.Position;

            // On centre la camera sur le centre de la map
            _Camera.X = 0 - ((SurfaceMap.MAPW / 2) * SurfaceMap.TILEW);
            _Camera.Y = 0 - ((SurfaceMap.MAPH / 2) * SurfaceMap.TILEH);
            _Camera.X += (_screenInfo.GetViewPort().X / 2) - SurfaceMap.TILEW;
            _Camera.Y += (_screenInfo.GetViewPort().Y / 2) - SurfaceMap.TILEH;

            _mapDestination = new Point(-1, -1);

            _bDestinationVisible = true;

            _surface = sm;

            // On calcule la surface visible exprimée en colonnes et lignes
            int nbCol = (int)(_screenInfo.GetViewPort().X / SurfaceMap.TILEW);
            int nbRow = (int)(_screenInfo.GetViewPort().Y / SurfaceMap.TILEH);

            _minimap = new Minimap((int)_screenInfo.GetViewPort().X - 65, 1, 64, 64, nbCol, nbRow, _surface.Map, _surface.Fog);
            _minimap.SetColor(0, new Color(0, 255, 0));
            _minimap.SetColor(1, new Color(120, 120, 120));

            UpdateMinimap();
        }

        private void UpdateMinimap()
        {
            _minimap.ResetObjects();
            // Position du vaisseau en colonnes/lignes
            Vector2 pShip = new Vector2();
            pShip = _surfaceShip.MapPosition / new Vector2(16, 16);
            _minimap.AddObject(new Point((int)pShip.X, (int)pShip.Y), new Color(255, 255, 255));
            pShip = _surfaceColony.MapPosition / new Vector2(16, 16);
            _minimap.AddObject(new Point((int)pShip.X, (int)pShip.Y), new Color(255, 0, 255));

            int nbCol = (int)(_screenInfo.GetViewPort().X / SurfaceMap.TILEW);
            int nbRow = (int)(_screenInfo.GetViewPort().Y / SurfaceMap.TILEH);
            _minimap.SetUpperLeft((int)Math.Abs(_Camera.X) / SurfaceMap.TILEW, (int)Math.Abs(_Camera.Y) / SurfaceMap.TILEH); ;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            Vector2 msPos = new Vector2(ms.X / _screenInfo.RatioX, ms.Y / _screenInfo.RatioY);

            if (ms.LeftButton == ButtonState.Pressed && _oldms.LeftButton == ButtonState.Released)
            {
                _mapDestination.X = (int)(msPos.X - _Camera.X) / 16;
                _mapDestination.Y = (int)(msPos.Y - _Camera.Y) / 16;

                _surfaceShip.MapDestination.X = (_mapDestination.X * 16) + 16 / 2;
                _surfaceShip.MapDestination.Y = (_mapDestination.Y * 16) + 16 / 2;
            }

            float distX = (_screenInfo.GetViewPort().X / 2) - _surfaceShip.x;
            float distY = (_screenInfo.GetViewPort().Y / 2) - _surfaceShip.y;

            float angle = (float)Utils.GetAngle(_Camera, new Vector2(_Camera.X + distX, _Camera.Y + distY));

            //float dist = (float)Utils.GetDistance(Camera, new Vector2(Camera.X + distX, Camera.Y + distY));

            float vx = 1f * (float)Math.Cos(angle);
            float vy = 1f * (float)Math.Sin(angle);

            /*
            if (Math.Abs(dist) > 5)
            {
                if (vx < 0 && Math.Abs(Camera.X) < (SurfaceMap.MAPW * dummySprite.largeurFrame) - _screenInfo.GetViewPort().X)
                    Camera.X += vx;

                if (vy > 0 && Camera.X < 0)
                    Camera.X += vx;

                if (vy < 0 && Math.Abs(Camera.Y) < (SurfaceMap.MAPH * dummySprite.hauteurFrame) - _screenInfo.GetViewPort().Y)
                    Camera.Y += vy;

                if (vy > 0 && Camera.Y < 0)
                    Camera.Y += vy;
            }
            */

            if ((ms.X > _screenInfo.GetScreenSize().X - 5) && Math.Abs(_Camera.X) < (SurfaceMap.MAPW * _sprTiles.largeurFrame) - _screenInfo.GetViewPort().X)
            //if ((distX < 5) && Math.Abs(Camera.X) < (SurfaceMap.MAPW * dummySprite.largeurFrame) - screenInfo.GetViewPort().X)
            {
                _Camera.X -= _CAMSPEED;
            }
            else if (ms.X < 5 && _Camera.X < 0)
            //if ((distX > 5) && Camera.X < 0)
            {
                _Camera.X += _CAMSPEED;
            }
            if ((ms.Y > _screenInfo.GetScreenSize().Y - 5) && Math.Abs(_Camera.Y) < (SurfaceMap.MAPH * _sprTiles.hauteurFrame) - _screenInfo.GetViewPort().Y)
            //if ((distY < 5) && Math.Abs(Camera.Y) < (SurfaceMap.MAPH * dummySprite.hauteurFrame) - screenInfo.GetViewPort().Y)
            {
                _Camera.Y -= _CAMSPEED;
            }
            else if (ms.Y < 5 && _Camera.Y < 0)
            //if ((distY > 5) && Camera.Y < 0)
            {
                _Camera.Y += _CAMSPEED;
            }

            _surfaceShip.Update(_Camera, gameTime);

            _surfaceColony.Update(_Camera, gameTime);

            // Passe à discover la position en ligne/colonne, cad sa position en pixels divisée par 16
            _surface.Discover(_surfaceShip.MapPosition / new Vector2(16, 16));

            _oldms = ms;

            _timerBlink.Update(gameTime);
            if (_timerBlink.Ended)
            {
                _bDestinationVisible = !_bDestinationVisible;
            }

            UpdateMinimap();

            _minimap.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw()
        {
            for (int l = 0; l < SurfaceMap.MAPH; l++)
            {
                for (int c = 0; c < SurfaceMap.MAPW; c++)
                {
                    // Tuile de surface
                    _sprTiles.x = (int)((c * 16) + _Camera.X);
                    _sprTiles.y = (int)(l * 16 + _Camera.Y);
                    if (_surface.Fog[l, c] != 1)
                    {
                        _sprTiles.frame = _surface.Map[l, c];
                        _sprTiles.Draw();
                    }

                    // Tuile de fog
                    //SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");
                    //spriteBatch.DrawString(font, _surface.Bitmask[l, c].ToString(), _sprTiles.Position, Color.White);
                    _sprTiles.frame = 8 + _surface.Bitmask[l, c];
                    _sprTiles.Draw();

                    // Tuile de sélection
                    if (l == _mapDestination.Y && c == _mapDestination.X && _bDestinationVisible && !_surfaceShip.isArrived)
                    {
                        _sprDestination.Position = _sprTiles.Position;
                        _sprDestination.Draw();
                    }
                }

            }

            Vector2 cellColony = _surfaceColony.MapPosition / new Vector2(16, 16);
            if (_surface.Fog[(int)cellColony.Y, (int)cellColony.X] != 1)
                _surfaceColony.Draw();
            _surfaceShip.Draw();

            _minimap.Draw();

            base.Draw();
        }
    }
}
