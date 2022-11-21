namespace SudokuDlxMaui.Demos.DraughtboardPuzzle;

public class ThumbnailDrawable : DraughtboardPuzzleDrawable
{
  public ThumbnailDrawable(DraughtboardPuzzleDemo demo)
    : base(new ThumbnailWhatToDraw(demo))
  {
  }
}
