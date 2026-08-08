namespace Minesweeper
{
    //Defines what observers want to be notified about changes in the Board.
    public interface IBoardObserver
    {
        void CellUpdated(int row, int col);

        void GameWon();

        void BoardUpdated();
        
        void GameLost();
    }
}
