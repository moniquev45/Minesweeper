namespace Minesweeper
{
    //Number cell and its number of adjacent mines.
    public class NumberCell : Cell
    {
        private int _adjacentMines;

        public NumberCell(int row, int col, int adjacentMines)
            : base(row, col)
        {
            _adjacentMines = adjacentMines;
        }

        public int AdjacentMines
        {
            get 
            {
                return _adjacentMines;
            }
        }

        public override void Reveal()
        {
            _isRevealed = true;
        }

        public override string GetDisplayValue()
        {
            return _adjacentMines.ToString();
        }
    }
}
