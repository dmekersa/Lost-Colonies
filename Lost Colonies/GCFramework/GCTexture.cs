using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
