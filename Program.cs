using SplashKitSDK;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            //Title screen uses a fixed 480x400 window.
            Window window = new Window("Minesweeper", 480, 500);
            int windowX = SplashKit.CurrentWindowX();
            int windowY = 0;
            SplashKit.MoveCurrentWindowTo(windowX, windowY);


            TitleScreen titleScreen = new TitleScreen(window);
            Screens currentScreen = titleScreen;

            GameState lastState = GameState.Title;

            while (!SplashKit.WindowCloseRequested("Minesweeper"))
            {
                SplashKit.ProcessEvents();

                GameState state = GameManager.Instance.State;

                //--- Screen transitions ---

                //Title, Playing: Resize window to match difficulty and build GameScreen.
                if (lastState != GameState.Playing && state == GameState.Playing)
                {
                    int w = GameManager.Instance.Difficulty.WindowWidth;
                    int h = GameManager.Instance.Difficulty.WindowLength;
                    window.Resize(w, h);
                    currentScreen = new GameScreen(window);
                    lastState = state;
                }

                //Playing, Won or Lost: Show game over overlay on same window.
                else if (lastState == GameState.Playing && (state == GameState.Won || state == GameState.Lost))
                {
                    bool won = state == GameState.Won;
                    currentScreen = new GameOverScreen(window, won);
                    lastState = state;
                }

                //Won/Lost, Title: Reset and rebuild title screen.
                else if ((lastState == GameState.Won || lastState == GameState.Lost) && state == GameState.Title)
                {
                    window.Resize(480, 500);
                    titleScreen = new TitleScreen(window);
                    currentScreen = titleScreen;
                    lastState = state;
                }

                //Makes back button work when not backing out of win or loss
                else if (state == GameState.Title && lastState != GameState.Title)
                {
                    window.Resize(480, 500);
                    titleScreen = new TitleScreen(window);
                    currentScreen = titleScreen;
                    lastState = state;
                }

                GameManager.Instance.Update();
                currentScreen.HandleInput();
                currentScreen.Update();
            }
        }
    }
}
