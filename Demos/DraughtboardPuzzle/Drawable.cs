namespace SudokuDlxMaui.Demos.DraughtboardPuzzle;

public class DraughtboardPuzzleDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _squareWidth;
  private float _squareHeight;

  public DraughtboardPuzzleDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _squareWidth = dirtyRect.Width / 8;
    _squareHeight = dirtyRect.Height / 8;
    var solutionInternalRows = _whatToDraw.SolutionInternalRows.Cast<DraughtboardPuzzleInternalRow>();
    foreach (var internalRow in solutionInternalRows)
    {
      DrawSquares(canvas, internalRow);
    }
  }

  private void DrawSquares(ICanvas canvas, DraughtboardPuzzleInternalRow internalRow)
  {
    foreach (var square in internalRow.Variation.Squares)
    {
      var coords = square.Coords;
      var row = internalRow.Location.Row + coords.Row;
      var col = internalRow.Location.Col + coords.Col;
      var colour = square.Colour == Colour.Black ? Colors.Black : Colors.White;
      DrawSquare(canvas, row, col, colour);
    }
  }

  private void DrawSquare(ICanvas canvas, int row, int col, Color colour)
  {
    var x = _squareWidth * col;
    var y = _squareHeight * row;
    var width = _squareWidth;
    var height = _squareHeight;

    canvas.FillColor = colour;
    canvas.FillRectangle(x, y, width, height);
  }
}
