namespace Minesweeper
{
    //The board where all the board creation, initalisation happens, cell interactions, flood fill and win/loss.
    public class Board
    {
        private Cell[,] _grid;
        private int _rows;
        private int _columns;
        
        //List of observer that listen for board events.
        private List<IBoardObserver> _observers;

        //Make sure mine generation occures after first click.
        private bool _firstClick = true;

        //Tracks how many flags are currnetly placed.
        private int _flagCount = 0;

        public Board()
        {
            _rows =  GameManager.Instance.Difficulty.Rows;
            _columns = GameManager.Instance.Difficulty.Columns;

            //Create an observer collection.
            //Stores all objects that want to be notified when something is done to the board.
            _observers = new List<IBoardObserver>();

            InitialiseBoard();
        }

        public int Rows 
        { 
            get 
            { 
                return _rows; 
            } 
        }

        public int Columns
        { 
            get 
            {
                return _columns;
            } 
        }

        public int FlagCount
        { 
            get 
            { 
                return _flagCount;
            } 
        }

        public Cell[,] Grid 
        {
            get 
            { 
                return _grid; 
            } 
        }

        //Initalises board, creates the grid with cell factory, with unrevealed cells.
        private void InitialiseBoard()
        {
            _grid = new Cell[_rows, _columns];
            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _columns; c++)
                {
                    _grid[r, c] = CellFactory.CreateCell(false, 0, r, c);
                }
            }
        }

        //Toggles a flag on a cell and updates the flag count.
        //Returns true if the flag was added, false if it was removed.
        public bool FlagCell(int row, int column)
        {
            Cell cell = _grid[row, column];

            //If revealed cells cannot be found.
            if (cell.IsRevealed)
            {
                return false;
            }

            bool wasFlagged = cell.IsFlagged;
            cell.ToggleFlag();

            //Updates the total flag count.
            if (!wasFlagged && cell.IsFlagged)
            {
                _flagCount++;
            }
            else if (wasFlagged && !cell.IsFlagged)
            {
                _flagCount--;
            }

            //Notifies observers that the cell changed.
            NotifyCellUpdated(row, column);
            return cell.IsFlagged;
        }

        //Reveals a cell and performs flood fill if needed.
        public void RevealCell(int row, int column)
        {
            Cell cell = _grid[row, column];

            // Cannot reveal a flagged cell.
            if (cell.IsFlagged)
            { 
                return;
            }

            //If cell revealed to nothing.
            if (cell.IsRevealed)
            {
                return;
            }

            //Generate/place mines and calculate where the mines are.
            if (_firstClick)
            {
                MineGenerator.PlaceMines(_grid, GameManager.Instance.Difficulty.MineCount, row, column);
                MineGenerator.CalculateNumbers(_grid);
                _firstClick = false;

                //Refresh the cell reference after the board is updated.
                cell = _grid[row, column];
            }

            //If player clicks mines cell, end game and reveal them.
            if (cell is MineCell)
            {
                cell.Reveal();
                NotifyCellUpdated(row, column);
                NotifyGameLost();
                return;
            }

            //If player clicks empty cell trigger flood fill.
            if (cell is EmptyCell)
            {
                //Measure memory allocations before floodfill.
                System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();
                long gcBefore = GC.GetAllocatedBytesForCurrentThread();

                //Start execution timer.
                System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

                //Run the flood fill.
                FloodFillService floodFill = new DepthFirstSearch();
                int noOfRevealedCells = floodFill.Reveal(_grid, row, column);

                //Stop timing.
                stopwatch.Stop();

                //Measure memory allocations after flood fill.
                long gcAfter = GC.GetAllocatedBytesForCurrentThread();
                
                //Output the statstics of performance.
                Console.WriteLine("---- Flood Fill Performance ----");
                Console.WriteLine("Cells revealed:    " + noOfRevealedCells);
                Console.WriteLine("Execution time:    " + stopwatch.Elapsed.TotalMilliseconds + " ms");
                Console.WriteLine("Memory Used:       " + (gcAfter - gcBefore) + " bytes");
            }
            else
            {
                //Reveal numberd cells normally.
                cell.Reveal();
                NotifyCellUpdated(row, column);
            }

            // Check whether all safe cells have been revealed. 
            if (CheckWin())
            {
                NotifyGameWon();
            }
        }

        // Returns a specific cell from the grid.
        public Cell GetCell(int row, int column)
        {
            return _grid[row, column];
        }

        // Registers a new observer.
        public void Subscribe(IBoardObserver observer)
        {
            _observers.Add(observer);
        }

        // Removes an observer.
        public void Unsubscribe(IBoardObserver observer)
        {
            _observers.Remove(observer);
        }

        // Notifies observers that the entire board changed.
        private void NotifyBoardUpdated()
        {
            foreach (IBoardObserver observer in _observers)
            {
                observer.BoardUpdated();
            }
        }

        // Notifies observers that a specific cell changed.
        private void NotifyCellUpdated(int row, int column)
        {
            foreach (IBoardObserver observer in _observers)
            {
                observer.CellUpdated(row, column);
            }
        }

        // Notifies observers that the game has been won.
        private void NotifyGameWon()
        {
            foreach (IBoardObserver observer in _observers)
            {
                observer.GameWon();
            }
        }

        // Notifies observers that the game has been lost.
        private void NotifyGameLost()
        {
            foreach (IBoardObserver observer in _observers)
            {
                observer.GameLost();
            }
        }

        // Determines whether all non-mine cells have been revealed.
        private bool CheckWin()
        {
            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _columns; c++)
                {
                    Cell cell = _grid[r, c];

                    //Win condition, every non-mine cell has been revealed.
                    if (!(cell is MineCell) && !cell.IsRevealed)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
