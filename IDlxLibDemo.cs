namespace SudokuDlxMaui;

public interface IDlxLibDemo
{
  public IDrawable CreateDrawable(DemoPageBaseViewModel demoPageBaseViewModel);
  public object[] BuildInternalRows(object inputData);
  public int[][] BuildMatrix(object[] internalRows);
  public int? GetNumPrimaryColumns(object inputData);
}
