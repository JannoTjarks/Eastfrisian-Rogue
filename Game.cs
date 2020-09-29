using System;
using System.Threading;

namespace EastfrisianRogue
{
    class Game
    {
        static void Main(string[] args)
        {
            var console = new ConsoleController(title: "EastfrisianRogue", consoleWidth: 80, consoleHeight: 24, mapWidth: 500, mapHeight: 500);

            console.Run();
        }        
    }
}
