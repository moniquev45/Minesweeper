using SplashKitSDK;

namespace Minesweeper
{
    public class GameScreen : Screens
    {
        private GameView _view;

        public GameScreen(Window window) : base(window)
        {
            Board board = GameManager.Instance.Board;
            _view = new GameView(board);
            
            //Wire up GameView as an observer for board events.
            board.Subscribe(_view);
            
            //Initial draw.
            _view.DrawBoard();
        }

        public override void NavigationBar()
        {
            int winWidth = (int)_window.Width;
            
            Rectangle navBar = new Rectangle(Color.RGBColor(30, 30, 30), 0, 0, winWidth, VisualToCell.navigationHeight, Color.RGBColor(30, 30, 30), false);
            navBar.Draw();

            //Title.
            Font font = SplashKit.LoadFont("MineFont", "assets/mine-sweeper.otf");
            SplashKit.DrawText("MINESWEEPER", Color.White, "MineFont", 10, 10, 15);

            string diffName;

            if (GameManager.Instance.Difficulty != null)
            {
                diffName = GameManager.Instance.Difficulty.Name;
            }
            else
            {
                diffName = null;
            }
            SplashKit.DrawText(diffName, Color.LightGray, "Arial", 14, 10, 38);

            // Flag count.
            int flags;
            int max;

            //Check the board for flags.
            if (GameManager.Instance.Board != null)
            {
                flags = GameManager.Instance.Board.FlagCount;
            }
            else
            {
                flags = 0;
            }

            //Check the difficulty for mines.
            if (GameManager.Instance.Difficulty != null)
            {
                max = GameManager.Instance.Difficulty.MineCount;
            }
            else
            {
                max = 0;
            }

            string flagText = $"Flags: {flags} / {max}";
            SplashKit.DrawText(flagText, Color.Yellow, "Arial", 16, winWidth / 2 - 40, 20);

            string timeText = "Time Is: 00:00:00";

            if (GameManager.Instance.Clock != null)
            {
                timeText = "Time Is: " + GameManager.Instance.Clock.ClockDisplay();
            }

            SplashKit.DrawText(timeText, Color.White, "Arial", 16, winWidth - 300, 40);

            //Back button.
            Button backButton = new Button("< Back", winWidth - 110, 12, Color.RGBColor(80, 80, 80), Color.Gray, Color.White, 90, 36, 14, "Arial", winWidth - 98, 22);
            backButton.Draw();
        }

        //Main section of the screen with the board/game.
        public override void TheMeatOfTheScreen()
        {
            //NullRefereneceException Guard for back button.
            if (GameManager.Instance.State == GameState.Title) 
            {
                return;
            }
            _view.DrawBoard();
        }

        //Handle input.
        public override void HandleInput()
        {
            Point2D mouse = SplashKit.MousePosition();
            
            int winWidth = SplashKit.ScreenWidth();

            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                //UI Click Check (Back Button).
                if (mouse.X >= winWidth - 110 && mouse.X <= winWidth - 20 && mouse.Y >= 12 && mouse.Y <= 48)
                {
                    GameManager.Instance.Reset();
                    return;
                }
            }

            //Guard: If we aren't playing, we don't process board grid clicks.
            if (GameManager.Instance.State != GameState.Playing) return;

            //Guard: Only process board clicks that are below the nav bar.
            if (mouse.Y < VisualToCell.navigationHeight) return;

            //Reveal the cell if left button clicked.
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                Cell clicked = VisualToCell.ConversionToCell((float)mouse.X, (float)mouse.Y);
                GameManager.Instance.Reveal(clicked.Row, clicked.Column);
            }

            //Flag the cell if right button clicked.
            if (SplashKit.MouseClicked(MouseButton.RightButton))
            {
                Cell clicked = VisualToCell.ConversionToCell((float)mouse.X, (float)mouse.Y);
                GameManager.Instance.Flag(clicked.Row, clicked.Column);
            }
        }

        //Updates so if any changes happen things happen.
        public override void Update()
        {
            TheMeatOfTheScreen();
            NavigationBar();
            SplashKit.RefreshScreen();
        }
    }
}
