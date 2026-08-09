using SplashKitSDK;

namespace Minesweeper
{
    public abstract class Screens
    {
        protected Window _window;

        public Screens(Window window)
        {
            _window = window;
        }

        //Shared navigation bar drawn at the top of every screen.
        //Virtual so subclasses can extend it (e.g. GameScreen adds flag count).
        public virtual void NavigationBar()
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

        //Returns true if the mouse clicked the Back button this frame.
        protected bool BackButtonClicked()
        {
            if (!SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                return false;
            }

            int winWidth = (int)_window.Width;
            Point2D mouse = SplashKit.MousePosition();
            return (mouse.X >= winWidth - 110 && mouse.X <= winWidth - 20 && mouse.Y >= 12 && mouse.Y <= 48);
        }

        public abstract void TheMeatOfTheScreen();
        public abstract void HandleInput();
        public abstract void Update();
    }
}
