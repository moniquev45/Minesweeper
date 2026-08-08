namespace Minesweeper
{
    //Mine cells.
    public class MineCell : Cell
    {
        public MineCell(int row, int col) : base(row, col) { }

        public override void Reveal()
        {
            _isRevealed = true;
        }

        public override string GetDisplayValue()
        {
            return "*";
        }
    }
}
