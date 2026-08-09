using SplashKitSDK;

namespace Minesweeper
{
    public class Circle : Shape
    {
        private int _radius;

        public int Radius
        {
            get 
            { 
                return _radius;
            }
            set 
            {
                _radius = value;
            }
        }

        //Empty constructor.
        public Circle() : this(Color.Maroon, 10, Color.Firebrick, false) {}

        //If you dont have a specific position.
        public Circle(Color colour, int radius, Color borderColour, bool deleteAble) : base(colour, borderColour, deleteAble)
        {
            Radius = radius;
        }

        //Constructor for placing at a specific position.
        public Circle(Color colour, float x, float y, int radius, Color borderColour, bool deleteAble) : base(colour, borderColour, deleteAble)
        {
            xPos = x;
            yPos = y;
            Radius = radius;
        }

        //Draw circle.
        public override void Draw()
        {
            SplashKit.FillCircle(Colour, xPos, yPos, _radius);
            DrawOutLine();
        }

        //Draw outline of circle.
        public override void DrawOutLine()
        {
            SplashKit.DrawCircle(BoarderColour, xPos, yPos, _radius + 2);
        }

        //Where is it at in the board.
        public override bool IsAt(Point2D point)
        {
            float xPoint = (float)point.X - xPos;
            float yPoint = (float)point.Y - yPos;

            if ((xPoint * xPoint) + (yPoint * yPoint) <= _radius * _radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
