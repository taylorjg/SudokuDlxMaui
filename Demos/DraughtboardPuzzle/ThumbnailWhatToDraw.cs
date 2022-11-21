using DlxLib;

namespace SudokuDlxMaui.Demos.DraughtboardPuzzle;

public class ThumbnailWhatToDraw : IWhatToDraw
{
  private IDemo _demo;

  public ThumbnailWhatToDraw(IDemo demo)
  {
    _demo = demo;
  }

  public object DemoSettings { get => null; }

  public object[] SolutionInternalRows
  {
    get
    {
      var internalRows = _demo.BuildInternalRows(DemoSettings);
      var matrix = _demo.BuildMatrix(internalRows);
      var dlx = new DlxLib.Dlx();
      var solutions = dlx.Solve(matrix, row => row, col => col);
      var solution = solutions.FirstOrDefault();
      return solution.RowIndexes.Select(rowIndex => internalRows[rowIndex]).ToArray();
    }
  }
}
