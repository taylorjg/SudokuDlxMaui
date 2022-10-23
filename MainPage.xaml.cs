using MetroLog.Maui;
using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class MainPage : ContentPage
{
  private ILogger<MainPage> _logger;
  private Puzzle _currentPuzzle;

  public MainPage(ILogger<MainPage> logger)
  {
    _currentPuzzle = SamplePuzzles.Puzzles[3];
    InitializeComponent();
    SudokuPuzzleGraphicsView.Drawable = new SudokuPuzzleDrawable(this);
    BindingContext = new LogController();
    _logger = logger;
  }

  public void OnSizeChanged(object sender, EventArgs e)
  {
    (sender as GraphicsView).Invalidate();
  }

  public Puzzle CurrentPuzzle { get => _currentPuzzle; }
}
