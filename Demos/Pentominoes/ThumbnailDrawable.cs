namespace SudokuDlxMaui.Demos.Pentominoes;

public class ThumbnailDrawable : PentominoesDrawable
{
  public ThumbnailDrawable(PentominoesDemo demo)
    : base(new ThumbnailWhatToDraw(demo))
  {
  }
}
