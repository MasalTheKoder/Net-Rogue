using RayGuiCreator;
using ZeroElectric.Vinculum;

namespace Rogue
{
    public class PauseMenu
    {
        private Game game;
        private OptionsMenu optionsMenu;

        public PauseMenu(Game game, OptionsMenu optionsMenu)
        {
            this.game = game;
            this.optionsMenu = optionsMenu;
        }

        /// <summary>
        /// Draws the pause menu
        /// </summary>
        public void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);

            int buttonWidth = 200;
            int buttonHeight = 40;
            int buttonX = Raylib.GetScreenWidth() / 2 - buttonWidth / 2;
            int buttonY = Raylib.GetScreenHeight() / 2 - buttonHeight * 3;

            MenuCreator menu = new MenuCreator(buttonX, buttonY, buttonHeight, buttonWidth);
            menu.Label("Pause Menu");

            if (menu.Button("Resume"))
                game.ChangeState(GameState.Playing);

            menu.Label("");

            if (menu.Button("Settings"))
            {
                optionsMenu.SetReturnState(GameState.Pause);
                game.ChangeState(GameState.Options);
            }

            menu.Label("");

            if (menu.Button("Exit to Main Menu"))
                game.ChangeState(GameState.MainMenu);

            Raylib.EndDrawing();
        }
    }
}
