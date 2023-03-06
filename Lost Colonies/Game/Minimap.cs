using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lost_Colonies
{
    struct mmObject
    {
        public Point position;
        public Texture2D color;
    }

    public class Minimap
    {
        public Point Position { get; private set; }
        private Point _UpperLeft;
        private int width;
        private int height;
        private int _visibleColumns;
        private int _visibleRows;
        private Dictionary<int, Texture2D> _colors;
        private List<mmObject> _lstObjects;
        private int[,] _map;
        private int[,] _fog;
        private bool _bVisibleObject = true;
        private double _timerVisibleObject;
        Texture2D _black;
        Texture2D _white;

        public Minimap(int pX, int pY, int pWidth, int pHeight, int pVisibleColumns, int pVisibleRows, int[,] pMap, int[,] pFog)
        {
            Position = new Point(pX, pY);
            width = pWidth;
            height = pHeight;
            _visibleColumns = pVisibleColumns;
            _visibleRows = pVisibleRows;
            _colors = new Dictionary<int, Texture2D>();
            _lstObjects = new List<mmObject>();
            _map = pMap;
            _fog = pFog;
            _black = new Texture2D(GCServiceLocator.GetService<GraphicsDevice>(), 1, 1);
            _black.SetData(new[] { new Color(0, 0, 0) });

            _white = new Texture2D(GCServiceLocator.GetService<GraphicsDevice>(), 1, 1);
            _white.SetData(new[] { new Color(255, 255, 255) });
        }

        public void SetUpperLeft(int pCol, int pRow)
        {
            _UpperLeft = new Point(pCol, pRow);
            Debug.WriteLine(pCol + "," + pRow);
        }

        public void SetColor(int pID, Color pColor)
        {
            Texture2D t = new Texture2D(GCServiceLocator.GetService<GraphicsDevice>(), 1, 1);
            t.SetData(new[] { pColor });
            _colors.Add(pID, t);
        }

        public void ResetObjects()
        {
            _lstObjects.Clear();
        }

        public void AddObject(Point pPosition, Color pColor)
        {
            mmObject o = new mmObject();
            o.position = pPosition;
            Texture2D t = new Texture2D(GCServiceLocator.GetService<GraphicsDevice>(), 1, 1);
            t.SetData(new[] { pColor });
            o.color = t;
            _lstObjects.Add(o);
        }

        private mmObject? getObjectAt(int pX, int pY)
        {
            foreach (mmObject o in _lstObjects)
            {
                if (o.position.X == pX && o.position.Y == pY)
                    return o;
            }
            return null;
        }

        public void Update(GameTime gameTime)
        {
            _timerVisibleObject += gameTime.ElapsedGameTime.TotalSeconds;
            if (_timerVisibleObject > 0.2f)
            {
                _timerVisibleObject = 0;
                _bVisibleObject = !_bVisibleObject;
            }
        }

        public void Draw()
        {
            int mapHeight = _map.GetLength(0);
            int mapWidth = _map.GetLength(1);
            float ratioX = mapWidth / width;
            float ratioY = mapHeight / height;
            SpriteBatch spriteBatch = GCServiceLocator.GetService<SpriteBatch>();

            // Dessine un fond
            spriteBatch.Draw(_white, new Rectangle(Position.X - 1, Position.Y - 1, width + 2, height + 2), Color.White);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int col = (int)(x * ratioX);
                    int line = (int)(y * ratioY);
                    int id = _map[line, col];
                    Texture2D t = _black;
                    if (_colors.ContainsKey(id) && _fog[line, col] == 0)
                    {
                        t = _colors[id];
                    }
                    spriteBatch.Draw(t, new Vector2(Position.X + x, Position.Y + y), Color.White);
                }
            }

            if (_bVisibleObject)
            {
                foreach (mmObject o in _lstObjects)
                {
                    Point p = new Point((int)(Position.X + (o.position.X / ratioX)), (int)(Position.Y + (o.position.Y / ratioY)));
                    p = p - new Point(1, 1);
                    Texture2D t = ((mmObject)o).color;
                    for (int xx = (int)p.X; xx < p.X + 3; xx++)
                    {
                        for (int yy = (int)p.Y; yy < p.Y + 3; yy++)
                        {
                            spriteBatch.Draw(t, new Vector2(xx, yy), Color.White);
                        }
                    }

                }
            }

            // Affiche la surface visible
            Vector2 start = new Vector2(Position.X + (_UpperLeft.X / ratioX), (int)(Position.Y + (_UpperLeft.Y / ratioY)));
            Vector2 end = start + new Vector2(_visibleColumns / ratioX, _visibleRows / ratioY);

            for (float y = start.Y; y <= end.Y; y++)
            {
                spriteBatch.Draw(_white, new Vector2(start.X, y), Color.White);
                spriteBatch.Draw(_white, new Vector2(end.X, y), Color.White);
            }
            for (float x = start.X; x <= end.X; x++)
            {
                spriteBatch.Draw(_white, new Vector2(x, start.Y), Color.White);
                spriteBatch.Draw(_white, new Vector2(x, end.Y), Color.White);
            }
        }
    }
}
