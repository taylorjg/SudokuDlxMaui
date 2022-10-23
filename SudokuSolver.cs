using DlxLib;
using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public static class SudokuSolver
{
  private static readonly Coords[] AllCoords =
    Enumerable.Range(0, 9).SelectMany(row =>
        Enumerable.Range(0, 9).Select(col =>
          new Coords(row, col))).ToArray();

  private static readonly int[] AllValues = Enumerable.Range(1, 9).ToArray();

  private static T Identity<T>(T t) => t;

  public static GridValue[] Solve(GridValue[] gridValues)
  {
    var internalRows = BuildInternalRows(gridValues);
    var matrix = BuildMatrix(internalRows);
    var dlx = new DlxLib.Dlx();
    var solutions = dlx.Solve(matrix, Identity, Identity).ToArray();
    if (solutions.Length == 1)
    {
      return solutions[0].RowIndexes.Select(rowIndex => internalRows[rowIndex]).ToArray();
    }
    return null;
  }

  private static GridValue[] BuildInternalRows(GridValue[] gridValues)
  {
    return AllCoords.SelectMany(coords =>
    {
      var gridValue = gridValues.FirstOrDefault(gv => gv.Coords == coords);
      if (gridValue != null) return new[] { gridValue };
      return BuildInternalRowsForUnknownValue(coords);
    }).ToArray();
  }

  private static GridValue[] BuildInternalRowsForUnknownValue(Coords coords)
  {
    return AllValues.Select(value => new GridValue(coords, value, false)).ToArray();
  }

  private static int[][] BuildMatrix(GridValue[] internalRows)
  {
    return internalRows.Select(BuildMatrixRow).ToArray();
  }

  private static int[] BuildMatrixRow(GridValue internalRow)
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

  private static int RowColToBox(int row, int col)
  {
    return row - (row % 3) + (col / 3);
  }

  private static int[] OneHot(int major, int minor)
  {
    var columns = Enumerable.Repeat(0, 81).ToArray();
    columns[major * 9 + minor] = 1;
    return columns;
  }
}
