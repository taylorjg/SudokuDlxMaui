using System.Windows.Input;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;
using DlxLib;

namespace SudokuDlxMaui;

public partial class MainPageViewModel : ObservableObject
{
  private IDlxLibDemo _dlxLibDemo;
  private ILogger<MainPageViewModel> _logger;
  private LogController _logController;
  private Puzzle _selectedPuzzle;
  private object[] _solutionInternalRows;
  private IDrawable _drawable;

  public MainPageViewModel(ILogger<MainPageViewModel> logger)
  {
    _dlxLibDemo = new DlxLibDemoSudoku();
    // _dlxLibDemo = new DlxLibDemoPentominoes();

    _drawable = _dlxLibDemo.CreateDrawable(this);
    _logger = logger;
    _logController = new LogController();
    _logger.LogInformation("[constructor]");
    SelectedPuzzle = SamplePuzzles.Puzzles[0];
    SolveCommand = new RelayCommand(Solve);
  }

  public event EventHandler NeedRedraw;

  public IDrawable Drawable { get => _drawable; }

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
        SolutionInternalRows = _selectedPuzzle.GridValues;
      }
    }
  }

  public object[] SolutionInternalRows
  {
    get => _solutionInternalRows;
    set
    {
      _logger.LogInformation($"[SolutionInternalRows setter] value: {value}");
      SetProperty(ref _solutionInternalRows, value);
      RaiseNeedRedraw();
    }
  }

  private void Solve()
  {
    _logger.LogInformation("[Solve]");
    var internalRows = _dlxLibDemo.BuildInternalRows(SelectedPuzzle.GridValues);
    var matrix = _dlxLibDemo.BuildMatrix(internalRows);
    var dlx = new DlxLib.Dlx();
    var solution = dlx.Solve(matrix, row => row, col => col).FirstOrDefault();
    if (solution != null)
    {
      var rowIndices = solution.RowIndexes.ToArray();
      _logger.LogInformation($"internalRows.Length: {internalRows.Length}");
      SolutionInternalRows = rowIndices.Select(rowIndex => internalRows[rowIndex]).ToArray();
    }
    else
    {
      _logger.LogInformation("[Solve] no solution found!");
    }
  }

  private void RaiseNeedRedraw()
  {
    var handler = NeedRedraw;
    handler?.Invoke(this, EventArgs.Empty);
  }
}
