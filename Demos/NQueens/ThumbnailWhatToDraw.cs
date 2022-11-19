using DlxLib;

namespace SudokuDlxMaui.Demos.NQueens;

public class ThumbnailWhatToDraw : IWhatToDraw
{
  private IDemo _demo;

  public ThumbnailWhatToDraw(IDemo demo)
  {
    _demo = demo;
  }

  public object DemoSettings { get => 8; }

  public object[] SolutionInternalRows
  {
    get
    {
      var internalRows = _demo.BuildInternalRows(DemoSettings);
      var numPrimaryColumns = _demo.GetNumPrimaryColumns(DemoSettings);
      var matrix = _demo.BuildMatrix(internalRows);
      var dlx = new DlxLib.Dlx();
      var solutions = dlx.Solve(matrix, row => row, col => col, numPrimaryColumns.Value);
      var solution = solutions.FirstOrDefault();
      return solution.RowIndexes.Select(rowIndex => internalRows[rowIndex]).ToArray();
    }
  }
}
