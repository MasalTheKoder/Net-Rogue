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
            Raylib.ClearBackground(Raylib.BLACK);

            int buttonWidth = 200;
            int buttonHeight = 40;
            int buttonX = Raylib.GetScreenWidth() / 2 - buttonWidth / 2;
            int buttonY = Raylib.GetScreenHeight() / 2 - buttonHeight / 2;

            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), "Settings") == 1)
            {
                game.optionsMenu.SetReturnState(GameState.Paused);
                game.ChangeState(GameState.Options);
            }

            buttonY += buttonHeight * 2;

            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), "Close Menu") == 1)
            {
                game.ChangeState(GameState.Playing);
            }

            Raylib.EndDrawing();
        }
    }
}
