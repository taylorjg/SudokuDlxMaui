namespace SudokuDlxMaui.Demos.DraughtboardPuzzle;

public class DraughtboardPuzzleDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _width;
  private float _height;
  private float _gridLineFullThickness;
  private float _gridLineHalfThickness;
  private float _squareWidth;
  private float _squareHeight;
  private float _squareWidth2;
  private float _squareHeight2;
  private readonly Color _gridColour = Color.FromRgba("#CD853F80");

  public DraughtboardPuzzleDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _width = dirtyRect.Width;
    _height = dirtyRect.Height;
    _gridLineFullThickness = dirtyRect.Width / 100;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _squareWidth = (dirtyRect.Width - _gridLineFullThickness) / 8;
    _squareHeight = (dirtyRect.Height - _gridLineFullThickness) / 8;
    _squareWidth2 = dirtyRect.Width / 8;
    _squareHeight2 = dirtyRect.Height / 8;

    canvas.BlendMode = BlendMode.Copy;
    DrawGrid(canvas);
    FillOddGridSquares(canvas);
    canvas.BlendMode = BlendMode.Normal;

    var solutionInternalRows = _whatToDraw.SolutionInternalRows.Cast<DraughtboardPuzzleInternalRow>();
    foreach (var internalRow in solutionInternalRows)
    {
      DrawShape(canvas, internalRow);
    }
  }

  private void DrawGrid(ICanvas canvas)
  {
    DrawHorizontalGridLines(canvas);
    DrawVerticalGridLines(canvas);
  }

  private void DrawHorizontalGridLines(ICanvas canvas)
  {
    foreach (var row in Enumerable.Range(0, 9))
    {
      var x1 = 0;
      var y1 = _squareHeight * row + _gridLineHalfThickness;
      var x2 = _width;
      var y2 = _squareHeight * row + _gridLineHalfThickness;
      canvas.StrokeColor = _gridColour;
      canvas.StrokeSize = _gridLineFullThickness;
      canvas.DrawLine(x1, y1, x2, y2);
    }
  }

  private void DrawVerticalGridLines(ICanvas canvas)
  {
    foreach (var col in Enumerable.Range(0, 9))
    {
      var x1 = _squareWidth * col + _gridLineHalfThickness;
      var y1 = 0;
      var x2 = _squareWidth * col + _gridLineHalfThickness;
      var y2 = _height;
      canvas.StrokeColor = _gridColour;
      canvas.StrokeSize = _gridLineFullThickness;
      canvas.DrawLine(x1, y1, x2, y2);
    }
  }

  private void FillOddGridSquares(ICanvas canvas)
  {
    foreach (var row in Enumerable.Range(0, 8))
    {
      foreach (var col in Enumerable.Range(0, 8))
      {
        if ((row + col) % 2 == 0)
        {
          var x = _squareWidth * col + _gridLineHalfThickness;
          var y = _squareHeight * row + _gridLineHalfThickness;
          var width = _squareWidth;
          var height = _squareHeight;
          var rect = new RectF(x, y, width, height);
          var factor = -0.1f;
          rect = rect.Inflate(_squareWidth * factor, _squareHeight * factor);
          canvas.FillColor = _gridColour;
          canvas.FillRectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }
      }
    }
  }

  private void DrawShape(ICanvas canvas, DraughtboardPuzzleInternalRow internalRow)
  {
    foreach (var square in internalRow.Variation.Squares)
    {
      var coords = square.Coords;
      var row = internalRow.Location.Row + coords.Row;
      var col = internalRow.Location.Col + coords.Col;
      var squareColour = square.Colour == Colour.Black ? Colors.Black : Colors.White;
      var labelColour = square.Colour == Colour.Black ? Colors.White : Colors.Black;
      DrawSquare(canvas, row, col, squareColour);
      DrawLabel(canvas, row, col, internalRow.Label, labelColour);
    }
  }

  private void DrawSquare(ICanvas canvas, int row, int col, Color colour)
  {
    var x = _squareWidth2 * col;
    var y = _squareHeight2 * row;
    var width = _squareWidth2;
    var height = _squareHeight2;

    canvas.FillColor = colour;
    canvas.FillRectangle(x, y, width, height);
  }

  private void DrawLabel(ICanvas canvas, int row, int col, string label, Color colour)
  {
    var x = _squareWidth2 * col;
    var y = _squareHeight2 * row;
    var width = _squareWidth2;
    var height = _squareHeight2;

    canvas.FontColor = colour;
    canvas.FontSize = _squareWidth2 * 0.25f;
    canvas.DrawString(
      label,
      x,
      y,
      width,
      height,
      HorizontalAlignment.Center,
      VerticalAlignment.Center
    );
  }
}
