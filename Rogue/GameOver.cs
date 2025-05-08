using System.Numerics;
using ZeroElectric.Vinculum;

namespace Rogue
{
    /// <summary>
    /// Displays the Game Over screen.
    /// </summary>
    class GameOver
    {
        private Game game;

        public GameOver(Game game)
        {
            this.game = game;
        }

        /// <summary>
        /// Draws the Game Over screen with an option to return to the main menu.
        /// </summary>
        public void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);

            // Get screen dimensions
            int screenWidth = Raylib.GetScreenWidth();
            int screenHeight = Raylib.GetScreenHeight();

            // Define the text to display
            string gameOverText = "You found the sword of legacy!!!";

            // Calculate the appropriate font size to fit the screen
            int fontSize = screenWidth / 10;  // Scale font size relative to screen width
            if (fontSize > 40) fontSize = 40; // Set a maximum font size

            // Calculate text size and position to center it
            Vector2 textSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), gameOverText, fontSize, 1);
            int textX = (screenWidth - (int)textSize.X) / 2;
            int textY = screenHeight / 4 - (int)textSize.Y / 2;

            // Draw the game over text
            Raylib.DrawTextEx(Raylib.GetFontDefault(), gameOverText, new Vector2(textX, textY), fontSize, 1, Raylib.DARKGRAY);

            // Define button dimensions
            int buttonWidth = 200;
            int buttonHeight = 40;
            int buttonX = screenWidth / 2 - buttonWidth / 2;
            int buttonY = screenHeight / 2 + 40;

            // Draw the "Back to Menu" button
            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), "Main Menu") == 1)
            {
                game.ChangeState(GameState.MainMenu);
            }

            Raylib.EndDrawing();
        }
    }
}
