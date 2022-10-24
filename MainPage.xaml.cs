using MetroLog.Maui;
using Microsoft.Extensions.Logging;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace SudokuDlxMaui;

public partial class MainPage : ContentPage
{
  private ILogger<MainPage> _logger;
  private LogController _logController;
  private Puzzle _selectedPuzzle;
  private GridValue[] _gridValues;

  public MainPage(ILogger<MainPage> logger)
  {
    _logger = logger;
    _logController = new LogController();
    InitializeComponent();
    SudokuPuzzleGraphicsView.Drawable = new SudokuPuzzleDrawable(this);
    SelectedPuzzle = SamplePuzzles.Puzzles[0];
    SolveCommand = new RelayCommand(Solve);
    BindingContext = this;
  }

  public ICommand SolveCommand { get; }

  public ICommand GoToLogsPageCommand { get => _logController.GoToLogsPageCommand; }

  private void OnSizeChanged(object sender, EventArgs e)
  {
    _logger.LogInformation($"[OnSizeChanged]");
    SudokuPuzzleGraphicsView.Invalidate();
  }

  public Puzzle[] Puzzles { get => SamplePuzzles.Puzzles; }

  public Puzzle SelectedPuzzle
  {
    get => _selectedPuzzle;
    set
    {
      if (value != _selectedPuzzle)
      {
        _logger.LogInformation($"[SelectedPuzzle setter] value: {value}");
        _selectedPuzzle = value;
        GridValues = _selectedPuzzle.GridValues;
      }
    }
  }

  public GridValue[] GridValues
  {
    get => _gridValues;
    set
    {
      _logger.LogInformation($"[GridValues setter] value: {value}");
      _gridValues = value;
      SudokuPuzzleGraphicsView.Invalidate();
    }
  }

  private void Solve()
  {
    _logger.LogInformation("[Solve]");
    GridValues = SudokuSolver.Solve(SelectedPuzzle.GridValues, _logger);
  }
}
