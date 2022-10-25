using DlxLib;
using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public class SudokuSolver : ISudokuSolver
{
  private static readonly Coords[] AllCoords =
    Enumerable.Range(0, 9).SelectMany(row =>
        Enumerable.Range(0, 9).Select(col =>
          new Coords(row, col))).ToArray();

  private static readonly int[] AllValues = Enumerable.Range(1, 9).ToArray();

  private static T Identity<T>(T t) => t;

  private ILogger<SudokuSolver> _logger;

  public SudokuSolver(ILogger<SudokuSolver> logger)
  {
    _logger = logger;
  }

  public GridValue[] Solve(GridValue[] gridValues)
  {
    var internalRows = BuildInternalRows(gridValues);
    var matrix = BuildMatrix(internalRows);
    var dlx = new DlxLib.Dlx();
    var steps = new List<int[]>();
    dlx.SearchStep += (object sender, DlxLib.SearchStepEventArgs e) => steps.Add(e.RowIndexes.ToArray());
    var solutions = dlx.Solve(matrix, Identity, Identity).ToArray();
    _logger.LogInformation($"[Solve] solutons.length: {solutions.Length}");
    _logger.LogInformation($"[Solve] teps.Count: {steps.Count}");
    if (solutions.Length == 1)
    {
      return solutions[0].RowIndexes.Select(rowIndex => internalRows[rowIndex]).ToArray();
    }
    return null;
  }

  private GridValue[] BuildInternalRows(GridValue[] gridValues)
  {
    return AllCoords.SelectMany(coords =>
    {
      var gridValue = gridValues.FirstOrDefault(gv => gv.Coords == coords);
      if (gridValue != null) return new[] { gridValue };
      return BuildInternalRowsForUnknownValue(coords);
    }).ToArray();
  }

  private GridValue[] BuildInternalRowsForUnknownValue(Coords coords)
  {
    return AllValues.Select(value => new GridValue(coords, value, false)).ToArray();
  }

  private int[][] BuildMatrix(GridValue[] internalRows)
  {
    return internalRows.Select(BuildMatrixRow).ToArray();
  }

  private int[] BuildMatrixRow(GridValue internalRow)
  {
    var zeroBasedValue = internalRow.Value - 1;
    var row = internalRow.Coords.Row;
    var col = internalRow.Coords.Col;
    var box = RowColToBox(row, col);
    var posColumns = OneHot(row, col);
    var rowColumns = OneHot(row, zeroBasedValue);
    var colColumns = OneHot(col, zeroBasedValue);
    var boxColumns = OneHot(box, zeroBasedValue);
    var combinedColumns = new[] { posColumns, rowColumns, colColumns, boxColumns }.SelectMany(Identity);
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
