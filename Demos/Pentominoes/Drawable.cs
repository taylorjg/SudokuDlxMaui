namespace SudokuDlxMaui.Demos.Pentominoes;

public class PentominoesDrawable : IDrawable
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
    _width = dirtyRect.Width;
    _height = dirtyRect.Height;
    _gridLineFullThickness = _width / 100;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _squareWidth = (_width - _gridLineFullThickness) / 8;
    _squareHeight = (_height - _gridLineFullThickness) / 8;
    _squareWidth2 = _width / 8;
    _squareHeight2 = _height / 8;

    DrawGrid(canvas);

    var solutionInternalRows = _whatToDraw.SolutionInternalRows.Cast<PentominoesInternalRow>();

    foreach (var internalRow in solutionInternalRows)
    {
      DrawShape(canvas, internalRow);
    }

    if (solutionInternalRows.Any())
    {
      DrawCentreShape(canvas);
    }
  }

  private void DrawGrid(ICanvas canvas)
  {
    canvas.SaveState();
    canvas.BlendMode = BlendMode.Copy;
    DrawHorizontalGridLines(canvas);
    DrawVerticalGridLines(canvas);
    canvas.RestoreState();
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

  private void DrawCentreShape(ICanvas canvas)
  {
    var fakeLabel = " ";
    var fakeCoordsList = new[] {
      new Coords(0, 0),
      new Coords(1, 0),
      new Coords(1, 1),
      new Coords(0, 1),
    };
    var fakeVariation = new Variation(Orientation.North, false, fakeCoordsList);
    var fakeLocation = new Coords(3, 3);
    var fakeInternalRow = new PentominoesInternalRow(fakeLabel, fakeVariation, fakeLocation);
    DrawShape(canvas, fakeInternalRow);
  }

  private void DrawShape(ICanvas canvas, PentominoesInternalRow internalRow)
  {
    var colour = PieceColours.GetValueOrDefault(internalRow.Label) ?? Colors.White;

    foreach (var coords in internalRow.Variation.CoordsList)
    {
      var row = internalRow.Location.Row + coords.Row;
      var col = internalRow.Location.Col + coords.Col;
      DrawSquare(canvas, row, col, colour);
      DrawLabel(canvas, row, col, internalRow.Label);
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

  private void DrawLabel(ICanvas canvas, int row, int col, string label)
  {
    var x = _squareWidth2 * col;
    var y = _squareHeight2 * row;
    var width = _squareWidth2;
    var height = _squareHeight2;

    canvas.SaveState();
    canvas.FontColor = Colors.White;
    canvas.FontSize = _squareWidth2 * 0.25f;
    canvas.SetShadow(new SizeF(2, 2), 2, Colors.Black);
    canvas.DrawString(
      label,
      x,
      y,
      width,
      height,
      HorizontalAlignment.Center,
      VerticalAlignment.Center
    );
    canvas.RestoreState();
  }
}
