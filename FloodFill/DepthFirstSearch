namespace Minesweeper
{
    //Implements Depth-First Search (DFS) flood fill for Minesweeper.
    //This approach recursively reveals connected empty cells and stops at numbered cells.
    public class DepthFirstSearch : FloodFillService
    {
        //Recursively reveals cells starting from (r, c).
        //Returns the total number of cells revealed in this branch.
        public int Reveal(Cell[,] grid, int r, int c)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            int noOfRevealedCells = 0;

            //Bounds check.
            if (r < 0 || r >= rows || c < 0 || c >= cols)
            {
                return 0;
            }

            Cell cell = grid[r, c];

            //Stop if cell is already revealed or flagged.
            if (cell.IsRevealed || cell.IsFlagged)
            {
                return 0;
            }

            //Reveal current cell and count it.
            noOfRevealedCells += 1;
            cell.Reveal();

            // Stop if number cell.
            if (cell is NumberCell num && num.AdjacentMines > 0)
            {
                return noOfRevealedCells;
            }

            //Recursively explore all 8 neighbouring cells.
            for (int dr = -1; dr <= 1; dr++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    // Skip the current cell itself.
                    if (dr == 0 && dc == 0) 
                    {
                        continue;
                    }

                    // Add results from recursive flood fill.
                    noOfRevealedCells += Reveal(grid, r + dr, c + dc);
                }
            }
            return noOfRevealedCells;
        }
    }
}
