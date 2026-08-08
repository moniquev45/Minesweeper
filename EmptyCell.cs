namespace Minesweeper
{
    //Empty cell.
    public class EmptyCell : Cell
    {
        public EmptyCell(int row, int col) : base(row, col) { }

        public override void Reveal()
        {
            _isRevealed = true;
        }

        public override string GetDisplayValue()
        {
            return " ";
        }
    }
}
