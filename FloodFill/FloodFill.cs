namespace Minesweeper
{
    //Interface for the floodfills BFS and DFS.
    public interface FloodFillService
    {
        int Reveal(Cell[,] grid, int row, int column);
    }
}
