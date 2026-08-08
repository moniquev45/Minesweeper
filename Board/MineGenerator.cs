namespace Minesweeper
{
    //Creates mines making it solvable, and generates the numbers around the mines.
    public static class MineGenerator
    {
        private static Random _random = new Random();

        public static void PlaceMines(Cell[,] grid, int mineCount, int playerClickedRow, int playerClickedColumn)
        {
            //Get the length of rows [#,]
            int row = grid.GetLength(0);
            //Get the length of columns [,#]
            int column = grid.GetLength(1);

            // Maximum allowed mines near the first-click area (based on edges/corners).
            int bombsInRadius = BorderMaxBombs(grid, playerClickedRow, playerClickedColumn);
            
            //Adds more randomness to the amount of bombs allowed to be near the player.
            int ranMaxNumberOfFreeMinesAroundThePlayer = _random.Next(0, bombsInRadius);

            int numberOfPlacedMines = 0;

            // Continue placing mines until target count is reached.
            while (numberOfPlacedMines < mineCount)
            {
                int ranRow = _random.Next(0, row);
                int ranColumn = _random.Next(0, column);

                // Skip if a mine already exists at this location.
                if (grid[ranRow, ranColumn] is MineCell)
                {
                    continue;
                }

                // Ensure placement respects the safe zone around first click.
                bool safeToPlace = SafeZoneCheck(grid, ranMaxNumberOfFreeMinesAroundThePlayer, playerClickedRow, playerClickedColumn, ranRow, ranColumn);

                if (!safeToPlace)
                {
                    continue;
                }

                // Prevent overly dense mine clusters.
                if (WouldCauseCluster(grid, ranRow, ranColumn))
                {
                    continue;
                }

                // Reduce safe zone if placing inside outer safe radius.
                if (ranRow >= playerClickedRow - 2 && ranRow <= playerClickedRow + 2 && ranColumn >= playerClickedColumn - 2 && ranColumn <= playerClickedColumn + 2)
                {
                    ranMaxNumberOfFreeMinesAroundThePlayer--;
                }

                // Place mine.
                grid[ranRow, ranColumn] = new MineCell(ranRow, ranColumn);
                numberOfPlacedMines++;
            }
        }


        public static bool SafeZoneCheck(Cell[,] grid, int ranCurrentMaxNumberOfFreeMinesAroundThePlayer, int playerRow, int playerCol, int ranRow, int ranCol)
        {
            // If within 5x5 area around first click.
            if (ranRow >= playerRow - 2 && ranRow <= playerRow + 2 && ranCol >= playerCol - 2 && ranCol <= playerCol + 2)
            {
                // Inner 3x3 zone is BANNED :).
                if (ranRow >= playerRow - 1 && ranRow <= playerRow + 1 && ranCol >= playerCol - 1 && ranCol <= playerCol + 1)
                {
                    return false; //3x3 core, never place mine here.
                }
                else
                {
                    //Outer ring, only if they have enough mines to allow it.
                    if (ranCurrentMaxNumberOfFreeMinesAroundThePlayer > 0 )
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        //Determines the maximum number of mines allowed near edges/corners, prevents being stuck and confused in corner, if player clicks the corner.
        public static int BorderMaxBombs(Cell[,] grid, int playerClickedRow, int playerClickedColumn)
        {
            //Get the length of rows [#,]
            int maxRow = grid.GetLength(0) - 1;
        
            int maxColumn = grid.GetLength(1) - 1;

            // Check if first click is in a corner.
            bool isCorner = (playerClickedRow == 0    && playerClickedColumn == 0) || (playerClickedRow == 0 && playerClickedColumn == maxColumn) || (playerClickedRow == maxRow && playerClickedColumn == 0) || (playerClickedRow == maxRow && playerClickedColumn == maxColumn);

            if (isCorner == true) 
            {
                return 2;
            }

            // Check if first click is on an edge.
            bool isEdge = playerClickedColumn == 0 || playerClickedRow == 0 || playerClickedRow == maxRow || playerClickedColumn == maxColumn;

            if (isEdge == true) 
            {
                return 4;
            }

            return 6;
        }

        // Converts a mine-filled grid into NumberCell and EmptyCell representations.
        public static void CalculateNumbers(Cell[,] grid)
        {
            //Get the length of rows [#,]
            int rows = grid.GetLength(0);
            //Get the length of rows [#,]
            int columns = grid.GetLength(1);

            //So we can read the grid and not overwrite whats actually there.
            Cell[,] newGrid = new Cell[rows, columns];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    //Skip over mines.
                    if (grid[r, c] is MineCell)
                    {
                        newGrid[r, c] = grid[r, c];
                        continue;
                    }

                    int numberOfBombsAroundTheCell = 0;

                    // Check all surrounding 8 neighbors.
                    for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
                    {
                        for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
                        {
                            if (rowOffset == 0 && columnOffset == 0) 
                            {
                                continue;
                            }

                            int neighborRow = r + rowOffset;
                            int neighborColumn = c + columnOffset;

                            //Checking if this is a place it can search and if it has a mine.
                            if (neighborRow >= 0 && neighborRow < rows && neighborColumn >= 0 && neighborColumn < columns && grid[neighborRow, neighborColumn] is MineCell)
                            {
                                numberOfBombsAroundTheCell++;
                            }
                        }
                    }

                    //Create appropriate cell type based on mine count.
                    if (numberOfBombsAroundTheCell == 0)
                    {
                        newGrid[r, c] = new EmptyCell(r, c);
                    }
                    else
                    {
                        newGrid[r, c] = new NumberCell(r, c, numberOfBombsAroundTheCell);
                    }
                }
            }

            //Copy calculated grid back into original grid.
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    grid[r, c] = newGrid[r, c];
                }
            }
        }

        // Checks whether placing a mine would create an overly dense cluster.
        public static bool WouldCauseCluster(Cell[,] grid, int row, int column)
        {
            int rows = grid.GetLength(0);
            int columns = grid.GetLength(1);

            //Check 3x3 aread.
            for (int r = row - 1; r <= row + 1; r++)
            {
                for (int c = column - 1; c <= column + 1; c++)
                {
                    if (r < 0 || r >= rows || c < 0 || c >= columns) 
                    {
                        continue;
                    }

                    int count = 0;

                    // Count mines in neighborhood.
                    for (int rr = r - 1; rr <= r + 1; rr++)
                    {
                        for (int cc = c - 1; cc <= c + 1; cc++)
                        {
                            if (rr < 0 || rr >= rows || cc < 0 || cc >= columns) continue;
                            if (grid[rr, cc] is MineCell) count++;
                        }
                    }

                    //Pretend if it was a mine and adding it to count.
                    if (!(grid[row, column] is MineCell)) 
                    {
                        count++;
                    }

                    //If to many mines, mine cant be put here.
                    if (count > 6)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
