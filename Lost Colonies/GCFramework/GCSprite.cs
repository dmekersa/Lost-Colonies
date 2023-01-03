using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Gamecodeur
{
    class GCSAnimation
    {
        public string name { get; private set; }
        public int[] frames { get; private set; }
        public float dureeFrame { get; private set; }
        public bool isLoop { get; set; }
        public bool isFinished { get; set; }
        public GCSAnimation(string pName, int[] pFrames, float pTime = 1f / 12f, bool pisLoop = true)
        {
            name = pName;
            frames = pFrames;
            dureeFrame = pTime;
            isLoop = pisLoop;
            isFinished = false;
        }
    }
    class GCSprite
    {
        public float x;
        public float y;
        protected Vector2 Velocity;
        public bool isVisible { get; set; }
        public bool isCentered { get; set; }
        public bool isPixel { get; set; }
        public float zoom;
        public float alpha;
        public float rotation { get; set; }
        public Dictionary<string, string> properties { get; }
        public SpriteBatch spriteBatch { get; }
        public Texture2D texture { get; }
        public List<GCSAnimation> animations;
        public GCSAnimation animationCourante;
        protected SpriteEffects effect;
        public int frame { get; set; }
        public int largeurFrame { get; private set; }
        public int hauteurFrame { get; private set; }
        public float speed { get; set; }
        private float time;

        //static public List<GCSprite> lstSprites = new List<GCSprite>();

        public GCSprite(SpriteBatch pSpriteBatch, Texture2D pTexture, int pLargeurFrame = 0, int pHauteurFrame = 0)
        {
            spriteBatch = pSpriteBatch;
            texture = pTexture;
            isVisible = true;
            isCentered = true;
            if (pLargeurFrame == 0)
            {
                largeurFrame = pTexture.Width;
                hauteurFrame = pTexture.Height;
            }
            else
            {
                largeurFrame = pLargeurFrame;
                hauteurFrame = pHauteurFrame;
            }
            speed = 60.0f;
            effect = SpriteEffects.None;
            animations = new List<GCSAnimation>();
            //lstSprites.Add(this);
            properties = new Dictionary<string, string>();
            reset();
        }

        public void reset()
        {
            frame = -1;
            zoom = 1.0f;
            alpha = 1.0f;
        }

        public string getProperty(string pName)
        {
            if (properties.ContainsKey(pName))
            {
                return properties[pName];
            }
            return "";
        }

        public void AjouteAnimation(string pName, int[] pFrames, float pDureeFrame, bool pisLoop = true)
        {
            GCSAnimation animation = new GCSAnimation(pName, pFrames, pDureeFrame, pisLoop);
            animations.Add(animation);
        }

        public void LanceAnimation(string pName)
        {
            Debug.WriteLine("LanceAnimation({0})", pName);
            foreach (GCSAnimation element in animations)
            {
                if (element.name == pName)
                {
                    animationCourante = element;
                    frame = 0;
                    animationCourante.isFinished = false;
                    Debug.WriteLine("LanceAnimation, OK {0}", animationCourante.name);
                    break;
                }
            }
            Debug.Assert(animationCourante != null, "LanceAnimation : Aucune animation trouvée");
        }

        public virtual void Update(GameTime gameTime)
        {
            this.x += this.Velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.y += this.Velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Traitement des animations image par image
            if (animationCourante == null) return;
            if (animationCourante.dureeFrame == 0) return;
            if (animationCourante.isFinished) return;

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time > animationCourante.dureeFrame)
            {
                //Debug.WriteLine("GCSprite/Update: Changement de frame, courante={0}", frame);
                frame++;
                if (frame >= animationCourante.frames.Count())
                {
                    if (animationCourante.isLoop)
                    {
                        frame = 0;
                    }
                    else
                    {
                        frame--;
                        animationCourante.isFinished = true;
                    }
                }
                time = 0;
            }
        }

        public virtual void Draw()
        {
            if (!isVisible) return;
            Vector2 position;

            if (isPixel)
                position = new Vector2((int)x, (int)y);
            else
                position = new Vector2(x, y);
            Vector2 origine = new Vector2(0, 0);
            Rectangle source;

            if (animationCourante == null && frame == -1)
            {
                spriteBatch.Draw(texture, position, null, Color.White * alpha, rotation, origine, zoom, effect, 0.0f);
            }
            else
            {
                int texW = texture.Width / largeurFrame;
                int texH = texture.Height / hauteurFrame;

                int f = 0;

                if (animationCourante != null)
                {
                    f = animationCourante.frames[frame];
                }
                else
                {
                    f = frame;
                }

                int line = (int)Math.Floor((double)(f / texW));
                int col = f - (line * texW);

                source = new Rectangle();
                source.X = col * largeurFrame;
                source.Y = line * hauteurFrame;
                source.Width = largeurFrame;
                source.Height = hauteurFrame;


                if (isCentered)
                {
                    origine = new Vector2(largeurFrame / 2, hauteurFrame / 2);
                }
                spriteBatch.Draw(texture, position, source, Color.White * alpha, rotation, origine, zoom, effect, 0.0f);

            }
        }
    }
}
