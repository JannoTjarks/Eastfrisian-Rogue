using System;
using System.Collections.Generic;
using System.Linq;
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

            camera = new Camera(consoleWidth, consoleHeight -2, mapWidth, mapHeight);
            
            isRunning = true;

            CreateWorld(mapWidth, mapHeight);
            Render();
        }

        public void Run()
        {            
            while (isRunning && !player.IsDead)
            {
                Input();
                Update();
                Render();
            }
            if(player.IsDead)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You are dead...");
            }
        }

        public void Update()
        {
            var rnd = new Random();
            
            List<Creature> deadCreatures = world.creatures.Where(c => c.Type != "player" && c.IsDead == true).ToList();
            
            if(deadCreatures.Capacity > 0)
            {
                foreach(Creature deadCreature in deadCreatures)
                {
                    world.creatures.Remove(deadCreature);
                }
            }

            var creaturesWithoutPlayer = world.creatures.Where(c => c.Type != "player");
        
            foreach (Creature creature in creaturesWithoutPlayer)
            {
                int rndNr = rnd.Next(4);

                if(creature.Behaviour == "docile")
                {
                    if (rndNr == 0)
                    {
                        creature.Move(world, 1, 0);
                    }
                    else if (rndNr == 1)
                    {
                        creature.Move(world, -1, 0);
                    }
                    else if (rndNr == 2)
                    {
                        creature.Move(world, 0, 1);
                    }
                    else if (rndNr == 3)
                    {
                        creature.Move(world, 0, -1);
                    }
                }
                else if(creature.Behaviour == "aggressive")
                {
                    List<Creature> creaturesToFollow = world.GetCreatureInArea(creature.X, creature.Y, 10, 10);
                    creaturesToFollow.Remove(creature);

                    if(creaturesToFollow.Count != 0)
                    {
                        Creature creatureToFollow = creaturesToFollow.First();                        
                        if (creature.X > creatureToFollow.X)
                        {
                            creature.Move(world, -1, 0);
                        }
                        else if (creature.X < creatureToFollow.X)
                        {
                            creature.Move(world, 1, 0);
                        }
                        else if (creature.Y > creatureToFollow.Y)
                        {
                            creature.Move(world, 0, -1);
                        }
                        else if (creature.Y < creatureToFollow.Y)
                        {
                            creature.Move(world, 0, 1);
                        }
                    }
                }
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
            Console.ForegroundColor = ConsoleColor.White;
            Utils.ClearConsoleLine(0);
            Console.Write("Life: " + player.Hitpoints);
            Utils.ClearConsoleLine(1);
            
            camera.lookAt(world, player.X, player.Y);
        }

        public void CreateWorld(int width, int height)
        {
            this.player = new Creature("player", '@', ConsoleColor.DarkRed, 10, 10, "friendly", 100, 50);
            world = new WorldBuilder(width, height).Fill("wall").CreateRandomWalkCave(3, 10, 10, 5000).PopulateWorld(10).Build();
            // world = new WorldBuilder(width, height).Fill("wall").CreateRandomWalkCave(3, 10, 10, 5000).Build();
            world.player = player;
            world.AddEntity(player);
        }
    }
}
