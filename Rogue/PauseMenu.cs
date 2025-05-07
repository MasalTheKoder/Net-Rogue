using ZeroElectric.Vinculum;

namespace Rogue
{
    public class PauseMenu
    {
        private Game game;

        public PauseMenu(Game game)
        {
            this.game = game;
        }

        public void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.DARKGRAY);

            int buttonWidth = 200;
            int buttonHeight = 40;
            int buttonX = Raylib.GetScreenWidth() / 2 - buttonWidth / 2;
            int buttonY = Raylib.GetScreenHeight() / 2 - buttonHeight / 2;

            RayGui.GuiLabel(new Rectangle(buttonX, buttonY - buttonHeight * 2, buttonWidth, buttonHeight), "Pause Menu");

            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), "Resume") == 1)
            {
                game.ChangeState(GameState.Playing);
            }

            buttonY += buttonHeight * 2;

            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), "Options") == 1)
            {
                game.ChangeState(GameState.Options);  // Use GameState.Options instead of GameState.Settings
            }

            buttonY += buttonHeight * 2;

            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), "Exit to Main Menu") == 1)
            {
                game.ChangeState(GameState.MainMenu);
            }

            Raylib.EndDrawing();
        }
    }
}
