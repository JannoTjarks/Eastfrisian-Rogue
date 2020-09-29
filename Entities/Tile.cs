using System;
using System.Collections.Generic;
using System.Text;

namespace EastfrisianRogue.Entities
{
    public class Tile : Entity
    {
        private ConsoleColor _backgroundColor;
        private bool _isBlocked = false;
        
        public ConsoleColor BackgroundColor { get => this._backgroundColor; set => this._backgroundColor = value; }
        public bool IsBlocked { get => this._isBlocked; set => this._isBlocked = value; }

        public Tile(String type, char glyph, ConsoleColor color, ConsoleColor backgroundColor, int x, int y, bool blocked) : base(type, glyph, color, x, y)
        {
            _backgroundColor = backgroundColor;
            _isBlocked = blocked;
        }
    }
}
