namespace Minesweeper
{
    public class EasyDifficulty : Difficulty
    {
        //10 rows, 8 cols, 10 mines, 60x60 cells, 480w x 660h window, 20 number cell font size.
        public EasyDifficulty() : base(10, 8, 10, 60, 60, 660, 480, 20, "Easy") { }
    }
}
