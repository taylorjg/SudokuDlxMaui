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

    DrawGrid(canvas);

    var solutionInternalRows = _whatToDraw.SolutionInternalRows.Cast<DraughtboardPuzzleInternalRow>();
    foreach (var internalRow in solutionInternalRows)
    {
      DrawShape(canvas, internalRow);
    }
  }

  private void DrawGrid(ICanvas canvas)
  {
    canvas.SaveState();
    canvas.BlendMode = BlendMode.Copy;
    DrawHorizontalGridLines(canvas);
    DrawVerticalGridLines(canvas);
    canvas.RestoreState();

    FillAlternateGridSquares(canvas);
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

  private void FillAlternateGridSquares(ICanvas canvas)
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

    DrawShapeBorder(canvas, internalRow);
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

  private void DrawShapeBorder(ICanvas canvas, DraughtboardPuzzleInternalRow internalRow)
  {
    var outsideEdges = GatherOutsideEdges(internalRow);
    var borderLocations = OutsideEdgesToBorderLocations(outsideEdges);
    var path = CreateBorderPath(borderLocations);

    canvas.SaveState();
    canvas.StrokeColor = Color.FromRgba("#0066CC");
    canvas.StrokeSize = _squareWidth2 * 0.1f;
    canvas.ClipPath(path);
    canvas.DrawPath(path);
    canvas.RestoreState();
  }

  private record OutsideEdge(Coords Location1, Coords Location2);

  private List<OutsideEdge> GatherOutsideEdges(DraughtboardPuzzleInternalRow internalRow)
  {
    var pieceExistsAt = (int row, int col) => Array.Exists(
      internalRow.Variation.Squares,
      square => square.Coords.Row == row && square.Coords.Col == col
    );

    var makeOutsideEdge = (int row1, int col1, int row2, int col2) =>
      new OutsideEdge(
        new Coords(row1 + internalRow.Location.Row, col1 + internalRow.Location.Col),
        new Coords(row2 + internalRow.Location.Row, col2 + internalRow.Location.Col)
      );

    var outsideEdges = new List<OutsideEdge>();

    foreach (var square in internalRow.Variation.Squares)
    {
      var row = square.Coords.Row;
      var col = square.Coords.Col;

      // top outside edge ?
      if (!pieceExistsAt(row - 1, col))
      {
        outsideEdges.Add(makeOutsideEdge(row, col, row, col + 1));
      }

      // bottom outside edge ?
      if (!pieceExistsAt(row + 1, col))
      {
        outsideEdges.Add(makeOutsideEdge(row + 1, col, row + 1, col + 1));
      }

      // left outside edge ?
      if (!pieceExistsAt(row, col - 1))
      {
        outsideEdges.Add(makeOutsideEdge(row, col, row + 1, col));
      }

      // right outside edge ?
      if (!pieceExistsAt(row, col + 1))
      {
        outsideEdges.Add(makeOutsideEdge(row, col + 1, row + 1, col + 1));
      }
    }

    return outsideEdges;
  }

  private List<Coords> OutsideEdgesToBorderLocations(List<OutsideEdge> outsideEdges)
  {
    var borderLocations = new List<Coords>();
    var seenOutsideEdges = new List<OutsideEdge>();

    var findNextOutsideEdge = (Coords coords) =>
      outsideEdges.Except(seenOutsideEdges).FirstOrDefault(outsideEdge =>
        outsideEdge.Location1 == coords ||
        outsideEdge.Location2 == coords
      );

    var firstOutsideEdge = outsideEdges.First();
    borderLocations.Add(firstOutsideEdge.Location1);
    borderLocations.Add(firstOutsideEdge.Location2);
    seenOutsideEdges.Add(firstOutsideEdge);

    for (; ; )
    {
      var mostRecentLocation = borderLocations.Last();
      var nextOutsideEdge = findNextOutsideEdge(mostRecentLocation);
      var nextLocation = nextOutsideEdge.Location1 == mostRecentLocation
        ? nextOutsideEdge.Location2
        : nextOutsideEdge.Location1;
      if (nextLocation == borderLocations.First()) break;
      borderLocations.Add(nextLocation);
      seenOutsideEdges.Add(nextOutsideEdge);
    }

    return borderLocations;
  }

  private PathF CreateBorderPath(List<Coords> borderLocations)
  {
    var locationToPoint = (Coords location) =>
      new PointF(location.Col * _squareWidth2, location.Row * _squareHeight2);

    var path = new PathF();
    path.MoveTo(locationToPoint(borderLocations.First()));
    foreach (var location in borderLocations.Skip(1))
    {
      path.LineTo(locationToPoint(location));
    }
    path.Close();
    return path;
  }
}
