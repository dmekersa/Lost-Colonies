using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Lost_Colonies
{
    class Exploration : GCGameObject
    {
        private SurfaceShip _surfaceShip;

        private SurfaceMap _surface;

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

            _Camera.X = 0 - ((SurfaceMap.MAPW / 2) * SurfaceMap.TILEW);
            _Camera.Y = 0 - ((SurfaceMap.MAPH / 2) * SurfaceMap.TILEH);
            _Camera.X += (_screenInfo.GetViewPort().X / 2) - SurfaceMap.TILEW;
            _Camera.Y += (_screenInfo.GetViewPort().Y / 2) - SurfaceMap.TILEH;

            _mapDestination = new Point(-1, -1);

            _bDestinationVisible = true;

            _surface = _gameState.currentPlanet.surfaceMap;
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

            _surface.Discover(_surfaceShip.MapPosition / new Vector2(16, 16));

            _oldms = ms;

            _timerBlink.Update(gameTime);
            if (_timerBlink.Ended)
            {
                _bDestinationVisible = !_bDestinationVisible;
            }

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

            _surfaceShip.Draw();

            base.Draw();
        }
    }
}
