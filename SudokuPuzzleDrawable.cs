using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public class SudokuPuzzleDrawable : IDrawable
{
  private MainPageViewModel _mainPageViewModel;
  private ILogger<SudokuPuzzleDrawable> _logger;
  private float _gridLineFullThickness;
  private float _gridLineHalfThickness;
  private float _gridLineQuarterThickness;
  private float _squareWidth;
  private float _squareHeight;

  public SudokuPuzzleDrawable(MainPageViewModel mainPageViewModel, ILogger<SudokuPuzzleDrawable> logger)
  {
    _mainPageViewModel = mainPageViewModel;
    _logger = logger;
    _logger.LogInformation("[constructor]");
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _gridLineFullThickness = dirtyRect.Width / 100;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _gridLineQuarterThickness = _gridLineFullThickness / 4;
    _squareWidth = (dirtyRect.Width - _gridLineFullThickness) / 9;
    _squareHeight = (dirtyRect.Height - _gridLineFullThickness) / 9;
    DrawGrid(canvas);
  }

  private void DrawGrid(ICanvas canvas)
  {
    DrawHorizontalGridLines(canvas);
    DrawVerticalGridLines(canvas);
    foreach (GridValue gridValue in _mainPageViewModel.GridValues)
    {
      DrawDigit(canvas, gridValue.Coords.Row, gridValue.Coords.Col, gridValue.Value, gridValue.IsInitialValue);
    }
  }

  private void DrawDigit(ICanvas canvas, int row, int col, int value, bool isInitialValue)
  {
    var valueString = value.ToString();
    var x1 = _squareWidth * (col + 0) + _gridLineHalfThickness;
    var x2 = _squareWidth * (col + 1) + _gridLineHalfThickness;
    var y1 = _squareHeight * (row + 0) + _gridLineHalfThickness;
    var y2 = _squareHeight * (row + 1) + _gridLineHalfThickness;
    canvas.FontColor = isInitialValue ? Colors.Magenta : Colors.Black;
    canvas.FontSize = _squareWidth * 0.75f;
    canvas.DrawString(valueString, x1, y1, x2 - x1, y2 - y1, HorizontalAlignment.Center, VerticalAlignment.Center);
  }

  private void DrawHorizontalGridLines(ICanvas canvas)
  {
    foreach (var row in Enumerable.Range(0, 10))
    {
      var isThickLine = row % 3 == 0;
      var full = isThickLine ? _gridLineFullThickness : _gridLineHalfThickness;
      var half = isThickLine ? _gridLineHalfThickness : _gridLineQuarterThickness;
      var x1 = 0;
      var y1 = _squareHeight * row + half;
      var x2 = 9 * _squareWidth + _gridLineFullThickness;
      var y2 = _squareHeight * row + half;
      canvas.StrokeColor = Colors.Black;
      canvas.StrokeSize = full;
      canvas.DrawLine(x1, y1, x2, y2);
    }
  }

  private void DrawVerticalGridLines(ICanvas canvas)
  {
    foreach (var col in Enumerable.Range(0, 10))
    {
      var isThickLine = col % 3 == 0;
      var full = isThickLine ? _gridLineFullThickness : _gridLineHalfThickness;
      var half = isThickLine ? _gridLineHalfThickness : _gridLineQuarterThickness;
      var x1 = _squareWidth * col + half;
      var y1 = _gridLineHalfThickness;
      var x2 = _squareWidth * col + half;
      var y2 = _gridLineHalfThickness + 9 * _squareHeight;
      canvas.StrokeColor = Colors.Black;
      canvas.StrokeSize = full;
      canvas.DrawLine(x1, y1, x2, y2);
    }
  }
}
