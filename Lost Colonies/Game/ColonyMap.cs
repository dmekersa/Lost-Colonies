using Gamecodeur;
using Microsoft.Xna.Framework.Content;

namespace Lost_Colonies
{
    internal class ColonyMap : GCGameObject
    {
        protected ContentManager contentManager;
        private PlanetBase _planetBase;
        private int _currentFloor;

        private GCTexture texRoom;
        private GCTexture texDoorUp;
        private GCTexture texDoorRight;
        private GCTexture texDoorDown;
        private GCTexture texDoorLeft;

        public ColonyMap(PlanetBase pBase)
        {
            _planetBase = pBase;
            _currentFloor = 0;

            texRoom = new GCTexture("gfx/mm_room");
            texDoorUp = new GCTexture("gfx/mm_doorup");
            texDoorRight = new GCTexture("gfx/mm_doorright");
            texDoorDown = new GCTexture("gfx/mm_doordown");
            texDoorLeft = new GCTexture("gfx/mm_doorleft");
        }

        public void SelectUp()
        {
            _currentFloor--;
            if (_currentFloor < 0)
                _currentFloor = _planetBase.baseFloors.Length - 1;
        }

        public void SelectDown()
        {
            _currentFloor++;
            if (_currentFloor > _planetBase.baseFloors.Length - 1)
                _currentFloor = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw()
        {
            SpriteFont font = GCServiceLocator.GetService<FontManager>().getFont("fontSmall");

            int nbFloors = _planetBase.baseFloors.Length;
            int fHeight = (int)font.MeasureString("FF").Y;
            int posY = 0;

            // Liste des étages
            for (int i = 0; i < nbFloors; i++)
            {
                spriteBatch.DrawString(font, "F" + (nbFloors - i).ToString(), new Vector2(x + 1, y + posY), Color.White);
                if (i == _currentFloor)
                {
                    spriteBatch.DrawString(font, ">", new Vector2(x - 5, y + posY), Color.Red);
                }
                posY = posY + fHeight;
            }

            // Map de l'étage courant
            int margin = 30;
            for (int l = 0; l < _planetBase.baseFloors[_currentFloor].rooms.GetLength(0); l++)
            {
                for (int c = 0; c < _planetBase.baseFloors[_currentFloor].rooms.GetLength(1); c++)
                {
                    BaseRoom room = _planetBase.baseFloors[_currentFloor].rooms[l, c];
                    if (room != null)
                    {
                        spriteBatch.Draw(texRoom.texture, new Vector2(x + margin + c * texRoom.texture.Width, y + l * texRoom.texture.Height), Color.White);
                        if (room.doorUp)
                            spriteBatch.Draw(texDoorUp.texture, new Vector2(x + margin + c * texRoom.texture.Width, y + l * texRoom.texture.Height), Color.White);
                        if (room.doorRight)
                            spriteBatch.Draw(texDoorRight.texture, new Vector2(x + margin + c * texRoom.texture.Width, y + l * texRoom.texture.Height), Color.White);
                        if (room.doorDown)
                            spriteBatch.Draw(texDoorDown.texture, new Vector2(x + margin + c * texRoom.texture.Width, y + l * texRoom.texture.Height), Color.White);
                        if (room.doorLeft)
                            spriteBatch.Draw(texDoorLeft.texture, new Vector2(x + margin + c * texRoom.texture.Width, y + l * texRoom.texture.Height), Color.White);
                    }
                }
            }

            base.Draw();
        }
    }
}
