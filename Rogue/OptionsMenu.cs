using ZeroElectric.Vinculum;

namespace Rogue
{
    public class OptionsMenu
    {
        private Game game;

        public OptionsMenu(Game game)
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

            if (RayGui.GuiButton(new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), "Return to Menu") == 1)
            {
                game.ChangeState(GameState.MainMenu);
            }

            Raylib.EndDrawing();
        }
    }
}
