namespace SudokuDlxMaui.Demos.NQueens;

public class NQueensDrawable : IDrawable
{
  private DemoPageViewModel _demoPageViewModel;
  private float _squareWidth;
  private float _squareHeight;

  private const int N = 8;

  public NQueensDrawable(DemoPageViewModel demoPageViewModel)
  {
    _demoPageViewModel = demoPageViewModel;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _squareWidth = dirtyRect.Width / N;
    _squareHeight = dirtyRect.Height / N;
    DrawGrid(canvas);
    var internalRows = _demoPageViewModel.SolutionInternalRows.Cast<NQueensInternalRow>();
    foreach (var internalRow in internalRows)
    {
      var row = internalRow.Coords.Row;
      var col = internalRow.Coords.Col;
      DrawSquare(canvas, row, col, Colors.Red);
    }
  }

  private void DrawGrid(ICanvas canvas)
  {
    var locations =
      Enumerable.Range(0, N).SelectMany(row =>
        Enumerable.Range(0, N).Select(col =>
          new Coords(row, col))).ToArray();

    foreach (var coords in locations)
    {
      var row = coords.Row;
      var col = coords.Col;
      var colour = (row + col) % 2 == 0 ? Colors.Black : Colors.White;
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
