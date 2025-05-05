using System;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Rogue
{
    public class Game
    {
        PlayerCharacter player;
        Map level01;
        public static readonly int tileSize = 16;

        private string AskName()
        {
            while (true)
            {
                Console.WriteLine("What is your character's name?");
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Name cannot be empty.");
                    continue;
                }

                bool nameOk = true;
                foreach (char c in name)
                {
                    if (!Char.IsLetter(c))
                    {
                        nameOk = false;
                        Console.WriteLine("Name must contain only letters.");
                        break;
                    }
                }

                if (nameOk)
                    return name;
            }
        }

        private Race AskRace()
        {
            while (true)
            {
                Console.WriteLine("Select Race:");
                Console.WriteLine("1 - Human");
                Console.WriteLine("2 - Elf");
                Console.WriteLine("3 - Orc");
                string input = Console.ReadLine();

                if (input == "1" || input.Equals(Race.Human.ToString(), StringComparison.OrdinalIgnoreCase))
                    return Race.Human;
                if (input == "2" || input.Equals(Race.Elf.ToString(), StringComparison.OrdinalIgnoreCase))
                    return Race.Elf;
                if (input == "3" || input.Equals(Race.Orc.ToString(), StringComparison.OrdinalIgnoreCase))
                    return Race.Orc;

                Console.WriteLine("Invalid input.");
            }
        }

        private Class AskClass()
        {
            while (true)
            {
                Console.WriteLine("Select Class:");
                Console.WriteLine("1 - Rogue");
                Console.WriteLine("2 - Warrior");
                Console.WriteLine("3 - Magician");
                string input = Console.ReadLine();

                if (input == "1" || input.Equals(Class.Rogue.ToString(), StringComparison.OrdinalIgnoreCase))
                    return Class.Rogue;
                if (input == "2" || input.Equals(Class.Warrior.ToString(), StringComparison.OrdinalIgnoreCase))
                    return Class.Warrior;
                if (input == "3" || input.Equals(Class.Magician.ToString(), StringComparison.OrdinalIgnoreCase))
                    return Class.Magician;

                Console.WriteLine("Invalid input.");
            }
        }

        private PlayerCharacter CreateCharacter()
        {
            PlayerCharacter player = new PlayerCharacter(
                AskName(),
                AskRace(),
                AskClass()
            );

            return player;
        }

        public void Run()
        {
            Console.Clear();
            Init();
            GameLoop();
        }

        private void Init()
        {
            Raylib.InitWindow(480, 270, "Rogue");
            player = CreateCharacter();
            player.LoadTexture();

            MapReader loader = new MapReader();
            level01 = loader.LoadMapFromFile("mapfile_layers.json"); // <- New JSON format
            level01.LoadTexture();
            level01.LoadItems();
            level01.LoadEnemies();
            Raylib.SetTargetFPS(30);
        }

        private void DrawGame()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.DARKGRAY);

            level01.Draw();
            player.Draw();

            Raylib.EndDrawing();
        }

        private void UpdateGame()
        {
            int moveX = 0;
            int moveY = 0;

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
            {
                moveY = -1;
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
            {
                moveY = 1;
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
            {
                moveX = -1;
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
            {
                moveX = 1;
            }

            int newX = (int)player.paikka.X + moveX;
            int newY = (int)player.paikka.Y + moveY;

            newX = Math.Clamp(newX, 0, level01.Width - 1);
            newY = Math.Clamp(newY, 0, level01.Height - 1);

            MapLayer groundLayer = level01.GetLayer("ground");
            int index = newX + newY * level01.Width;
            int tileId = groundLayer.mapTiles[index];

            if ((MapTile)tileId == MapTile.Floor)
            {
                player.paikka.X = newX;
                player.paikka.Y = newY;

                Enemy enemy = level01.GetEnemyAt(newX, newY);
                if (enemy != null)
                {
                    Console.WriteLine($"Pelaaja kohtasi vihollisen: {enemy.name}");
                }

                Item item = level01.GetItemAt(newX, newY);
                if (item != null)
                {
                    Console.WriteLine($"Pelaaja löysi esineen: {item.name}");
                }
            }
        }


        private void GameLoop()
        {
            while (!Raylib.WindowShouldClose())
            {
                DrawGame();
                UpdateGame();
            }
            Raylib.CloseWindow();
        }
    }
}
