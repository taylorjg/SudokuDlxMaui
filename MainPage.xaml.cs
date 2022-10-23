using MetroLog.Maui;
using Microsoft.Extensions.Logging;
using System.Windows.Input;

namespace SudokuDlxMaui;

public partial class MainPage : ContentPage
{
  private ILogger<MainPage> _logger;
  private LogController _logController;
  private Puzzle _currentPuzzle;

  public MainPage(ILogger<MainPage> logger)
  {
    _logger = logger;
    _logController = new LogController();
    _currentPuzzle = SamplePuzzles.Puzzles[0];
    InitializeComponent();
    SudokuPuzzleGraphicsView.Drawable = new SudokuPuzzleDrawable(this);
    BindingContext = this;
  }

  public ICommand GoToLogsPageCommand { get => _logController.GoToLogsPageCommand; }

  public void OnSizeChanged(object sender, EventArgs e)
  {
    (sender as GraphicsView).Invalidate();
  }

  public Puzzle CurrentPuzzle { get => _currentPuzzle; }
  public Puzzle[] Puzzles { get => SamplePuzzles.Puzzles; }
  public int SelectedPuzzleIndex { get => 0; }
}
