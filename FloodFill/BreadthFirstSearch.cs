namespace Minesweeper
{
    //Implements Breadth-First Search (BFS) flood fill for Minesweeper.
    // his is used to automatically reveal connected empty cells and stop at numbered cells.
    public class BreadthFirstSearch : FloodFillService
    {
        //Performs a BFS flood fill starting from the clicked cell.
        //Returns the total number of cells revealed during the process.
        public int Reveal(Cell[,] grid, int startRow, int startCol)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            int noOfRevealedCells = 0;
            
            //Queue used to process cells in BFS order (level by level).
            Queue<(int r, int c)> queue = new Queue<(int r, int c)>();

            //Start from the initial clicked cell.
            queue.Enqueue((startRow, startCol));

            while (queue.Count > 0)
            {
                var (r, c) = queue.Dequeue();

                //Skip out of bounds positions.
                if (r < 0 || r >= rows || c < 0 || c >= cols)
                {
                    continue;
                }

                Cell cell = grid[r, c];

                //Skip already revealed or flagged cells.
                if (cell.IsRevealed || cell.IsFlagged)
                {
                    continue;
                }

                //Reveal this cell and count it.
                noOfRevealedCells += 1;
                cell.Reveal();

                //Stop expanding into neighbours if this cell has adjacent mines.
                if (cell is NumberCell num && num.AdjacentMines > 0)
                {
                    continue;
                }

                //Add all 8 neighbouring cells to the queue for processing.
                for (int dr = -1; dr <= 1; dr++)
                {
                    for (int dc = -1; dc <= 1; dc++)
                    {
                        // Skip the current cell itself.
                        if (dr == 0 && dc == 0) 
                        {
                            continue;
                        }

                        queue.Enqueue((r + dr, c + dc));
                    }
                }
            }
            return noOfRevealedCells;
        }
    }
}
