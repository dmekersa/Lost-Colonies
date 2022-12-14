using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LD36;

namespace Gamecodeur
{
    public abstract class GCSceneBase
    {
        protected Game MyGame;
        protected ContentManager contentManager;
        protected GCControlManager controlManager;
        protected SpriteBatch spriteBatch { get; }
        public Dictionary<string, string> Properties { get; }

        public GCSceneBase()
        {
            contentManager = GCServiceLocator.GetService<ContentManager>();
            controlManager = GCServiceLocator.GetService<GCControlManager>();
            spriteBatch = GCServiceLocator.GetService<SpriteBatch>();
            Properties = new Dictionary<string, string>();
        }

        public void setProperty(string pName, string pProperty)
        {
            if (!Properties.ContainsKey(pName))
            {
                Properties.Add(pName, pProperty);
            }
            else
            {
                Properties[pName] = pProperty;
            }
        }

        public string getProperty(string pName)
        {
            if (Properties.ContainsKey(pName))
            {
                return Properties[pName];
            }
            return "";
        }

        public virtual void Load()
        {

        }

        public virtual void Stop()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw()
        {

        }

        public abstract void DrawGUI();

    }
}
