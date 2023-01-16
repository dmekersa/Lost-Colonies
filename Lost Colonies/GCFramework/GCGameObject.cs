using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gamecodeur
{
    public class GCGameObject
    {
        public float x;
        public float y;

        public Vector2 Position
        {
            get
            {
                return new Vector2(x, y);
            }

            set
            {
                x = value.X;
                y = value.Y;
            }
        }

        protected SpriteBatch spriteBatch { get; set; }

        public GCGameObject()
        {
            spriteBatch = GCServiceLocator.GetService<SpriteBatch>();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw()
        {

        }
    }
}
