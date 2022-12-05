using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamecodeur
{
    class GCAssetManager
    {
        Dictionary<string, Texture2D> DicoTextures;

        int preLoadCount = 0;
        string[] preLoadList;

        public GCAssetManager()
        {
            DicoTextures = new Dictionary<string, Texture2D>();
        }

        public void PreLoad(string[] pasTextures)
        {
            preLoadList = pasTextures;
            preLoadCount = 0;
        }

        public void NextPreload()
        {
            if (preLoadCount < preLoadList.Length)
            {
                Texture2D texture = GetTexture(preLoadList[preLoadCount]);
                preLoadCount++;
            }
        }

        public bool PreloadDone()
        {
            return preLoadCount == preLoadList.Length;
        }

        public Texture2D GetTexture(string psTextureName)
        {
            Texture2D texture;

            if (psTextureName.Contains(".xnb"))
            {
                int pos1 = psTextureName.IndexOf("/");
                int pos2 = psTextureName.LastIndexOf(".");

                psTextureName = psTextureName.Substring(pos1 + 1, (pos2 - pos1) - 1);
            }


            if (!DicoTextures.TryGetValue(psTextureName, out texture))
            {
                texture = GCServiceLocator.GetService<ContentManager>().Load<Texture2D>(psTextureName);
                DicoTextures.Add(psTextureName, texture);
            }

            return texture;
        }
    }
}
