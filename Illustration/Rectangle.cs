using SplashKitSDK;

namespace Minesweeper
{
    //Rectangle shape.
    public class Rectangle : Shape
    {
        private int _width;
        private int _height;

        public int Width
        {
            get 
            {
                return _width;
            }
            set 
            {
                _width = value;
            }
        }

        public int Height 
        {
            get 
            {
                return _height;
            }
            set 
            {
                _height = value;
            }
        }

        //Constructor.
        public Rectangle(Color colour, float x, float y, int width, int height, Color borderColour, bool deleteAble) : base(colour, borderColour, deleteAble)
        {
            xPos = x;
            yPos = y;
            _width = width;
            _height = height;
        }

        //Empty Constructor.
        public Rectangle() : this(Color.Green, 0.0f, 0.0f, 60, 60, Color.Black, false) { }

        //Draw the rectangle.
        public override void Draw()
        {
            SplashKit.FillRectangle(Colour, xPos, yPos, _width, _height);
            DrawOutLine();
        }

        //Draw the outline of the shape.
        public override void DrawOutLine()
        {
            SplashKit.DrawRectangle(BoarderColour, xPos, yPos, _width, _height);
        }

        //Find where the shape is at.
        public override bool IsAt(Point2D point)
        {
            return ((float)point.X > xPos && (float)point.X < xPos + _width && (float)point.Y > yPos && (float)point.Y < yPos + _height);
        }
    }
}
