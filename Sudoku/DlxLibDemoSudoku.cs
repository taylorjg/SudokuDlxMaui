namespace SudokuDlxMaui;

public class DlxLibDemoSudoku : IDlxLibDemo
{
  public IDrawable CreateDrawable(MainPageViewModel mainPageViewModel)
  {
    return new SudokuDrawable(mainPageViewModel);
  }

  public object[] BuildInternalRows(object inputData)
  {
    var gridValues = inputData as GridValue[];
    return AllCoords.SelectMany(coords =>
    {
      var gridValue = gridValues.FirstOrDefault(gv => gv.Coords == coords);
      if (gridValue != null) return new[] { gridValue };
      return BuildInternalRowsForUnknownValue(coords);
    }).ToArray();
  }

  public int[][] BuildMatrix(object[] internalRows)
  {
    return (internalRows as GridValue[]).Select(BuildMatrixRow).ToArray();
  }

  private static readonly Coords[] AllCoords =
    Enumerable.Range(0, 9).SelectMany(row =>
        Enumerable.Range(0, 9).Select(col =>
          new Coords(row, col))).ToArray();

  private static readonly int[] AllValues = Enumerable.Range(1, 9).ToArray();

  private GridValue[] BuildInternalRowsForUnknownValue(Coords coords)
  {
    return AllValues.Select(value => new GridValue(coords, value, false)).ToArray();
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
