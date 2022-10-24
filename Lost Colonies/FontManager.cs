using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost_Colonies
{
    public class FontManager
    {
        ContentManager _content;
        private static readonly Dictionary<string, SpriteFont> listFonts = new Dictionary<string, SpriteFont>();

        public FontManager()
        {
            _content = GCServiceLocator.GetService<ContentManager>();
        }

        public void AddFont(string psContentName)
        {
            SpriteFont font = _content.Load<SpriteFont>(psContentName);
            listFonts[psContentName] = font;
        }

        public SpriteFont getFont(string pType)
        {
            if (listFonts.ContainsKey(pType))
                return listFonts[pType];
            else
            {
                return null;
            }
        }
    }
}
