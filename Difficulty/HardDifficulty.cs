namespace Minesweeper
{
    public class HardDifficulty : Difficulty
    {
        //24 rows, 20 cols, 99 mines, 25x25 cells, 500w x 660h window, 10 number cell font size.
        public HardDifficulty() : base(24, 20, 99, 25, 25, 660, 500, 10, "Hard") { }
    }
}
