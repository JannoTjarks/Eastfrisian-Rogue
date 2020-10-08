using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EastfrisianRogue.Entities;

namespace EastfrisianRogue
{
    public class World
    {
        private Tile[,] tiles;
        private int _width;
        private int _height;
        public Creature player;
        public List<Creature> creatures;

        public int Width { get => this._width; set => this._width = value; }
        public int Height { get => this._height; set => this._height = value; }

        public World(Tile[,] tiles, List<Creature> creatures)
        {
            this.creatures = new List<Creature>();
            this.creatures.AddRange(creatures);
            this.tiles = tiles;
            this.Width = tiles.GetLength(0);
            this.Height = tiles.GetLength(1);
        }

        public void AddEntity(Creature creature)
        {
            this.creatures.Add(creature);
        }

        public Creature GetCreatureAt(int x, int y)
        {
            var queryCreature = from creature in creatures
                                where creature.X == x && creature.Y == y
                                select creature;
            return queryCreature.FirstOrDefault();
        }

        public List<Creature> GetCreatureInArea(int x, int y, int width, int height)
        {
            return creatures.Where(c => c.X > x - width / 2 &&
                                        c.X < x + width / 2 &&
                                        c.Y > y - height / 2 &&
                                        c.Y < y + height / 2).ToList();
        }

        public Tile GetTileAt(int x, int y)
        {
            return tiles[x, y];            
        }

        public bool IsBlocked(int x, int y)
        {
            return (GetCreatureAt(x, y) != null || GetTileAt(x, y).IsBlocked);
        }
    }
}
