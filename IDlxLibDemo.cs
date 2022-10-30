namespace SudokuDlxMaui;

public interface IDlxLibDemo
{
  // public IDrawable GetDrawable();
  public object[] BuildInternalRows(object inputData);
  public int[][] BuildMatrix(object[] internalRows);
}
