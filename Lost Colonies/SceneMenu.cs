﻿using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost_Colonies
{
    internal class SceneMenu : GCSceneBase
    {
        public SceneMenu()
        {
        }

        public override void Start()
        {
            controlManager.Reset();
            controlManager.SetMethodKey("play", Microsoft.Xna.Framework.Input.Keys.Space);
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update();

            if (controlManager.Pressed("play")) 
                GCServiceLocator.GetService<GCSceneManager>().StartScene("dashboard");

            base.Update(gameTime);
        }

        public override void Draw()
        {
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");
            spriteBatch.DrawString(font, "Menu - [Space] Dashboard", new Vector2(1, 1), Color.White);
        }

        public override void DrawGUI()
        {
            
        }
    }
}
