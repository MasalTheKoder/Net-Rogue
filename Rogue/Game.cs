using System;
using System.Collections.Generic;
using ZeroElectric.Vinculum;

namespace Rogue
{
    public enum GameState
    {
        MainMenu,
        CharacterCreation,
        Playing,
        Options,
        Pause
    }

    public class Game
    {
        private PlayerCharacter player;
        private Map level01;
        public static readonly int tileSize = 16;
        private List<int> WallTileNumbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 12, 13, 14, 15, 16, 17, 18, 19, 20, 24, 25, 26, 27, 28, 29, 40, 57, 58, 59 };
        private GameState currentState = GameState.MainMenu;
        private OptionsMenu optionsMenu;
        private PauseMenu pauseMenu;
        private CharacterCreation characterCreation;

        public void Run()
        {
            Init();

            optionsMenu = new OptionsMenu(this);
            pauseMenu = new PauseMenu(this);
            characterCreation = new CharacterCreation(this);

            while (!Raylib.WindowShouldClose())
            {
                switch (currentState)
                {
                    case GameState.MainMenu:
                        DrawMainMenu();
                        break;
                    case GameState.Options:
                        optionsMenu.Draw();
                        break;
                    case GameState.CharacterCreation:
                        characterCreation.Draw();
                        break;
                    case GameState.Playing:
                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_TAB))
                        {
                            ChangeState(GameState.Pause);
                        }
                        GameLoop();
                        break;
                    case GameState.Pause:
                        pauseMenu.Draw();
                        break;
                }
            }

            Raylib.CloseWindow();
        }

        private void Init()
        {
            Raylib.InitWindow(480, 480, "Rogue");
            Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);
            Raylib.SetTargetFPS(30);
        }

        private void DrawMainMenu()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);

            int buttonWidth = 200;
            int buttonHeight = 40;
            int buttonX = Raylib.GetScreenWidth() / 2 - buttonWidth / 2;
            int buttonY = Raylib.GetScreenHeight() / 2 - buttonHeight / 2;

            RayGui.GuiLabel(new Rectangle(buttonX, buttonY - buttonHeight * 2, buttonWidth, buttonHeight), "Rogue");

            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), "Play Game") == 1)
            {
                ChangeState(GameState.CharacterCreation);
            }

            buttonY += buttonHeight * 2;
            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), "Settings") == 1)
            {
                ChangeState(GameState.Options);
            }

            buttonY += buttonHeight * 2;
            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), "Exit") == 1)
            {
                Raylib.CloseWindow();
            }

            Raylib.EndDrawing();
        }

        private void GameLoop()
        {
            DrawGame();
            UpdateGame();
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

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP)) moveY = -1;
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN)) moveY = 1;
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT)) moveX = -1;
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT)) moveX = 1;

            int newX = (int)player.paikka.X + moveX;
            int newY = (int)player.paikka.Y + moveY;

            newX = Math.Clamp(newX, 0, level01.Width - 1);
            newY = Math.Clamp(newY, 0, level01.Height - 1);

            MapLayer groundLayer = level01.GetLayer("ground");
            int index = newX + newY * level01.Width;
            int tileId = groundLayer.mapTiles[index];

            if (!WallTileNumbers.Contains(tileId))
            {
                player.paikka.X = newX;
                player.paikka.Y = newY;

                Enemy enemy = level01.GetEnemyAt(newX, newY);
                if (enemy != null) Console.WriteLine($"Pelaaja kohtasi vihollisen: {enemy.name}");

                Item item = level01.GetItemAt(newX, newY);
                if (item != null) Console.WriteLine($"Pelaaja löysi esineen: {item.name}");
            }
        }

        public void ChangeState(GameState newState)
        {
            currentState = newState;
        }

        public void CreatePlayer(string name, Race race, Class cls)
        {
            player = new PlayerCharacter(name, race, cls);
            player.LoadTexture();

            MapReader loader = new MapReader();
            level01 = loader.LoadMapFromFile("tiledmap.tmj");
            level01.LoadTexture();
            level01.LoadItems();
            level01.LoadEnemies();
        }
    }
}
