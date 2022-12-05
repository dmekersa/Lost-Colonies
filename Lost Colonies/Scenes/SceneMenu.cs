using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost_Colonies
{
    internal class SceneMenu : GCSceneBase
    {
        Texture2D texBG;
        Texture2D texLogo;
        Song music;
        float alphaBG;
        float alphaLogo;
        float alphaText;
        double timer;

        public SceneMenu()
        {
        }

        public override void Load()
        {
            texBG = contentManager.Load<Texture2D>("menu/startBG");
            texLogo = contentManager.Load<Texture2D>("menu/start-logo-baseline");
            music = contentManager.Load<Song>("snd/menu");

            base.Load();
        }

        public override void Start()
        {
            alphaBG = 0;
            alphaLogo = 0;
            alphaText = 0;
            controlManager.Reset();
            controlManager.SetMethodKey("play", Microsoft.Xna.Framework.Input.Keys.Space);
            base.Start();
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(music);
        }

        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;

            controlManager.Update();
            if (alphaBG<1)
            {
                alphaBG += 0.05f;
                if (alphaBG > 1)
                    alphaBG = 1;
            }

            if (timer>=8f)
            {
                if (alphaLogo<1)
                {
                    alphaLogo += 0.005f;
                    if (alphaLogo>1)
                        alphaLogo = 1;
                }
            }

            if (timer >= 16f)
            {
                if (alphaText < 1)
                {
                    alphaText += 0.005f;
                    if (alphaText > 1)
                        alphaText = 1;
                }
            }

            if (controlManager.Pressed("play"))
            {
                GCServiceLocator.GetService<GCSceneManager>().StartScene("dashboard");
                MediaPlayer.Stop();
            }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");
            string text = "Appuyez sur ESPACE pour démarrer";
            Vector2 textSize = font.MeasureString(text);

            spriteBatch.Draw(texBG, new Vector2(0, 0), Color.White * alphaBG);
            spriteBatch.Draw(texLogo, new Vector2(texBG.Width/2, (texBG.Height/2) - textSize.Y), null, Color.White * alphaLogo, 0, new Vector2(texLogo.Width/2, texLogo.Height/2), 1, SpriteEffects.None, 0);

            spriteBatch.DrawString(font, text, new Vector2((texBG.Width/2)-(textSize.X/2), (texBG.Height / 2) - (textSize.Y / 2) + (texLogo.Height/2)), Color.White * alphaText);
        }

        public override void DrawGUI()
        {
            
        }
    }
}
