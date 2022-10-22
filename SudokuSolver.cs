using DlxLib;
using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public static class SudokuSolver
{
  private static readonly int[] Digits = Enumerable.Range(1, 9).ToArray();

  private record InternalRow(Coords Coords, int Value, bool IsInitialValue);

  public static GridValue[] Solve(GridValue[] gridValues)
  {
    var internalRows = BuildInternalRows(gridValues);
    var matrix = BuildMatrix(internalRows);
    var dlx = new DlxLib.Dlx();
    var solutions = dlx.Solve(matrix, r => r, c => c).ToArray();
    if (solutions.Length == 1) {
      var solution = solutions[0];
      var selectedInternalRows = solution.RowIndexes.Select(internalRowIndex => internalRows[internalRowIndex]);
      return selectedInternalRows.Select(internalRow => new GridValue(internalRow.Coords, internalRow.Value, internalRow.IsInitialValue)).ToArray();
    }
    return null;
  }

  private static InternalRow[] BuildInternalRows(GridValue[] gridValues)
  {
    var allCoords = Enumerable.Range(0, 9).SelectMany(row =>
      Enumerable.Range(0, 9).Select(col =>
        new Coords(row, col))).ToArray();
    return allCoords.SelectMany(coords =>
    {
      var gridValue = gridValues.FirstOrDefault(gv => gv.Coords == coords);
      if (gridValue != null)
      {
        var internalRow = BuildInternalRowForInitialValue(gridValue.Coords, gridValue.Value);
        return new[] { internalRow };
      }
      return BuildInternalRowsForUnknownValue(coords);
    }).ToArray();
  }

  private static InternalRow BuildInternalRowForInitialValue(Coords coords, int value)
  {
    return new InternalRow(coords, value, true);
  }

  private static InternalRow[] BuildInternalRowsForUnknownValue(Coords coords)
  {
    return Digits.Select(value => new InternalRow(coords, value, false)).ToArray();
  }

  private static int[][] BuildMatrix(InternalRow[] internalRows)
  {
    return internalRows.Select(BuildMatrixRow).ToArray();
  }

  private static int[] BuildMatrixRow(InternalRow internalRow)
  {
    var zeroBasedValue = internalRow.Value - 1;
    var row = internalRow.Coords.Row;
    var col = internalRow.Coords.Col;
    var box = RowColToBox(row, col);
    var posColumns = OneHot(row, col);
    var rowColumns = OneHot(row, zeroBasedValue);
    var colColumns = OneHot(col, zeroBasedValue);
    var boxColumns = OneHot(box, zeroBasedValue);
    var combinedColumns = new[] { posColumns, rowColumns, colColumns, boxColumns }.SelectMany(xs => xs);
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
