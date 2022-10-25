using System.Windows.Input;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace SudokuDlxMaui;

public partial class MainPageViewModel : ObservableObject
{
  private ILogger<MainPage> _logger;
  private ISudokuSolver _sudokuSolver;
  private LogController _logController;
  private Puzzle _selectedPuzzle;
  private GridValue[] _gridValues;

  public MainPageViewModel(ILogger<MainPage> logger, ISudokuSolver sudokuSolver)
  {
    _logger = logger;
    _sudokuSolver = sudokuSolver;
    _logController = new LogController();
    SelectedPuzzle = SamplePuzzles.Puzzles[0];
    SolveCommand = new RelayCommand(Solve);
  }

  public event EventHandler NeedRedraw;

  public ICommand SolveCommand { get; }

  public ICommand GoToLogsPageCommand { get => _logController.GoToLogsPageCommand; }

  public Puzzle[] Puzzles { get => SamplePuzzles.Puzzles; }

  public Puzzle SelectedPuzzle
  {
    get => _selectedPuzzle;
    set
    {
      if (value != _selectedPuzzle)
      {
        _logger.LogInformation($"[SelectedPuzzle setter] value: {value}");
        SetProperty(ref _selectedPuzzle, value);
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
      SetProperty(ref _gridValues, value);
      RaiseNeedRedraw();
    }
  }

  private void Solve()
  {
    _logger.LogInformation("[Solve]");
    GridValues = _sudokuSolver.Solve(SelectedPuzzle.GridValues);
  }

  private void RaiseNeedRedraw()
  {
    NeedRedraw?.Invoke(this, new EventArgs());
  }
}
