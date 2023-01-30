using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Gamecodeur
{
    public abstract class GCSceneBase : GCGameObject
    {
        protected Game MyGame;
        protected ContentManager contentManager;
        protected GCControlManager controlManager;
        public Dictionary<string, string> Properties { get; }

        public GCSceneBase() : base()
        {
            contentManager = GCServiceLocator.GetService<ContentManager>();
            controlManager = GCServiceLocator.GetService<GCControlManager>();
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

        public virtual void Focus()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw()
        {

        }

        public abstract void DrawGUI();

    }
}
