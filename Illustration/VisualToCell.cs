using SplashKitSDK;

namespace Minesweeper
{
    //Conversion from pixel to cell and cell to pixels.
    public static class VisualToCell
    {
        public const int navigationHeight = 60;

        //Convert from pixel to cell.
        public static Cell ConversionToCell(float mouseX, float mouseY)
        {
            int cellLength = GameManager.Instance.Difficulty.LengthOfCell;
            int cellWidth = GameManager.Instance.Difficulty.WidthOfCell;

            int cellRow = (int)((mouseY - navigationHeight) / cellLength);
            int cellColumn = (int)(mouseX / cellWidth);

            int rows = GameManager.Instance.Board.Rows;
            int cols = GameManager.Instance.Board.Columns;

            //Clamp to valid range to avoid out of bounds on edge clicks.
            cellRow = Math.Clamp(cellRow, 0, rows - 1);
            cellColumn = Math.Clamp(cellColumn, 0, cols - 1);

            return GameManager.Instance.Board.GetCell(cellRow, cellColumn);
        }

        //Convert from cells to pixels.
        public static (int pixelX, int pixelY) ConversionToPixels(int row, int column)
        {
            int cellLength = GameManager.Instance.Difficulty.LengthOfCell;
            int cellWidth = GameManager.Instance.Difficulty.WidthOfCell;

            int pixelY = (row * cellLength) + navigationHeight;
            int pixelX = column * cellWidth;

            return (pixelX, pixelY);
        }
    }
}
