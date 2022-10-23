using MetroLog.Maui;
using Microsoft.Extensions.Logging;
using System.Windows.Input;

namespace SudokuDlxMaui;

public partial class MainPage : ContentPage
{
  private ILogger<MainPage> _logger;
  private LogController _logController;
  private Puzzle _selectedPuzzle;

  public MainPage(ILogger<MainPage> logger)
  {
    _logger = logger;
    _logController = new LogController();
    _selectedPuzzle = SamplePuzzles.Puzzles[0];
    InitializeComponent();
    SudokuPuzzleGraphicsView.Drawable = new SudokuPuzzleDrawable(this);
    BindingContext = this;
  }

  public ICommand GoToLogsPageCommand { get => _logController.GoToLogsPageCommand; }

  public void OnSizeChanged(object sender, EventArgs e)
  {
    (sender as GraphicsView).Invalidate();
  }

  public Puzzle[] Puzzles { get => SamplePuzzles.Puzzles; }
  public Puzzle SelectedPuzzle
  {
    get => _selectedPuzzle;
    set
    {
      if (value != _selectedPuzzle)
      {
        _selectedPuzzle = value;
        _logger.LogInformation($"[SelectedPuzzle setter] value: {value}");
        SudokuPuzzleGraphicsView.Invalidate();
      }
    }
  }
}
