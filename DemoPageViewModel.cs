using System.Windows.Input;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;
using DlxLib;

namespace SudokuDlxMaui;

[QueryProperty(nameof(DemoName), "demoName")]
public partial class DemoPageViewModel : ObservableObject
{
  private ILogger<DemoPageViewModel> _logger;
  private IServiceProvider _serviceProvider;
  private LogController _logController;
  private IDlxLibDemo _dlxLibDemo;
  private IDrawable _drawable;
  private SudokuDlxMaui.Demos.Sudoku.Puzzle _selectedPuzzle;
  private object[] _solutionInternalRows;
  private string _demoName;

  public DemoPageViewModel(ILogger<DemoPageViewModel> logger, IServiceProvider serviceProvider)
  {
    _logger = logger;
    _serviceProvider = serviceProvider;
    _logController = new LogController();
    _logger.LogInformation("constructor");
    SelectedPuzzle = SudokuDlxMaui.Demos.Sudoku.SamplePuzzles.Puzzles[0];
  }


  public string DemoName
  {
    get => _demoName;
    set
    {
      _demoName = value;
      _logger.LogInformation($"DemoName setter {_demoName}");
      var dlxLibDemoFactory = _serviceProvider.GetRequiredService<DlxLibDemoFactory>();
      _dlxLibDemo = dlxLibDemoFactory(_demoName);
      Drawable = _dlxLibDemo.CreateDrawable(this);
    }
  }

  public event EventHandler NeedRedraw;

  public IDrawable Drawable
  {
    get => _drawable;
    set
    {
      _logger.LogInformation($"Drawable setter value: {value}");
      SetProperty(ref _drawable, value);
    }
  }

  public ICommand GoToLogsPageCommand { get => _logController.GoToLogsPageCommand; }

  public SudokuDlxMaui.Demos.Sudoku.Puzzle[] Puzzles { get => SudokuDlxMaui.Demos.Sudoku.SamplePuzzles.Puzzles; }

  public SudokuDlxMaui.Demos.Sudoku.Puzzle SelectedPuzzle
  {
    get => _selectedPuzzle;
    set
    {
      if (value != _selectedPuzzle)
      {
        _logger.LogInformation($"SelectedPuzzle setter value: {value}");
        SetProperty(ref _selectedPuzzle, value);
        SolutionInternalRows = _selectedPuzzle.InternalRows;
      }
    }
  }

  public object[] SolutionInternalRows
  {
    get => _solutionInternalRows;
    set
    {
      _logger.LogInformation($"SolutionInternalRows setter value: {value}");
      SetProperty(ref _solutionInternalRows, value);
      RaiseNeedRedraw();
    }
  }

  [RelayCommand]
  private void Solve()
  {
    _logger.LogInformation("Solve");
    var internalRows = _dlxLibDemo.BuildInternalRows(SelectedPuzzle.InternalRows);
    var maybeNumPrimaryColumns = _dlxLibDemo.GetNumPrimaryColumns(SelectedPuzzle.InternalRows);
    var matrix = _dlxLibDemo.BuildMatrix(internalRows);

    _logger.LogInformation($"internalRows.Length: {internalRows.Length}");
    _logger.LogInformation($"maybeNumPrimaryColumns: {(maybeNumPrimaryColumns.HasValue ? maybeNumPrimaryColumns.Value : "null")}");

    var dlx = new DlxLib.Dlx();
    var solutions = maybeNumPrimaryColumns.HasValue
      ? dlx.Solve(matrix, row => row, col => col, maybeNumPrimaryColumns.Value)
      : dlx.Solve(matrix, row => row, col => col);
    var solution = solutions.FirstOrDefault();
    if (solution != null)
    {
      var rowIndices = solution.RowIndexes.ToArray();
      _logger.LogInformation($"rowIndices.Length: {rowIndices.Length}");
      _logger.LogInformation($"rowIndices: {string.Join(",", rowIndices.Select(n => n.ToString()))}");
      SolutionInternalRows = rowIndices.Select(rowIndex => internalRows[rowIndex]).ToArray();
    }
    else
    {
      _logger.LogInformation("No solution found!");
    }
  }

  private void RaiseNeedRedraw()
  {
    var handler = NeedRedraw;
    handler?.Invoke(this, EventArgs.Empty);
  }
}
