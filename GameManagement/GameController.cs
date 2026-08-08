using SplashKitSDK;

namespace Minesweeper
{
    //Handles player input and transulates mouse actions to game actions.
    public class GameController
    {
        private Board _board;

        //Reference to the view (rendering layer).
        private GameView _view;

        public GameController(Board board, GameView view)
        {
            _board = board;
            _view  = view;
        }

        //Processes user input each frame.
        public void HandleInput()
        {
            //Ignore input if the game is not currently active.
            if (GameManager.Instance.State != GameState.Playing) 
            {
                return;
            }
            
            Point2D mouse = SplashKit.MousePosition();

            //Prevent clicks from affecting the top navigation bar area.
            if (mouse.Y < VisualToCell.navigationHeight) 
            {
                return;
            }

            //Left-click reveals a cell.
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                Cell clicked = VisualToCell.ConversionToCell((float)mouse.X, (float)mouse.Y);

                // Trigger reveal logic through the GameManager.
                GameManager.Instance.Reveal(clicked.Row, clicked.Column);
            }

            //Right-click toggles a flag on a cell.
            if (SplashKit.MouseClicked(MouseButton.RightButton))
            {
                Cell clicked = VisualToCell.ConversionToCell((float)mouse.X, (float)mouse.Y);
                
                //Trigger flag logic through the GameManager.
                GameManager.Instance.Flag(clicked.Row, clicked.Column);
            }
        }
    }
}
