using EastfrisianRogue.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EastfrisianRogue
{
    class WorldBuilder
    {
        private int _width;
        private int _height;

        private Tile[,] _tiles;
        private List<Creature> _creatures;

        public WorldBuilder(int width, int height)
        {
            _width = width;
            _height = height;
            _tiles = new Tile[width, height];
            _creatures = new List<Creature>();
        }

        public Tile CreateTile(string type, int x, int y)
        {
            if (type == "ground")
            {
                return new Tile("ground", '.', ConsoleColor.White, ConsoleColor.Black, x, y, false);
            }
            else if (type == "wall")
            {
                return new Tile("wall", '#', ConsoleColor.White, ConsoleColor.Black, x, y, true);
            }
            else
            {
                return null;
            }
        }

        public Creature CreateCreature(String type, int x, int y) 
        {
            if (type == "zombie") 
            {
                return new Creature("zombie", 'z', ConsoleColor.Green, x, y, "aggressive", 100, 40);
            } 
            else if (type == "sheep") 
            {
                return new Creature("sheep", 's', ConsoleColor.White, x, y, "docile", 10, 0);
            } 
            else 
            {
                return null;
            }
        }

        public WorldBuilder PopulateWorld(int nrOfCreatures)
        {
            var rnd = new Random();
            int rndX = 0;
            int rndY = 0;
            int creatureType = 0;
            Creature creature = null;

            for(int i = 0; i < nrOfCreatures; i++) 
            {
                do 
                {
                    rndX = rnd.Next(_width);
                    rndY = rnd.Next(_height);

                    if(_height <= 1)
                    {
                        _height = 2;
                    }
                }
                while (_tiles[rndX, rndY].IsBlocked);

                creatureType = rnd.Next(2);
                if (creatureType == 0) {
                    creature = CreateCreature("zombie", rndX, rndY);
                } else if (creatureType == 1) {
                    creature = CreateCreature("sheep", rndX, rndY);
                }

                _creatures.Add(creature);
            }            

            return this;
        }

        public WorldBuilder Fill(String tileType)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _tiles[x, y] = CreateTile(tileType, x, y);
                }
            }

            return this;
        }

        public World Build()
        {
            return new World(_tiles, _creatures);
        }

        public WorldBuilder CreateRandomWalkCave(int seed, int startX, int startY, int length)
        {
            var rnd = new Random(seed);
            int direction;
            int x = startX;
            int y = startY;

            for (int i = 0; i < length; i++)
            {
                direction = rnd.Next(4);
                if (direction == 0 && (x + 1) < (_width - 1))
                {
                    x += 1;
                }
                else if (direction == 1 && (x - 1) > 0)
                {
                    x -= 1;
                }
                else if (direction == 2 && (y + 1) < (_height - 1))
                {
                    y += 1;
                }
                else if (direction == 3 && (y - 1) > 0)
                {
                    y -= 1;
                }

                _tiles[x, y] = CreateTile("ground", x, y);
            }

            return this;
        }
    }
}
