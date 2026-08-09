using SplashKitSDK;

namespace Minesweeper
{
    //Basis of how all the shape classes are.
    public abstract class Shape
    {
        private Color _colour;
        protected float _x;
        protected float _y;
        private bool _selected;
        protected Color _boarderColour;
        private bool _deleteAble;

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }

        public bool DeleteAble
        {
            get 
            {
                return _deleteAble;
            }
        }

        public Shape() : this(Color.Black, Color.Black, false) {}

        public Shape(Color colour, Color borderColour, bool deleteAble)
        {
            _colour = colour;
            _boarderColour = borderColour;
            xPos = 0.0f;
            yPos = 0.0f;
            _deleteAble = deleteAble;
        }

        //Setup for the shapes so they got some structure.
        public abstract void Draw();
        public abstract bool IsAt(Point2D point);
        public abstract void DrawOutLine();

        public Color Colour
        {
            get 
            {
                return _colour;
            }
            set 
            {
                _colour = value;
            }
        }

        public Color BoarderColour
        {
            get
            {
                return _boarderColour;
            }
            set
            {
                _boarderColour = value;
            }
        }

        public float xPos
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public float yPos
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }
    }
}
