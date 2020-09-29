using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;
using EastfrisianRogue.Entities;

namespace EastfrisianRogue
{
    public class ConsoleController
    {
        private Creature player;
        private World world;

        private Camera camera;

        private bool isRunning;

        public ConsoleController(int consoleWidth, int consoleHeight, int mapWidth, int mapHeight, string title)
        {
            Console.SetWindowSize(consoleWidth, consoleHeight);
            Console.Title = title;
            Console.CursorVisible = false;      

            camera = new Camera(consoleWidth, consoleHeight, mapWidth, mapHeight);     

            CreateWorld(mapWidth, mapHeight);
            Render();
        }

        public void Run()
        {
            isRunning = true;
            while (isRunning)
            {
                Input();
                Render();
            }
        }

        public void Input()
        {            
            ConsoleKey pressedKey = Console.ReadKey(true).Key;

            int dx = 0;
            int dy = 0;

            if (pressedKey == ConsoleKey.W)
            {
                dy = -1;
            }
            else if (pressedKey == ConsoleKey.S)
            {
                dy = 1;
            }
            else if (pressedKey == ConsoleKey.A)
            {
                dx = -1;
            }
            else if (pressedKey == ConsoleKey.D)
            {
                dx = 1;
            }

            player.Move(world, dx, dy);            
        }

        public void Render()
        {
            camera.lookAt(world, player.X, player.Y);
        }

        public void CreateWorld(int width, int height)
        {
            this.player = new Creature("player", '@', ConsoleColor.DarkRed, 10, 10);
            world = new WorldBuilder(width, height).Fill("wall").CreateRandomWalkCave(3, 10, 10, 10000).Build();
            world.player = player;
            world.AddEntity(player);
        }
    }
}
