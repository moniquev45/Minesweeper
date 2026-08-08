namespace Minesweeper
{
    public class MediumDifficulty : Difficulty
    {
    // 18 rows,148 cols, 40 mines, 33x33 cells, 462w x 654h window, 15 number cell font size.
        public MediumDifficulty() : base(18, 14, 40, 33, 33, 654, 462, 15, "Medium") { }
    }
}
