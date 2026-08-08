using SplashKitSDK;

namespace Minesweeper
{
    // OBSERVER design pattern: GameView subscribes to the Board and redraws on each event.
    public class GameView : IBoardObserver
    {
        private Board _board;
        private DrawBoard _drawer;

        public GameView(Board board)
        {
            _board  = board;
            _drawer = new DrawBoard();
        }

        //Manual full board draw call (used externally if needed).
        public void DrawBoard()
        {
            _drawer.DrawThisBoard(_board);
        }

        //Called when any board-wide change occurs.
        public void BoardUpdated()
        {
            _drawer.DrawThisBoard(_board);
        }

        //Called when a single cell changes state.
        public void CellUpdated(int row, int col)
        {
            //Previously attempted partial redraw caused UI inconsistencies, nav bar not updating correctly), so full redraw is used instead.
            _drawer.DrawThisBoard(_board);
        }

        //Called when the player wins the game.
        public void GameWon()
        {
            //Ensure final board state is rendered.
            _drawer.DrawThisBoard(_board);
            //Overlay is shown in GameScreen.Update() when State == Won.
        }

        public void GameLost()
        {
            //Reveal all mines visually.
            RevealAllMines();
            _drawer.DrawThisBoard(_board);
            //Overlay is shown in GameScreen.Update() when State == Lost.
        }

        //Reveals all mine cells so the full board state is visible after losing.
        private void RevealAllMines()
        {
            for (int row = 0; row < _board.Rows; row++)
            {
                for (int column = 0; column < _board.Columns; column++)
                {
                    if (_board.Grid[row, column] is MineCell mine && !mine.IsRevealed)
                    {
                        mine.Reveal();
                    }
                }
            }
        }
    }
}
