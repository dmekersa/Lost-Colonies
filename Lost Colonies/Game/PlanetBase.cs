using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Lost_Colonies
{
    public class BaseRoom
    {
        public bool doorUp = false;
        public bool doorRight = false;
        public bool doorDown = false;
        public bool doorLeft = false;
        public int Line { get; private set; }
        public int Column { get; private set; }

        public BaseRoom(int pLine, int pCol)
        {
            Line = pLine;
            Column = pCol;
        }
    }

    public class BaseFloor
    {
        private Random _rnd = new Random();
        public const int width = 9;
        public const int height = 6;
        public BaseRoom[,] rooms;
        List<BaseRoom> listRooms = new List<BaseRoom>();

        private BaseRoom CreateRoom(int pLine, int pCol)
        {
            BaseRoom newRoom = new BaseRoom(pLine, pCol);
            rooms[pLine, pCol] = newRoom;
            listRooms.Add(rooms[pLine, pCol]);

            return newRoom;
        }

        public BaseFloor()
        {
            rooms = new BaseRoom[height, width];

            // sélectionne une position pour la salle de départ
            int startCol = _rnd.Next(width);
            int startLine = _rnd.Next(height);

            int nbRooms = _rnd.Next(1, 10 + 1);

            // Crée la salle de départ
            CreateRoom(startLine, startCol);

            while (listRooms.Count < nbRooms)
            {
                // Pioche une salle dans la liste
                BaseRoom rndRoom = listRooms[_rnd.Next(0, listRooms.Count)];

                // Sélectionne une direction
                int direction = _rnd.Next(4);

                // Si pas de salle dans cette direction, je crée une salle, et j'ouvre les portes
                switch (direction)
                {
                    case 0: // Up
                        if (rndRoom.Line > 0 && rooms[rndRoom.Line - 1, rndRoom.Column] == null)
                        {
                            BaseRoom newBase = CreateRoom(rndRoom.Line - 1, rndRoom.Column);
                            newBase.doorDown = true;
                            rndRoom.doorUp = true;
                        }
                        break;
                    case 1: // Right
                        if (rndRoom.Column < width - 1 && rooms[rndRoom.Line, rndRoom.Column + 1] == null)
                        {
                            BaseRoom newBase = CreateRoom(rndRoom.Line, rndRoom.Column + 1);
                            newBase.doorLeft = true;
                            rndRoom.doorRight = true;
                        }
                        break;
                    case 2: // Down
                        if (rndRoom.Line < height - 1 && rooms[rndRoom.Line + 1, rndRoom.Column] == null)
                        {
                            BaseRoom newBase = CreateRoom(rndRoom.Line + 1, rndRoom.Column);
                            newBase.doorUp = true;
                            rndRoom.doorDown = true;
                        }
                        break;
                    case 3: // Left
                        if (rndRoom.Column > 0 && rooms[rndRoom.Line, rndRoom.Column - 1] == null)
                        {
                            BaseRoom newBase = CreateRoom(rndRoom.Line, rndRoom.Column - 1);
                            newBase.doorRight = true;
                            rndRoom.doorLeft = true;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public class PlanetBase
    {
        private Random _rnd = new Random();
        public Point basePosition;
        public BaseFloor[] baseFloors;

        public PlanetBase()
        {
            int nbFloors = _rnd.Next(5, 10 + 1);
            baseFloors = new BaseFloor[nbFloors];
            for (int i = 0; i < nbFloors; i++)
            {
                baseFloors[i] = new BaseFloor();
            }
        }
    }

}
