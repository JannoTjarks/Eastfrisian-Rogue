using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;
using EastfrisianRogue.Entities;

namespace EastfrisianRogue
{
    public class Camera 
    {
        public int _screenWidth;
        public int _screenHeight;

        public int _mapWidth;
        public int _mapHeight;

        public Camera(int screenWidth, int screenHeight, int mapWidth, int mapHeight)
        {
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;
            this._mapWidth = mapWidth;
            this._mapHeight = mapHeight;
        } 

        public Point getCameraOrigin(int xfocus, int yfocus)
        {
            int spx = Math.Max(0, Math.Min(xfocus - _screenWidth /2, _mapWidth - _screenWidth));
            int spy = Math.Max(0, Math.Min(yfocus - _screenHeight /2, _mapHeight - _screenHeight));
            
            var origin = new Point();
            origin.X = spx;
            origin.Y = spy;

            return origin;
        }

        public void lookAt(World world, int xfocus, int yfocus)
        {
            Tile tile;            

            var origin = getCameraOrigin(xfocus, yfocus);

            for (int x = 0; x < _screenWidth; x++)
            {
                for (int y = 0; y < _screenHeight; y++)
                {                    
                    tile = world.GetTileAt(origin.X + x, origin.Y + y);

                    Draw(tile.Glyph, x, y, tile.Color, tile.BackgroundColor);
                }
            }

            int spx;
            int spy;
            foreach (Creature creature in world.creatures)
            {
                spx = creature.X - origin.X;
                spy = creature.Y - origin.Y;

                if ((spx >= 0 && spx < _screenWidth) && (spy >= 0 && spy < _screenHeight)) 
                {
                    Draw(creature.Glyph, spx, spy, creature.Color, world.GetTileAt(creature.X, creature.Y).BackgroundColor);
                }
            }
        }

        public void Draw(char glyphe, int x, int y, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.SetCursorPosition(x, y);
            Console.Write(glyphe);
        }
    }
}