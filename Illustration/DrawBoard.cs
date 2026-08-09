using SplashKitSDK;

namespace Minesweeper
{
    public class DrawBoard
    {
        private int _cellLength;
        private int _cellWidth;
        private readonly Drawing _drawing;

        //Colours for the different numbers for the number cells.
        private static readonly Color[] NumberColors =
        {
            Color.Blue, Color.Green, Color.Red, Color.DarkBlue, 
            Color.DarkRed, Color.Teal, Color.Black, Color.Gray
        };

        public DrawBoard()
        {
            _drawing = new Drawing(Color.LightGray);
        }

        //Drawing the board
        public void DrawThisBoard(Board board)
        {
            _cellLength = GameManager.Instance.Difficulty.LengthOfCell;
            _cellWidth  = GameManager.Instance.Difficulty.WidthOfCell;
            int fontSize = GameManager.Instance.Difficulty.FontSize;

            _drawing.Clear();
            _drawing.Draw();

            //Draw each cell in the board.
            for (int row = 0; row < board.Rows; row++)
            {
                for (int col = 0; col < board.Columns; col++)
                {
                    Cell cell = board.Grid[row, col];
                    DrawCell(cell, row, col, fontSize);
                }
            }
        }

        //If they only want one cell drawn.
        public void DrawCellOnly(Board board, int row, int col)
        {
            _cellLength = GameManager.Instance.Difficulty.LengthOfCell;
            _cellWidth  = GameManager.Instance.Difficulty.WidthOfCell;
            int fontSize = GameManager.Instance.Difficulty.FontSize;

            Cell cell = board.Grid[row, col];
            DrawCell(cell, row, col, fontSize);

            DrawNavBar();
            SplashKit.RefreshScreen();
        }

        //Draw the cell when cell is known.
        private void DrawCell(Cell cell, int row, int col, int fontSize)
        {
            (int x, int y) = VisualToCell.ConversionToPixels(row, col);

            //Revealed vs unrevealed background.
            Color fillColor;
            Color boarderColour;

            //If cell is revealed cell is grey if not green.
            if (cell.IsRevealed) 
            {
                fillColor = Color.LightGray;
                boarderColour = Color.Gray;
            } 
            else 
            {
                fillColor = Color.OliveDrab;
                boarderColour = Color.DarkOliveGreen;
            }

            //Create the create cell visual.
            Rectangle cellBox = new Rectangle(fillColor, x, y, _cellWidth, _cellLength, boarderColour, false);
            cellBox.Draw();

            //If cell is revealed, display number for number cell, red circle for mine.
            if (cell.IsRevealed)
            {
                if (cell is MineCell)
                {
                    int cx = x + _cellWidth / 2;
                    int cy = y + _cellLength / 2;
                    int r  = Math.Min(_cellWidth, _cellLength) / 4;
                    
                    Circle mineCircle = new Circle(Color.Red, cx, cy, r, Color.DarkRed, false);
                    mineCircle.Draw();
                }
                else if (cell is NumberCell numCell)
                {
                    Color textColor;

                    if (numCell.AdjacentMines >= 1 && numCell.AdjacentMines <= 8) 
                    {
                        textColor = NumberColors[numCell.AdjacentMines - 1];
                    } 
                    else 
                    {
                        textColor = Color.Black;
                    }

                    //Draw number on cell.
                    Font font = SplashKit.LoadFont("MineFont", "/assets/mine-sweeper.otf");
                    SplashKit.DrawText(numCell.GetDisplayValue(), textColor, "MineFont", fontSize, x + _cellWidth  / 4, y + _cellLength / 4);
                }
            }

            //If it is a flagged cell, and obvi not revealed then draw blue circle.
            if (cell.IsFlagged && !cell.IsRevealed)
            {
                int cx = x + _cellWidth  / 2;
                int cy = y + _cellLength / 2;
                int r  = _cellWidth / 3;

                Circle flagCircle = new Circle(Color.Blue, cx, cy, r, Color.DarkBlue, false);
                flagCircle.Draw();
            }
        }

        //Draw navigation bar.
        public void DrawNavBar()
        {
            //Safeguard against nulls if redrawing immediately after resetting.
            int winWidth;
            if (GameManager.Instance.Difficulty != null)
            {
                winWidth = GameManager.Instance.Difficulty.WindowWidth; 
            }
            else
            {
                winWidth = SplashKit.ScreenWidth(); // Fallback to full screen/default width.
            }
            int navH = VisualToCell.navigationHeight;

            SplashKit.FillRectangle(SplashKit.RGBColor(40, 40, 40), 0, 0, winWidth, navH);
            SplashKit.DrawText("MINESWEEPER", Color.White, "Arial", 18, 10, 15);

            // Flag count.
            int flags;
            if (GameManager.Instance.Board != null) 
            {
                flags = GameManager.Instance.Board.FlagCount;
            } 
            else 
            {
                flags = 0; // Fallback if the board isn't loaded.
            }

            int max;

            //Max amount of mines.
            if (GameManager.Instance.Difficulty != null)
            {
                max = GameManager.Instance.Difficulty.MineCount; 
            }
            else
            {
                max = 0; // Fallback if no difficulty is loaded, there are 0 mines.
            }

            string flagText = $"Flags: {flags} / {max}";
            SplashKit.DrawText(flagText, Color.Yellow, "Arial", 14, winWidth - 140, 20);
        }
    }
}
