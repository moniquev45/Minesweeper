namespace Minesweeper
{
    // OBSERVER design pattern: GameObserver sits between the Board (subject) and the GameManager.
    // The Board fires events: GameObserver translates them into GameManager state changes.
    public class GameObserver : IBoardObserver
    {
        private GameManager _manager;

        public GameObserver(GameManager manager)
        {
            _manager = manager;
        }

        public void CellUpdated(int row, int col)
        {
            // Individual cell change, GameView handles the visual, nothing to do here.
        }

        public void BoardUpdated()
        {
            // Full board change, GameView handles the visual.
        }

        public void GameWon()
        {
            _manager.Win();
        }

        public void GameLost()
        {
            _manager.Lose();
        }
    }
}
