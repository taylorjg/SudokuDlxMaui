namespace SudokuDlxMaui.Demos.NQueens;

public class ThumbnailDrawable : NQueensDrawable
{
  public ThumbnailDrawable(NQueensDemo demo)
    : base(new ThumbnailWhatToDraw(demo, 8))
  {
  }
}
