using System.Collections.Concurrent;
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
  private IDemo _demo;
  private object _demoSettings;
  private object _demoOptionalSettings;
  private IDrawable _drawable;
  private object[] _solutionInternalRows;
  private bool _solutionAvailable;
  private IDispatcherProvider _dispatcherProvider;
  private IDispatcherTimer _dispatcherTimer;
  private ConcurrentQueue<object[]> _searchSteps = new ConcurrentQueue<object[]>();

  public DemoPageBaseViewModel(Dependencies dependencies)
  {
    _logger = dependencies.Logger;
    _logger.LogInformation("constructor");
    _dispatcherProvider = dependencies.DispatcherProvider;
    _dispatcherTimer = _dispatcherProvider.GetForCurrentThread().CreateTimer();
    _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(10);
    _dispatcherTimer.Tick += (_, __) =>
    {
      if (_searchSteps.TryDequeue(out object[] searchStep))
      {
        if (searchStep == null)
        {
          _logger.LogInformation("stopping the dispatch timer");
          _dispatcherTimer.Stop();
          return;
        }

        SolutionInternalRows = searchStep;
      }
    };
    SolutionInternalRows = new object[0];
  }

  // https://stackoverflow.com/questions/52982560/asp-net-core-constructor-injection-with-inheritance
  public sealed class Dependencies
  {
    internal ILogger<DemoPageBaseViewModel> Logger { get; private set; }
    internal IDispatcherProvider DispatcherProvider { get; private set; }

    public Dependencies(
      ILogger<DemoPageBaseViewModel> logger,
      IDispatcherProvider dispatcherProvider
    )
    {
      Logger = logger;
      DispatcherProvider = dispatcherProvider;
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

  public object[] SolutionInternalRows
  {
    get => _solutionInternalRows;
    set
    {
      _logger.LogInformation($"SolutionInternalRows setter value: {value}");
      SetProperty(ref _solutionInternalRows, value);
      SolutionAvailable = _solutionInternalRows.Any();
      RaiseNeedRedraw();
    }
  }

  public bool SolutionAvailable
  {
    get => _solutionAvailable;
    set
    {
      _logger.LogInformation($"SolutionAvailable setter value: {value}");
      SetProperty(ref _solutionAvailable, value);
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

    _logger.LogInformation("starting the dispatch timer");
    _dispatcherTimer.Start();

    var dlx = new DlxLib.Dlx();

    dlx.SearchStep += (_, e) => _searchSteps.Enqueue(e.RowIndexes.Select(rowIndex => internalRows[rowIndex]).ToArray());
    dlx.SolutionFound += (_, e) => _searchSteps.Enqueue(null);

    var solutions = maybeNumPrimaryColumns.HasValue
     ? dlx.Solve(matrix, row => row, col => col, maybeNumPrimaryColumns.Value)
     : dlx.Solve(matrix, row => row, col => col);
    var solution = solutions.FirstOrDefault();
    _logger.LogInformation($"_searchSteps.Count: {_searchSteps.Count}");
    if (solution != null)
    {
      var rowIndices = solution.RowIndexes.ToArray();
      _logger.LogInformation($"rowIndices.Length: {rowIndices.Length}");
      _logger.LogInformation($"rowIndices: {string.Join(",", rowIndices.Select(n => n.ToString()))}");
      // SolutionInternalRows = rowIndices.Select(rowIndex => internalRows[rowIndex]).ToArray();
      // foreach (var internalRow in SolutionInternalRows)
      // {
      //   _logger.LogInformation($"solutionInternalRow: {internalRow}");
      // }
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
