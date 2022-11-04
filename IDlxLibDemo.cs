namespace SudokuDlxMaui;

public interface IDlxLibDemo
{
  public IDrawable CreateDrawable(DemoPageViewModel demoPageViewModel);
  public object[] BuildInternalRows(object inputData);
  public int[][] BuildMatrix(object[] internalRows);
}
