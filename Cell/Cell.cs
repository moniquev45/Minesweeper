namespace Minesweeper
{
    //Basis for how cells are set up and the information that they can contain.
    public abstract class Cell
    {
        private int _row;
        private int _column;
        protected bool _isRevealed;
        private bool _isFlagged;

        public Cell(int row, int column)
        {
            _row = row;
            _column = column;
            _isRevealed = false;
            _isFlagged = false;
        }

        public int Row
        {
            get 
            { 
                return _row; 
            }
        }

        public int Column
        {
            get 
            { 
                return _column; 
            }
        }

        public bool IsRevealed
        {
            get 
            { 
                return _isRevealed;
            }
        }

        public bool IsFlagged
        {
            get 
            { 
                return _isFlagged;
            }
        }

        public abstract void Reveal();

        //Flag toggles on/off, a flagged cell cannot be revealed.
        public void ToggleFlag()
        {
            if (!_isRevealed)
            {
                _isFlagged = !_isFlagged;
            }
        }

        public abstract string GetDisplayValue();
    }
}
