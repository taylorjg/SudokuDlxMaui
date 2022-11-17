namespace SudokuDlxMaui.Demos.Pentominoes;

public class PentominoesDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _squareWidth;
  private float _squareHeight;

  private static readonly Dictionary<string, Color> PieceColours = new Dictionary<string, Color>
  {
    { "F", Color.FromRgba("#CCCCE5") },
    { "I", Color.FromRgba("#650205") },
    { "L", Color.FromRgba("#984D11") },
    { "N", Color.FromRgba("#FFFD38") },
    { "P", Color.FromRgba("#FD8023") },
    { "T", Color.FromRgba("#FC2028") },
    { "U", Color.FromRgba("#7F1CC9") },
    { "V", Color.FromRgba("#6783E3") },
    { "W", Color.FromRgba("#0F7F12") },
    { "X", Color.FromRgba("#FC1681") },
    { "Y", Color.FromRgba("#29FD2F") },
    { "Z", Color.FromRgba("#CCCA2A") }
  };

  public PentominoesDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _squareWidth = dirtyRect.Width / 8;
    _squareHeight = dirtyRect.Height / 8;
    var solutionInternalRows = _whatToDraw.SolutionInternalRows.Cast<PentominoesInternalRow>();
    foreach (var internalRow in solutionInternalRows)
    {
      DrawSquares(canvas, internalRow);
    }
  }

  private void DrawSquares(ICanvas canvas, PentominoesInternalRow internalRow)
  {
    var colour = PieceColours[internalRow.Label];
    foreach (var coords in internalRow.Variation.CoordsList)
    {
      var row = internalRow.Location.Row + coords.Row;
      var col = internalRow.Location.Col + coords.Col;
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
