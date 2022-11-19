using DlxLib;

namespace SudokuDlxMaui.Demos.Sudoku;

public class ThumbnailWhatToDraw : IWhatToDraw
{
  public object DemoSettings { get => SamplePuzzles.Puzzles.First(); }

  public object[] SolutionInternalRows
  {
    get
    {
      var demo = MauiUIApplicationDelegate.Current.Services.GetService<SudokuDemo>();
      var internalRows = demo.BuildInternalRows(DemoSettings);
      var matrix = demo.BuildMatrix(internalRows);
      var dlx = new DlxLib.Dlx();
      var solutions = dlx.Solve(matrix, row => row, col => col);
      var solution = solutions.FirstOrDefault();
      var solutionInternalRows = solution.RowIndexes.Select(rowIndex => internalRows[rowIndex]).ToArray();
      return solutionInternalRows;
    }
  }
}
