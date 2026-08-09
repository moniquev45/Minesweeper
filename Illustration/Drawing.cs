using SplashKitSDK;

namespace Minesweeper
{
    //Drawing YAY!
    public class Drawing
    {
        private readonly List<Shape> _shapes;
        private Color _background;
        private List<Shape> _selectedShapes;

        public Drawing(Color background)
        {
            _shapes = new List<Shape>();
            _background = background;
            _selectedShapes = new List<Shape>();
        }

        public Drawing() : this(Color.White) {}

        public Color Background
        {
            get 
            { 
                return _background; 
            }
            set 
            {
                _background = value;
            }
        }

        public int ShapeCount 
        { 
            get 
            { 
                return _shapes.Count;
            }
        }

        //Add the shape.
        public void AddShape(Shape newShape)
        {
            _shapes.Add(newShape);
        }

        //Remove the shape.
        public void RemoveShape(Shape aShape)
        {
            if (aShape.DeleteAble == true)
            {
                _shapes.Remove(aShape);
            }
        }

        //Draw all of the shapes.
        public void Draw()
        {
            SplashKit.ClearScreen(_background);

            foreach (Shape shape in _shapes) 
            {
                shape.Draw();
            }
        }

        //Find where one shape is at.
        public void SelectedShapesAt(Point2D pt)
        {
            foreach (Shape shape in _shapes)
            {
                shape.Selected = shape.IsAt(pt);
            }
        }

        public List<Shape> SelectedShapes
        {
            get 
            {
                foreach (Shape shape in _shapes)
                {
                    if (shape.Selected)
                    {
                        _selectedShapes.Add(shape);
                    }
                }
                
                return _selectedShapes;
            }
        }

        //Clear all shapes.
        public void Clear()
        {
            _shapes.Clear();
        }
    }
}
