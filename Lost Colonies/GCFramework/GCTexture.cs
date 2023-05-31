using Microsoft.Xna.Framework.Graphics;

namespace Gamecodeur
{
    class GCTexture
    {
        public Texture2D texture { get; private set; }
        public GCTexture(string psTextureName)
        {
            texture = GCServiceLocator.GetService<GCAssetManager>().GetTexture(psTextureName);
        }
    }
}
