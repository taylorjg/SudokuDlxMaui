using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui.Demos.NQueens;

public class NQueensDlxLibDemo : IDlxLibDemo
{
  private ILogger<NQueensDlxLibDemo> _logger;
  private const int N = 8;

  private static readonly Coords[] Locations =
    Enumerable.Range(0, N).SelectMany(row =>
      Enumerable.Range(0, N).Select(col =>
        new Coords(row, col))).ToArray();

  public NQueensDlxLibDemo(ILogger<NQueensDlxLibDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(DemoPageViewModel demoPageViewModel)
  {
    return new NQueensDrawable(demoPageViewModel);
  }

  public object[] BuildInternalRows(object inputData)
  {
    return Locations.Select(coords => new NQueensInternalRow(coords)).ToArray();
  }

  public int[][] BuildMatrix(object[] internalRows)
  {
    return (internalRows as NQueensInternalRow[]).Select(BuildMatrixRow).ToArray();
  }

  public int? GetNumPrimaryColumns(object inputData)
  {
    return N + N;
  }

  private int[] BuildMatrixRow(NQueensInternalRow internalRow)
  {
    var row = internalRow.Coords.Row;
    var col = internalRow.Coords.Col;
    var diagonalColumnCount = N + N - 3;

    var rowColumns = Enumerable.Repeat(0, N).ToArray();
    var colColumns = Enumerable.Repeat(0, N).ToArray();
    var diagonal1Columns = Enumerable.Repeat(0, diagonalColumnCount).ToArray();
    var diagonal2Columns = Enumerable.Repeat(0, diagonalColumnCount).ToArray();

    rowColumns[row] = 1;
    colColumns[col] = 1;

    var diagonal1 = row + col - 1;
    if (diagonal1 >= 0 && diagonal1 < diagonalColumnCount) diagonal1Columns[diagonal1] = 1;

    var diagonal2 = N - 1 - col + row - 1;
    if (diagonal2 >= 0 && diagonal2 < diagonalColumnCount) diagonal2Columns[diagonal2] = 1;

    var combinedColumns = new[] { rowColumns, colColumns, diagonal1Columns, diagonal2Columns }.SelectMany(col => col);
    return combinedColumns.ToArray();
  }
}
