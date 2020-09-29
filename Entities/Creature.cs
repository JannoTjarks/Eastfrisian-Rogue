using System;
using System.Collections.Generic;
using System.Text;

namespace EastfrisianRogue.Entities
{
    public class Creature : Entity
    {
        public Creature(string type, char glyph, ConsoleColor color, int x, int y) : base(type, glyph, color, x, y)
        {
                        
        }

        public void Move(World world, int dx, int dy)
        {
            if (world.IsBlocked(_x + dx, _y + dy) != true)
            {
                this._x += dx;
                this._y += dy;
            }
        }
    }
}
