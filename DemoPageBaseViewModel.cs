using System.Windows.Input;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;
using DlxLib;

namespace SudokuDlxMaui;

public partial class DemoPageBaseViewModel : ObservableObject, IWhatToDraw
{
  private ILogger<DemoPageBaseViewModel> _logger;
  private LogController _logController;
  private IDemo _demo;
  private object _demoSettings;
  private object _demoOptionalSettings;
  private IDrawable _drawable;
  private object[] _solutionInternalRows;

  public DemoPageBaseViewModel(ILogger<DemoPageBaseViewModel> logger)
  {
    _logger = logger;
    _logController = new LogController();
    _logger.LogInformation("constructor");
    SolutionInternalRows = new object[0];
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

  public IDemo Demo
  {
    get => _demo;
    set
    {
      _logger.LogInformation($"Demo setter value: {value}");
      SetProperty(ref _demo, value);
      Drawable = _demo.CreateDrawable(this);
    }
  }
  public object DemoSettings
  {
    get => _demoSettings;
    set
    {
      _logger.LogInformation($"DemoSettings setter value: {value}");
      SetProperty(ref _demoSettings, value);
      RaiseNeedRedraw();
      SolutionInternalRows = new object[0];
    }
  }

  public object DemoOptionalSettings
  {
    get => _demoOptionalSettings;
    set
    {
      _logger.LogInformation($"DemoOptionalSettings setter value: {value}");
      SetProperty(ref _demoOptionalSettings, value);
      RaiseNeedRedraw();
    }
  }

  public ICommand GoToLogsPageCommand { get => _logController.GoToLogsPageCommand; }

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
    _logger.LogInformation($"Solve DemoSettings: {DemoSettings}");
    _logger.LogInformation($"Solve DemoOptionalSettings: {DemoOptionalSettings}");

    var internalRows = _demo.BuildInternalRows(DemoSettings);
    var maybeNumPrimaryColumns = _demo.GetNumPrimaryColumns(DemoSettings);
    var matrix = _demo.BuildMatrix(internalRows);

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
      foreach (var internalRow in SolutionInternalRows) {
        _logger.LogInformation($"solutionInternalRow: {internalRow}");
      }
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
