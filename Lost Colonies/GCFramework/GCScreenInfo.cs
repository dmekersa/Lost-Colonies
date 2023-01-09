using Microsoft.Xna.Framework;

namespace Gamecodeur
{
    public class GCScreenInfo
    {
        private GraphicsDeviceManager _graphics;
        private Rectangle _viewPort;

        public GCScreenInfo(GraphicsDeviceManager pGraphics, Rectangle pViewPort)
        {
            _graphics = pGraphics;
            _viewPort = pViewPort;
        }

        public Vector2 GetScreenSize()
        {
            return new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        public Vector2 GetViewPort()
        {
            return new Vector2(_viewPort.Width, _viewPort.Height);
        }
    }
}
