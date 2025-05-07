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
            int buttonY = Raylib.GetScreenHeight() / 2 - ButtonHeight / 2;
            RayGuiCreator.MenuCreator menu = new MenuCreator(buttonX, buttonY, ButtonHeight, ButtonWidth);
            menu.Label("Character Creation");

            menu.TextBox(playerNameEntry);

            if (string.IsNullOrEmpty(playerNameEntry.ToString()) || !IsValidName(playerNameEntry.ToString()))
            {
                RayGui.GuiLabel(new Rectangle(buttonX, buttonY, ButtonWidth, ButtonHeight), "Name must be at least one letter.");
            }
            else
            {
                RayGui.GuiLabel(new Rectangle(buttonX, buttonY, ButtonWidth, ButtonHeight), "Name is valid!");
            }
            buttonY += ButtonHeight;

            RayGui.GuiLabel(new Rectangle(buttonX, buttonY, ButtonWidth, ButtonHeight), $"Race: {selectedRace}");
            buttonY += ButtonHeight;

            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY, ButtonWidth, ButtonHeight), "Select Race") == 1)
            {
                isRaceMenuOpen = true;
            }
            buttonY += ButtonHeight * 2;

            RayGui.GuiLabel(new Rectangle(buttonX, buttonY, ButtonWidth, ButtonHeight), $"Class: {selectedClass}");
            buttonY += ButtonHeight;

            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY, ButtonWidth, ButtonHeight), "Select Class") == 1)
            {
                isClassMenuOpen = true;
            }
            buttonY += ButtonHeight * 2;

            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY, ButtonWidth, ButtonHeight), "Start Game") == 1)
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
            if (name.Length < 2) return false;
            foreach (char c in name)
            {
                if (!char.IsLetter(c)) return false;
            }
            return true;
        }

        private void DrawRaceSelectionMenu()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.DARKGRAY);

            int buttonX = Raylib.GetScreenWidth() / 2 - ButtonWidth / 2;
            int buttonY = Raylib.GetScreenHeight() / 2 - ButtonHeight / 2;

            for (int i = 0; i < raceOptions.Length; i++)
            {
                if (RayGui.GuiButton(new Rectangle(buttonX, buttonY + (ButtonHeight + Margin) * i, ButtonWidth, ButtonHeight), raceOptions[i]) == 1)
                {
                    if (Enum.TryParse(raceOptions[i], out Race selected))
                    {
                        selectedRace = selected;
                        isRaceMenuOpen = false;
                    }
                }
            }

            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY + (ButtonHeight + Margin) * raceOptions.Length, ButtonWidth, ButtonHeight), "Back") == 1)
            {
                isRaceMenuOpen = false;
            }

            Raylib.EndDrawing();
        }

        private void DrawClassSelectionMenu()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.DARKGRAY);

            int buttonX = Raylib.GetScreenWidth() / 2 - ButtonWidth / 2;
            int buttonY = Raylib.GetScreenHeight() / 2 - ButtonHeight / 2;

            for (int i = 0; i < classOptions.Length; i++)
            {
                if (RayGui.GuiButton(new Rectangle(buttonX, buttonY + (ButtonHeight + Margin) * i, ButtonWidth, ButtonHeight), classOptions[i]) == 1)
                {
                    if (Enum.TryParse(classOptions[i], out Class selected))
                    {
                        selectedClass = selected;
                        isClassMenuOpen = false;
                    }
                }
            }

            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY + (ButtonHeight + Margin) * classOptions.Length, ButtonWidth, ButtonHeight), "Back") == 1)
            {
                isClassMenuOpen = false;
            }

            Raylib.EndDrawing();
        }
    }
}
