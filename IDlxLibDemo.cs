namespace SudokuDlxMaui;

public interface IDlxLibDemo
{
  public IDrawable CreateDrawable(DemoPageBaseViewModel demoPageBaseViewModel);
  public object[] BuildInternalRows(object demoSettings);
  public int[][] BuildMatrix(object[] internalRows);
  public int? GetNumPrimaryColumns(object demoSettings);
}
