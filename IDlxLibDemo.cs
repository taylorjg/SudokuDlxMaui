namespace SudokuDlxMaui;

public interface IDlxLibDemo
{
  public IDrawable CreateDrawable(MainPageViewModel mainPageViewModel);
  public object[] BuildInternalRows(object inputData);
  public int[][] BuildMatrix(object[] internalRows);
}
