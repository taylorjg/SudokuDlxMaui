using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui.Demos.Sudoku;

public class SudokuDlxLibDemo : IDlxLibDemo
{
  private ILogger<SudokuDlxLibDemo> _logger;

  public SudokuDlxLibDemo(ILogger<SudokuDlxLibDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(DemoPageBaseViewModel demoPageBaseViewModel)
  {
    return new SudokuDrawable(demoPageBaseViewModel, _logger);
  }

  public object[] BuildInternalRows(object demoSettings)
  {
    var puzzle = demoSettings as Puzzle;
    var internalRows = puzzle.InternalRows;
    return AllCoords.SelectMany(coords =>
    {
      var internalRow = internalRows.FirstOrDefault(gv => gv.Coords == coords);
      if (internalRow != null) return new[] { internalRow };
      return BuildInternalRowsForUnknownValue(coords);
    }).ToArray();
  }

  public int[][] BuildMatrix(object[] internalRows)
  {
    return (internalRows as SudokuInternalRow[]).Select(BuildMatrixRow).ToArray();
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    return null;
  }

  private static readonly Coords[] AllCoords =
    Enumerable.Range(0, 9).SelectMany(row =>
        Enumerable.Range(0, 9).Select(col =>
          new Coords(row, col))).ToArray();

  private static readonly int[] AllValues = Enumerable.Range(1, 9).ToArray();

  private SudokuInternalRow[] BuildInternalRowsForUnknownValue(Coords coords)
  {
    return AllValues.Select(value => new SudokuInternalRow(coords, value, false)).ToArray();
  }

  private int[] BuildMatrixRow(SudokuInternalRow internalRow)
  {
    var zeroBasedValue = internalRow.Value - 1;
    var row = internalRow.Coords.Row;
    var col = internalRow.Coords.Col;
    var box = RowColToBox(row, col);
    var posColumns = OneHot(row, col);
    var rowColumns = OneHot(row, zeroBasedValue);
    var colColumns = OneHot(col, zeroBasedValue);
    var boxColumns = OneHot(box, zeroBasedValue);
    var combinedColumns = new[] { posColumns, rowColumns, colColumns, boxColumns }.SelectMany(cols => cols);
    return combinedColumns.ToArray();
  }

  private int RowColToBox(int row, int col)
  {
    return row - (row % 3) + (col / 3);
  }

  private int[] OneHot(int major, int minor)
  {
    var columns = Enumerable.Repeat(0, 81).ToArray();
    columns[major * 9 + minor] = 1;
    return columns;
  }
}
