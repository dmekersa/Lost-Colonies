using Gamecodeur;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost_Colonies
{
    internal class SceneTest : GCSceneBase
    {
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.DrawString(GCServiceLocator.GetService<FontManager>().getFont("fontBig"), "Scène de test", new Vector2(1, 1), Color.White);
        }
    }
}
