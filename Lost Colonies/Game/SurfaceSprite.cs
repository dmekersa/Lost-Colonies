using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lost_Colonies
{
    class SurfaceSprite : GCSprite
    {
        public Vector2 MapPosition;
        public Vector2 Camera;

        public SurfaceSprite(SpriteBatch pSpriteBatch, Texture2D pTexture, int pLargeurFrame = 0, int pHauteurFrame = 0) : base(pSpriteBatch, pTexture, pLargeurFrame, pHauteurFrame)
        {
            MapPosition = new Vector2(Position.X, Position.Y);
            isPixel = true;
        }

        public void Update(Vector2 pCamera, GameTime gameTime)
        {
            Camera = pCamera;
            this.Update(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            x = MapPosition.X + Camera.X;
            y = MapPosition.Y + Camera.Y;
            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
