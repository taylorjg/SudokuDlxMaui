namespace SudokuDlxMaui.Demos.Sudoku;

public class ThumbnailDrawable : SudokuDrawable
{
  public ThumbnailDrawable(SudokuDemo demo)
    : base(new ThumbnailWhatToDraw(demo))
  {
  }
}
