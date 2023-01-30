using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
        Tween twExplorerX = new Tween();
        Tween twExplorerY = new Tween();
        Tween twExplorerZ = new Tween();
        enum eExplorer
        {
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

        public override void Focus()
        {
            controlManager.SetMethodKey("play", Microsoft.Xna.Framework.Input.Keys.Space);

            spPlanet.x = spOrbit.largeurFrame - spPlanet.largeurFrame + 50;
            spPlanet.y = 0;
            sfxFlightI.Play();
            spShip.x = 26;
            spShip.y = 51;
            spExplorer.x = 110 + 25;
            spExplorer.y = 90;
            spExplorer.reset();
            shipState = eExplorer.down;
            Timer = 0;

            twExplorerX.Start(spExplorer.x, spExplorer.x + 30, 2, Tween.easeType.SinOut);
            twExplorerY.Start(spExplorer.y, spExplorer.y + 50, 2, Tween.easeType.SinOut);
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Timer += dt;

            if (Timer >= 6 || controlManager.Pressed("play"))
            {
                Timer = 0;
                sfxFlightI.Stop();
                GCServiceLocator.GetService<GCSceneManager>().StartScene("surface");
            }

            if (shipState == eExplorer.down && twExplorerX.ended)
            {
                shipState = eExplorer.go;
                twExplorerX.Start(spExplorer.x, spExplorer.x + 400, 2, Tween.easeType.Cube);
                twExplorerZ.Start(1, 0.1f, 2, Tween.easeType.Cube);
            }

            spPlanet.x -= (50f / 6f) / 60f;
            spShip.x += (30f / 6f) / 60f;
            spShip.y += (10f / 6f) / 60f;


            if (shipState == eExplorer.down)
            {
                twExplorerX.Update(dt, ref spExplorer.x);
                twExplorerY.Update(dt, ref spExplorer.y);
            }
            else if (shipState == eExplorer.go)
            {
                twExplorerX.Update(dt, ref spExplorer.x);
                spExplorer.alpha -= 0.005f;
                //spExplorer.zoom -= (0.99f / 4f) / 60f;
                twExplorerZ.Update(dt, ref spExplorer.zoom);
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
