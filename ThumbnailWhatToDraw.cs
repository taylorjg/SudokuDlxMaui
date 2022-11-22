using DlxLib;

namespace SudokuDlxMaui;

public class ThumbnailWhatToDraw : IWhatToDraw
{
  private IDemo _demo;
  private object _demoSettings;

  public ThumbnailWhatToDraw(IDemo demo, object demoSettings = null)
  {
    _demo = demo;
    _demoSettings = demoSettings;
  }

  public object DemoSettings { get => _demoSettings; }

  public object[] SolutionInternalRows
  {
    get
    {
      var internalRows = _demo.BuildInternalRows(DemoSettings);
      var maybeNumPrimaryColumns = _demo.GetNumPrimaryColumns(DemoSettings);
      var matrix = _demo.BuildMatrix(internalRows);
      var dlx = new DlxLib.Dlx();
      var solutions = maybeNumPrimaryColumns.HasValue
        ? dlx.Solve(matrix, row => row, col => col, maybeNumPrimaryColumns.Value)
        : dlx.Solve(matrix, row => row, col => col);
      var solution = solutions.FirstOrDefault();
      return solution.RowIndexes.Select(rowIndex => internalRows[rowIndex]).ToArray();
    }
  }
}
