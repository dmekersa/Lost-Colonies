using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lost_Colonies
{
    internal class ScenePlanetList : GCSceneBase
    {
        List<string> testNames;
        NameGenerator nameGenerator;
        int first = 0;
        int startScrollValue;

        public ScenePlanetList()
        {
            MouseState ms = Mouse.GetState();
            startScrollValue = ms.ScrollWheelValue;

            testNames = new List<string>();
            nameGenerator = new NameGenerator();

            for (int i = 0; i < 100000; i++)
            {
                string name = nameGenerator.GetPlanetName(i);
                testNames.Add(name);
            }
        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            int newScrollValue = ms.ScrollWheelValue/120;
            int offset = newScrollValue - startScrollValue;
            if (offset>0)
                first = offset;
            else
            {
                startScrollValue = newScrollValue;
                first = 0;
            }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontBig");
            spriteBatch.DrawString(font, "Liste des planètes", new Vector2(1, 1), Color.White);
            const int NBNAMES = 24;
            int y = 32;
            int last = first + NBNAMES;
            if (last > testNames.Count)
            {
                last = testNames.Count;
            }
            for (int i = first; i < last; i++)
            {
                string name = testNames[i];
                spriteBatch.DrawString(font,name, new Vector2(1, y), Color.White);
                y += 18;
            }

            spriteBatch.DrawString(font, first.ToString(), new Vector2(400, 1), Color.White);
        }

        public override void DrawGUI()
        {
            
        }
    }
}