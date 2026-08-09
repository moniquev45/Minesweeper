using SplashKitSDK;

namespace Minesweeper
{
    //Display overlay when the player finishes the game.
    public class GameOverScreen : Screens
    {
        private bool _playerWon;

        public GameOverScreen(Window window, bool playerWon) : base(window)
        {
            _playerWon = playerWon;
        }

        //Navigation bar with back button.
        public override void NavigationBar()
        {
            int winWidth = (int)_window.Width;

            Rectangle navBar = new Rectangle(Color.RGBColor(30, 30, 30), 0, 0, winWidth, VisualToCell.navigationHeight, Color.RGBColor(30, 30, 30), false);
            navBar.Draw();
            
            Font font = SplashKit.LoadFont("MineFont", "assets/mine-sweeper.otf");
            SplashKit.DrawText("MINESWEEPER", Color.White, "MineFont", 10, 10, 15);

            //Back button.
            Button backButton = new Button("< Back", winWidth - 110, 12, Color.RGBColor(80, 80, 80), Color.Gray, Color.White, 90, 36, 14, "Arial", winWidth - 98, 22);
            backButton.Draw();
        }

        //The main section of the screen saying if player won or lost.
        public override void TheMeatOfTheScreen()
        {
            int winWidth = (int)_window.Width;
            int winHeight = (int)_window.Height;

            //Result panel.
            int panelW = 320;
            int panelH = 180;
            int panelX = (winWidth - panelW) / 2;
            int panelY = (winHeight - panelH) / 2;

            Rectangle panel = new Rectangle(Color.RGBColor(20, 20, 20), panelX, panelY, panelW, panelH, Color.White, false);
            panel.Draw();

            SplashKit.DrawText("Final Time: " + GameManager.Instance.Clock.ClockDisplay(), Color.White, "Arial", 20, panelX + 30, panelY + 70);

            if (_playerWon)
            {
                //Tell the player they won.
                SplashKit.DrawText("YOU WIN!", Color.LimeGreen, "Arial", 36, panelX + 30, panelY + 30);
                SplashKit.DrawText("Congratulations!", Color.White, "Arial", 16, panelX + 30, panelY + 90);
            }
            else
            {
                //Tell the player they lost.
                SplashKit.DrawText("GAME OVER", Color.Red, "Arial", 32, panelX + 30, panelY + 30);
                SplashKit.DrawText("Better luck next time.", Color.White, "Arial", 16, panelX + 30, panelY + 90);
            }

            SplashKit.DrawText("Click < Back to return to menu.", Color.Gray, "Arial", 13, panelX + 30, panelY + 130);
        }

        public override void HandleInput()
        {
            // Back button click, reset game and return to title.
            if (BackButtonClicked())
            {
                GameManager.Instance.Reset();
            }
        }

        //Updates so if any changes happen things happen.
        public override void Update()
        {
            NavigationBar();
            TheMeatOfTheScreen();
            SplashKit.RefreshScreen();
        }
    }
}
