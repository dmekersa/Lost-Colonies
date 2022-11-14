using Gamecodeur;
using Lost_Colonies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SceneGalaxy : GCSceneBase
{
    Galaxy theGalaxy;
    Texture2D texSelectMap;
    Vector2 posSelectMap;
    Planet nearestPlanet = null;

    public override void Load()
    {
        // sélecteur de planète
        texSelectMap = GCServiceLocator.GetService<ContentManager>().Load<Texture2D>("selectMap");
        posSelectMap = new Vector2(0, 0);

        Debug.WriteLine("Load Galaxy");
        theGalaxy = new Galaxy();
        theGalaxy.Generate(256, 256*2, 256);
    }

    public override void Stop()
    {

    }

    public override void Start()
    {
        controlManager.Reset();
        controlManager.SetMethodKey("new", Microsoft.Xna.Framework.Input.Keys.Space);

        controlManager.SetMethodKey("right", Microsoft.Xna.Framework.Input.Keys.Right);
        controlManager.SetMethodKey("down", Microsoft.Xna.Framework.Input.Keys.Down);
        controlManager.SetMethodKey("left", Microsoft.Xna.Framework.Input.Keys.Left);
        controlManager.SetMethodKey("up", Microsoft.Xna.Framework.Input.Keys.Up);

        controlManager.SetMethodKey("back", Microsoft.Xna.Framework.Input.Keys.Escape);
        controlManager.SetMethodKey("select", Microsoft.Xna.Framework.Input.Keys.Space);
    }

    public override void Update(GameTime gameTime)
    {
        controlManager.Update();

        if (controlManager.Pressed("back") || controlManager.Pressed("select"))
        {
            GCServiceLocator.GetService<GCSceneManager>().StartScene("dashboard");
        }

        if (controlManager.Pressed("new"))
        {
            theGalaxy.Generate(256, 256 * 2, 256);
            posSelectMap = new Vector2(0, 0);
        }

        bool bStick = true;
        if (controlManager.Down("right"))
        {
            posSelectMap.X++;
            bStick = false;
        }
        if (controlManager.Down("left"))
        {
            posSelectMap.X--;
            bStick = false;
        }
        if (controlManager.Down("down"))
        {
            posSelectMap.Y++;
            bStick = false;
        }
        if (controlManager.Down("up"))
        {
            posSelectMap.Y--;
            bStick = false;
        }

        if (bStick)
        {
            // Rechercher la planete la + proche de la croix
            int minDistance = 9999;
            Point posSelect = new Point((int)posSelectMap.X+1, (int)posSelectMap.Y+1);
            foreach (Planet planet in theGalaxy.planets)
            {
                double distance = Utils.GetDistance((double)posSelect.X, (double)posSelect.Y, (double)planet.Position.X, (double)planet.Position.Y);
                if (distance<minDistance)
                {
                    nearestPlanet = planet;
                    minDistance = (int)distance;
                }
            }

            if (nearestPlanet!=null)
            {
                posSelectMap.X = nearestPlanet.Position.X - 1;
                posSelectMap.Y = nearestPlanet.Position.Y - 1;
            }
        }

        base.Update(gameTime);
    }

    public override void Draw()
    {
        theGalaxy.Draw(spriteBatch);

        spriteBatch.Draw(texSelectMap, posSelectMap, Color.White);


        base.Draw();
    }

    public override void DrawGUI()
    {
        if (nearestPlanet != null)
        {
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontBig");
            spriteBatch.DrawString(font, nearestPlanet.Name, new Vector2(5, (theGalaxy.Height*2) + 2), Color.White);
        }

    }
}
