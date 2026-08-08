namespace Minesweeper
{
    public class CellFactory
    {
        //Creates the cells, general rule for the factory.
        public static Cell CreateCell(bool hasMine, int adjacentMines, int row, int col)
        {
            //Good for potental pre-loading of grids.
            if (hasMine)
            {
                return new MineCell(row, col);
            }
            if (adjacentMines > 0)
            {
                return new NumberCell(row, col, adjacentMines);
            }

            return new EmptyCell(row, col);
        }
    }
}
