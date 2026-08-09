using SplashKitSDK;

namespace Minesweeper
{
    //Making drawing buttons much easier.
    public class Button : Shape
    {
        private string _label;
        private int _width;
        private int _height;
        private Color _textColour;
        private int _textFontSize;
        private string _textFontType;
        private int _width2;
        private int _height2;

        public string Label     
        {
            get
            {
                return _label;
            }
        }

        public int WidthButton
        {
            get
            {
                return _width;
            }
        }

        public int HeightButton
        {
            get
            {
                return _height;
            }
        }

        public int WidthButton2
        {
            get
            {
                return _width2;
            }
        }

        public int HeightButton2
        {
            get
            {
                return _height2;
            }
        }

        public int TextFontSize
        {
            get
            {
                return _textFontSize;
            }
        }

        public string TextFontType
        {
            get
            {
                return _textFontType;
            }
        }

        public Color TextColour
        {
            get
            {
                return _textColour;
            }
        }

        //Constructor.
        public Button(string label, float x, float y, Color colour, Color borderColour, Color textColour, int width, int height, int textFontSize, string textFontType, int width2, int height2) : base(colour, borderColour, false)
        {
            _label = label;
            xPos = x;
            yPos = y;
            _width = width;
            _height = height;
            _textColour = textColour;
            _textFontSize = textFontSize;
            _textFontType = textFontType;
            _width2 = width2;
            _height2 = height2;
        }

        //Empty case constructor.
        public Button() : this("Stinky", 0, 0, Color.MistyRose, Color.LightPink, Color.Black, 30, 40, 10, "MineFont", 0, 0) {}

        //Draw the button.
        public override void Draw()
        {
            SplashKit.FillRectangle(Colour, xPos, yPos, WidthButton, HeightButton);
            DrawOutLine();
            SplashKit.DrawText(Label, _textColour, TextFontType, TextFontSize, WidthButton2, HeightButton2);
        } 

        //If button is clicked.
        public bool IsClicked()
        {
            return (SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.PointInRectangle(SplashKit.MousePosition(), SplashKit.RectangleFrom(xPos, yPos, WidthButton, HeightButton)));
        }

        //Draw outline of box.
        public override void DrawOutLine()
        {
            SplashKit.DrawRectangle(BoarderColour, xPos , yPos,  _width, _height);
        }

        //Where is it at in the board.
        public override bool IsAt(Point2D pt)
        {
            return (SplashKit.PointInRectangle(pt, SplashKit.RectangleFrom(xPos, yPos, WidthButton, HeightButton)));
        }
    }
}
