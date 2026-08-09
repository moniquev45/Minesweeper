using SplashKitSDK;

namespace Minesweeper
{
    //Full title screen with difficulty selection (button cycling) and a Play button.
    public class TitleScreen : Screens
    {
        private int _selectedDifficulty = 0; //0=Easy, 1=Medium, 2=Hard.
        private readonly string[] _difficultyNames = { "Easy", "Medium", "Hard" };

        // Button layout constants.
        private const int buttonWidth = 200;
        private const int buttonHeight = 50;
        private const int titleWidth = 480;

        public TitleScreen(Window window) : base(window) { }

        public override void NavigationBar()
        {
            //Title screen has a simpler nav, just the game title, no Back button.
            SplashKit.FillRectangle(Color.RGBColor(30, 30, 30), 0, 0, titleWidth, VisualToCell.navigationHeight);
            Font font = SplashKit.LoadFont("MineFont", "assets/mine-sweeper.otf");
            SplashKit.DrawText("MINESWEEPER", Color.White, "MineFont", 10, 10, 15);
        }

        public override void TheMeatOfTheScreen()
        {
            Font font = SplashKit.LoadFont("MineFont", "assets/mine-sweeper.otf");
            int winH = (int)_window.Height;

            //Background.
            SplashKit.FillRectangle(Color.RGBColor(50, 50, 50), 0, VisualToCell.navigationHeight, titleWidth, winH - VisualToCell.navigationHeight);

            //Big title text.
            SplashKit.DrawText("MINESWEEPER", Color.White, "MineFont", 36, 25, 100);

            //Difficulty label.
            SplashKit.DrawText("Select Difficulty:", Color.LightGray, "Arial", 18, 140, 200);

            //--- Difficulty cycle button ---
            int diffBtnX = (titleWidth - buttonWidth) / 2;
            int diffBtnY = 235;

            Color diffFill;

            if (_selectedDifficulty == 0) 
            {
                diffFill = Color.Green;       // Easy
            } 
            else if (_selectedDifficulty == 1) 
            {
                diffFill = Color.Orange;      // Medium
            } 
            else 
            {
                diffFill = Color.Red;         // Hard
            }

            Button difficultyButton = new Button(_difficultyNames[_selectedDifficulty], diffBtnX, diffBtnY, diffFill, Color.White, Color.White, buttonWidth, buttonHeight, 20, "Arial", buttonWidth - 100 / 2 + diffBtnX - 70, buttonHeight / 2 + diffBtnY - 5);
            difficultyButton.Draw();
            //Small hint text.
            SplashKit.DrawText("(click to cycle)", Color.Gray, "MineFont", 10, diffBtnX + 25, diffBtnY + buttonHeight + 5);

            //--- Play button ---
            int playBtnY = 340;
            Button playButton = new Button("PLAY", diffBtnX, playBtnY, Color.RGBColor(0, 120, 200), Color.White, Color.White, buttonWidth, buttonHeight, 20, "Arial", buttonWidth - 100 / 2 + diffBtnX - 70, buttonHeight / 2 + playBtnY - 5);
            playButton.Draw();
        }

        //Handles input.
        public override void HandleInput()
        {
            if (!SplashKit.MouseClicked(MouseButton.LeftButton)) 
            {
                return;
            }

            Point2D mouse = SplashKit.MousePosition();

            int diffBtnX = (titleWidth - buttonWidth) / 2;
            int diffBtnY = 235;
            int playBtnY = 340;

            //Difficulty cycle.
            if (mouse.X >= diffBtnX && mouse.X <= diffBtnX + buttonWidth && mouse.Y >= diffBtnY && mouse.Y <= diffBtnY + buttonHeight)
            {
                _selectedDifficulty = (_selectedDifficulty + 1) % 3;
            }

            //Play.
            if (mouse.X >= diffBtnX && mouse.X <= diffBtnX + buttonWidth && mouse.Y >= playBtnY && mouse.Y <= playBtnY + buttonHeight)
            {
               Difficulty chosen;

                if (_selectedDifficulty == 0)
                {
                    chosen = (Difficulty)new EasyDifficulty();
                }
                else if (_selectedDifficulty == 1)
                {
                    chosen = new MediumDifficulty();
                }
                else
                {
                    chosen = new HardDifficulty();
                }

                GameManager.Instance.SetDifficulty(chosen);
                GameManager.Instance.StartGame();
                //GameManager.State is now Playing, Program.cs loop will switch to GameScreen.
            }
        }

        //Updates so if any changes happen things happen.       
        public override void Update()
        {
            SplashKit.ClearScreen(Color.RGBColor(50, 50, 50));
            NavigationBar();
            TheMeatOfTheScreen();
            SplashKit.RefreshScreen();
        }
    }
}
