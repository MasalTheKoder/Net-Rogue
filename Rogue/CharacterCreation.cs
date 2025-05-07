using System;
using RayGuiCreator;
using ZeroElectric.Vinculum;

namespace Rogue
{
    public class CharacterCreation
    {
        private Game game;
        private Race selectedRace = Race.Human;
        private Class selectedClass = Class.Rogue;
        private bool isRaceMenuOpen = false;
        private bool isClassMenuOpen = false;

        private string[] raceOptions = { "Human", "Elf", "Orc" };
        private string[] classOptions = { "Rogue", "Warrior", "Magician" };

        private const int ButtonWidth = 200;
        private const int ButtonHeight = 40;
        private const int Margin = 10;
        RayGuiCreator.TextBoxEntry playerNameEntry = new TextBoxEntry(15);

        public CharacterCreation(Game game)
        {
            this.game = game;
        }

        public void Draw()
        {
            if (!isRaceMenuOpen && !isClassMenuOpen)
            {
                DrawCharacterCreationScreen();
            }

            if (isRaceMenuOpen)
            {
                DrawRaceSelectionMenu();
            }

            if (isClassMenuOpen)
            {
                DrawClassSelectionMenu();
            }
        }

        private void DrawCharacterCreationScreen()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);

            int buttonX = Raylib.GetScreenWidth() / 2 - ButtonWidth / 2;
            int buttonY = Raylib.GetScreenHeight() / 2 - ButtonHeight * 5;

            MenuCreator menu = new MenuCreator(buttonX, buttonY, ButtonHeight, ButtonWidth);

            menu.Label("Character Creation");
            menu.TextBox(playerNameEntry);
            menu.Label(string.IsNullOrEmpty(playerNameEntry.ToString()) || !IsValidName(playerNameEntry.ToString())
                ? "Name must be at least one letter."
                : "Name is valid!");

            menu.Label("");
            menu.Label($"Race: {selectedRace}");
            if (menu.Button("Select Race")) isRaceMenuOpen = true;

            menu.Label("");
            menu.Label($"Class: {selectedClass}");
            if (menu.Button("Select Class")) isClassMenuOpen = true;

            menu.Label("");
            if (menu.Button("Start Game"))
            {
                if (!string.IsNullOrEmpty(playerNameEntry.ToString()) && IsValidName(playerNameEntry.ToString()))
                {
                    game.CreatePlayer(playerNameEntry.ToString(), selectedRace, selectedClass);
                    game.ChangeState(GameState.Playing);
                }
            }

            Raylib.EndDrawing();
        }

        private bool IsValidName(string name)
        {
            if (name.Length < 1) return false;  
            foreach (char c in name)
            {
                if (!char.IsLetter(c)) return false;
            }
            return true;
        }

        private void DrawRaceSelectionMenu()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);

            int buttonX = Raylib.GetScreenWidth() / 2 - ButtonWidth / 2;
            int buttonY = Raylib.GetScreenHeight() / 2 - ((ButtonHeight + Margin) * raceOptions.Length) / 2;

            MenuCreator menu = new MenuCreator(buttonX, buttonY, ButtonHeight, ButtonWidth);

            foreach (string race in raceOptions)
            {
                if (menu.Button(race))
                {
                    if (Enum.TryParse(race, out Race selected))
                    {
                        selectedRace = selected;
                        isRaceMenuOpen = false;
                        break;
                    }
                }
                menu.Label("");
            }

            if (menu.Button("Back"))
                isRaceMenuOpen = false;

            Raylib.EndDrawing();
        }

        private void DrawClassSelectionMenu()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);

            int buttonX = Raylib.GetScreenWidth() / 2 - ButtonWidth / 2;
            int buttonY = Raylib.GetScreenHeight() / 2 - ((ButtonHeight + Margin) * classOptions.Length) / 2;

            MenuCreator menu = new MenuCreator(buttonX, buttonY, ButtonHeight, ButtonWidth);

            foreach (string cls in classOptions)
            {
                if (menu.Button(cls))
                {
                    if (Enum.TryParse(cls, out Class selected))
                    {
                        selectedClass = selected;
                        isClassMenuOpen = false;
                        break;
                    }
                }
                menu.Label("");
            }

            if (menu.Button("Back"))
                isClassMenuOpen = false;

            Raylib.EndDrawing();
        }
    }
}
