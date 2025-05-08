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
        Pause,
        GameOver
    }
    
    /// <summary>
    /// Game handles the main game loop, state transitions, and rendering of core game screens.
    /// </summary>
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
        private GameOver gameOverScreen;

        /// <summary>
        /// This starts the main game loop and handles state management.
        /// </summary>
        public void Run()
        {
            Init();

            optionsMenu = new OptionsMenu(this);
            pauseMenu = new PauseMenu(this, optionsMenu);
            characterCreation = new CharacterCreation(this);
            gameOverScreen = new GameOver(this);

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
                    case GameState.GameOver: 
                        gameOverScreen.Draw();
                        break;
                }
            }

            Raylib.CloseWindow();
        }

        /// <summary>
        /// Initializes the game window and sets basic settings like resolution and Fps.
        /// </summary>
        private void Init()
        {
            Raylib.InitWindow(480, 480, "Rogue");
            Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);
            Raylib.SetTargetFPS(30);
        }

        /// <summary>
        /// Draws the main menu and handles button interactions for navigating the game.
        /// </summary>
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
                optionsMenu.SetReturnState(GameState.MainMenu);  
                ChangeState(GameState.Options);
            }

            buttonY += buttonHeight * 2;
            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), "Exit") == 1)
            {
                Raylib.CloseWindow();
            }

            Raylib.EndDrawing();
        }

        /// <summary>
        /// The main game update loop; handles rendering and logic updates.
        /// </summary>
        private void GameLoop()
        {
            DrawGame();
            UpdateGame();
        }

        /// <summary>
        /// Draws the game world stuff like the map and player.
        /// </summary>
        private void DrawGame()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.DARKGRAY);

            level01.Draw();
            player.Draw();

            Raylib.EndDrawing();
        }

        /// <summary>
        /// Updates player movement and interactions with map elements like enemies and items.
        /// </summary>
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
                if (enemy != null) Console.WriteLine($"Player encountered enemy: {enemy.name}");

                Item item = level01.GetItemAt(newX, newY);
                if (item != null)
                {
                    Console.WriteLine($"Player found item: {item.name}");
                    ChangeState(GameState.GameOver); 
                }
            }
        }

        /// <summary>
        /// Changes the current game state.
        /// </summary>
        /// <param name="newState">New game state to switch to</param>
        public void ChangeState(GameState newState)
        {
            currentState = newState;
        }

        /// <summary>
        /// Creates the player character and loads the initial map and game assets.
        /// </summary>
        /// <param name="name">Player name</param>
        /// <param name="race">Chosen race</param>
        /// <param name="cls">Chosen class</param>
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
