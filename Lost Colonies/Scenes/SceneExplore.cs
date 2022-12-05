using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost_Colonies
{
    internal class SceneExplore : GCSceneBase
    {
        double Timer;
        GCTexture texOrbit;
        GCTexture texPlanet;
        GCTexture texExplorer;
        GCTexture texShip;
        GCSprite spOrbit;
        GCSprite spPlanet;
        GCSprite spShip;
        GCSprite spExplorer;
        SoundEffect sfxFlight;
        SoundEffectInstance sfxFlightI;
        enum eExplorer {
            down,
            go
        }
        eExplorer shipState;

        public SceneExplore()
        {
        }

        public override void Load()
        {
            ContentManager Content = GCServiceLocator.GetService<ContentManager>();
            texOrbit = new GCTexture("gfx/orbit_bg");
            texPlanet = new GCTexture("gfx/orbit_planet");
            texExplorer = new GCTexture("gfx/orbit_explorer");
            texShip = new GCTexture("gfx/orbit_ship");
            sfxFlight = Content.Load<SoundEffect>("sfx/flight");
            sfxFlightI = sfxFlight.CreateInstance();

            spOrbit = new GCSprite(spriteBatch, texOrbit.texture);
            spPlanet = new GCSprite(spriteBatch, texPlanet.texture);
            spPlanet.isPixel = true;
            spShip = new GCSprite(spriteBatch, texShip.texture);
            spShip.isPixel = true;
            spExplorer = new GCSprite(spriteBatch, texExplorer.texture);
            spExplorer.isPixel = true;

            base.Load();
        }

        public override void Start()
        {
            spPlanet.x = spOrbit.largeurFrame - spPlanet.largeurFrame + 50;
            spPlanet.y = 0;
            sfxFlightI.Play();
            spShip.x = 26;
            spShip.y = 51;
            spExplorer.x = 110+25;
            spExplorer.y = 90;
            spExplorer.reset();
            shipState = eExplorer.down;
            Timer = 0;
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update();

            Timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (Timer>=6)
            {
                Timer = 0;
                sfxFlightI.Stop();
                GCServiceLocator.GetService<GCSceneManager>().StartScene("surface");
            }

            if (Timer >= 2 && shipState != eExplorer.go)
            {
                shipState = eExplorer.go;
            }

            spPlanet.x -= (50f/6f)/60f;
            spShip.x += (30f / 6f) / 60f;
            spShip.y += (10f / 6f) / 60f;


            if (shipState == eExplorer.down)
            {
                spExplorer.x += (30f / 2f) / 60f;
                spExplorer.y += (50f / 2f) / 60f;
            }
            else if (shipState == eExplorer.go)
            {
                spExplorer.x += (500f / 2f) / 60f;
                spExplorer.zoom -= (0.99f / 2f) / 60f;
                spExplorer.alpha -= 0.015f;
            }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");

            spOrbit.Draw();
            spPlanet.Draw();
            spExplorer.Draw();
            spShip.Draw();

            spriteBatch.DrawString(font, "Explore... please wait...", new Vector2(1, 1), Color.White);
        }

        public override void DrawGUI()
        {

        }
    }
}
