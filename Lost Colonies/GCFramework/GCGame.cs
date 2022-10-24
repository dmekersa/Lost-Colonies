using Lost_Colonies;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gamecodeur
{
    class GCGame
    {
        // Les trucs de Monogame
        ContentManager Content;

        // Les autres composants de GC
        public GCSceneManager SceneManager;
        public GCControlManager ControlManager;
        public FontManager FontManager;

        public GCGame()
        {
            Content = GCServiceLocator.GetService<ContentManager>();

            SceneManager = new GCSceneManager();
            GCServiceLocator.RegisterService<GCSceneManager>(SceneManager);
            ControlManager = new GCControlManager();
            GCServiceLocator.RegisterService<GCControlManager>(ControlManager);
            FontManager = new FontManager();
            GCServiceLocator.RegisterService<FontManager>(FontManager);

        }

    }
}
