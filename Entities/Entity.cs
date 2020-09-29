using System;
using System.Collections.Generic;
using System.Text;

namespace EastfrisianRogue.Entities
{
    public class Entity
    {
        protected int _x;
        protected int _y;

        protected string _type;
        protected char _glyph;
        protected ConsoleColor _color;

        public Entity(string type, char glyph, ConsoleColor color, int x, int y)
        {
            _type = type;
            _glyph = glyph;
            _color = color;
            _x = x;
            _y = y;
        }

        public int X { get => this._x; set => this._x = value; }
        public int Y { get => this._y; set => this._y = value; }
        public string Type { get => this._type; set => this._type = value; }
        public char Glyph { get => this._glyph; set => this._glyph = value; }
        public ConsoleColor Color { get => this._color; set => this._color = value; }        
    }
}
