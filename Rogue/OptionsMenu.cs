using Rogue;
using ZeroElectric.Vinculum;

public class OptionsMenu
{
    private Game game;
    private GameState returnToState;

    public OptionsMenu(Game game)
    {
        this.game = game;
        this.returnToState = GameState.MainMenu;
    }

    public void SetReturnState(GameState state)
    {
        returnToState = state;
    }

    public void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Raylib.BLACK);

        int buttonWidth = 200;
        int buttonHeight = 40;
        int buttonX = Raylib.GetScreenWidth() / 2 - buttonWidth / 2;
        int buttonY = Raylib.GetScreenHeight() / 2 - buttonHeight / 2;

        if (RayGui.GuiButton(new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), "Return") == 1)
        {
            game.ChangeState(returnToState);
        }

        Raylib.EndDrawing();
    }
}
