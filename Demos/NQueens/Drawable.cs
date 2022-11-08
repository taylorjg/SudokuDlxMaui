namespace SudokuDlxMaui.Demos.NQueens;

public class NQueensDrawable : IDrawable
{
  private DemoPageViewModel _demoPageViewModel;

  public NQueensDrawable(DemoPageViewModel demoPageViewModel)
  {
    _demoPageViewModel = demoPageViewModel;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
  }
}
