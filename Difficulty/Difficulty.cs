namespace Minesweeper
{
    //Sets up the different difficulties.
    public abstract class Difficulty
    {
        private int _rows;
        private int _columns;
        private int _mineCount;
        private int _lengthOfCell;
        private int _widthOfCell;
        private int _windowLength;
        private int _windowWidth;
        private int _fontSize;
        private string _name;

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

        public int MineCount
        {
            get
            {
                return _mineCount;
            }
        }

        public int LengthOfCell
        { 
            get
            {
                return _lengthOfCell;
            }
        }

        public int WidthOfCell
        {
            get
            {
                return _widthOfCell;
            }
        }

        public int WindowLength
        {
            get
            {
                return _windowLength;
            }
        }

        public int WindowWidth
        {
            get
            {
                return _windowWidth;
            }
        }

        public int FontSize
        {
            get
            {
                return _fontSize;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        protected Difficulty(int rows, int columns, int mineCount, int lengthOfCell, int widthOfCell, int windowLength, int windowWidth, int fontSize, string name)
        {
            _rows = rows;
            _columns = columns;
            _mineCount = mineCount;
            _lengthOfCell = lengthOfCell;
            _widthOfCell = widthOfCell;
            _windowLength = windowLength;
            _windowWidth = windowWidth;
            _fontSize = fontSize;
            _name = name;
        }
    }
}
