using Lost_Colonies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Planet
{
    Random random = new Random();
    NameGenerator nameGenerator;

    public string Name { get; private set; }
    public Point Position { get; private set; }
    public SurfaceMap surfaceMap { get; private set; }

    public Planet(int pSeed, int pGalaxyWidth, int pGalaxyHeight)
    {
        surfaceMap = new SurfaceMap();
        nameGenerator = new NameGenerator();
        Name = nameGenerator.GetPlanetName(pSeed);
        Position = new Point(random.Next(pGalaxyWidth), random.Next(pGalaxyHeight));
    }
}

public class Galaxy
{
    public List<Planet> planets { get; private set; }
    Texture2D texPoint;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Galaxy()
    {
        GraphicsDevice gd = GCServiceLocator.GetService<GraphicsDevice>();
        Color[] data = new Color[1];
        data[0] = Color.White;
        texPoint = new Texture2D(gd, 1, 1);
        texPoint.SetData(data);
        planets = new List<Planet>();
    }

    public void Generate(int pPlanetQty, int pWidth, int pHeight)
    {
        Width = pWidth;
        Height = pHeight;

        planets.Clear();
        while (planets.Count < pPlanetQty)
        {
            Planet thePlanet = new Planet(planets.Count, pWidth, pHeight);
            planets.Add(thePlanet);
        }
        Debug.WriteLine("{0} generated planets", planets.Count);
    }

    public Planet GetFirstPlanet()
    {
        Planet result = null;
        int minDistance = int.MaxValue;

        Point posSelect = new Point(0, 0);
        foreach (Planet planet in planets)
        {
            double distance = Utils.GetDistance((double)posSelect.X, (double)posSelect.Y, (double)planet.Position.X, (double)planet.Position.Y);
            if (distance < minDistance)
            {
                result = planet;
                minDistance = (int)distance;
            }
        }

        Debug.WriteLine("Best planet is " + result.Name);

        Debug.Assert(result != null, "Impossible qu'on n'ai pas trouvé de planete dans la galaxie !");
        return result;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (Planet planet in planets)
        {
            spriteBatch.Draw(texPoint, planet.Position.ToVector2(), Color.White);
        }
    }
}
