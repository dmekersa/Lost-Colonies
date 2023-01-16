using Microsoft.Xna.Framework;
using System;

public static class Utils
{
    public static double GetDistance(Vector2 p1, Vector2 p2)
    {
        return GetDistance(p1.X, p1.Y, p2.X, p2.Y);
    }

    public static double GetDistance(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
    }

    public static double GetAngle(Vector2 p1, Vector2 p2)
    {
        //function math.angle(x1,y1, x2,y2) return math.atan2(y2-y1, x2-x1) end
        return Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
    }
}
