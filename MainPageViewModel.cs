using System.Windows.Input;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace SudokuDlxMaui;

public partial class MainPageViewModel : ObservableObject
{
  private ILogger<MainPage> _logger;
  private LogController _logController;
  private Action _onNeedRedraw;
  private Puzzle _selectedPuzzle;
  private GridValue[] _gridValues;

  public MainPageViewModel(ILogger<MainPage> logger, Action OnNeedRedraw)
  {
    _logger = logger;
    _logController = new LogController();
    _onNeedRedraw = OnNeedRedraw;
    SelectedPuzzle = SamplePuzzles.Puzzles[0];
    SolveCommand = new RelayCommand(Solve);
  }

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
      _onNeedRedraw();
    }
  }

  private void Solve()
  {
    _logger.LogInformation("[Solve]");
    GridValues = SudokuSolver.Solve(SelectedPuzzle.GridValues, _logger);
  }
}
