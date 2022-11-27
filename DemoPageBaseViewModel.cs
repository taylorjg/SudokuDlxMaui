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
  private IDispatcherTimer _dispatcherTimer;
  private bool _animationEnabled;
  private int _animationInterval;
  private ConcurrentQueue<BaseMessage> _messages = new ConcurrentQueue<BaseMessage>();

  public DemoPageBaseViewModel(Dependencies dependencies)
  {
    _logger = dependencies.Logger;
    _logger.LogInformation("constructor");
    var dispatcherProvider = dependencies.DispatcherProvider;
    var dispatcher = dispatcherProvider.GetForCurrentThread();
    _dispatcherTimer = dispatcher.CreateTimer();
    _dispatcherTimer.Tick += (_, __) => OnTick();
    SolutionInternalRows = new object[0];
    AnimationEnabled = false;
    AnimationInterval = 10;
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

  public bool AnimationEnabled
  {
    get => _animationEnabled;
    set
    {
      _logger.LogInformation($"AnimationEnabled setter value: {value}");
      SetProperty(ref _animationEnabled, value);
    }
  }

  public int AnimationInterval
  {
    get => _animationInterval;
    set
    {
      if (value != _animationInterval)
      {
        _logger.LogInformation($"AnimationInterval setter value: {value}");
        SetProperty(ref _animationInterval, value);
        _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(_animationInterval);
      }
    }
  }

  private void OnTick()
  {
    if (_messages.TryDequeue(out BaseMessage message))
    {
      if (message is SearchStepMessage)
      {
        SolutionInternalRows = (message as SearchStepMessage).SolutionInternalRows;
        return;
      }
      if (message is SolutionFoundMessage)
      {
        StopTimer();
        return;
      }
    }
  }

  [RelayCommand(CanExecute = nameof(CanSolve))]
  private void Solve()
  {
    _logger.LogInformation($"Solve DemoSettings: {DemoSettings}");
    _logger.LogInformation($"Solve DemoOptionalSettings: {DemoOptionalSettings}");

    var internalRows = _demo.BuildInternalRows(DemoSettings);
    var maybeNumPrimaryColumns = _demo.GetNumPrimaryColumns(DemoSettings);
    var matrix = _demo.BuildMatrix(internalRows);

    _logger.LogInformation($"internalRows.Length: {internalRows.Length}");
    _logger.LogInformation($"maybeNumPrimaryColumns: {(maybeNumPrimaryColumns.HasValue ? maybeNumPrimaryColumns.Value : "null")}");

    var findSolutionInternalRows = (IEnumerable<int> rowIndices) =>
      rowIndices.Select(rowIndex => internalRows[rowIndex]).ToArray();

    var dlx = new DlxLib.Dlx();

    if (AnimationEnabled)
    {
      StartTimer();
      dlx.SearchStep += (_, e) => _messages.Enqueue(new SearchStepMessage(findSolutionInternalRows(e.RowIndexes)));
      dlx.SolutionFound += (_, e) => _messages.Enqueue(new SolutionFoundMessage(findSolutionInternalRows(e.Solution.RowIndexes)));
    }

    var solutions = maybeNumPrimaryColumns.HasValue
     ? dlx.Solve(matrix, row => row, col => col, maybeNumPrimaryColumns.Value)
     : dlx.Solve(matrix, row => row, col => col);

    var solution = solutions.FirstOrDefault();

    if (solution != null && !AnimationEnabled)
    {
      SolutionInternalRows = findSolutionInternalRows(solution.RowIndexes);
    }

    if (solution == null)
    {
      _logger.LogInformation("No solution found!");
    }
  }

  private bool CanSolve()
  {
    return !_dispatcherTimer.IsRunning;
  }

  private void StartTimer()
  {
    _logger.LogInformation("starting the dispatch timer");
    _dispatcherTimer.Start();
    SolveCommand.NotifyCanExecuteChanged();
  }

  private void StopTimer()
  {
    _logger.LogInformation("stopping the dispatch timer");
    _dispatcherTimer.Stop();
    _messages.Clear();
    SolveCommand.NotifyCanExecuteChanged();
  }

  private void RaiseNeedRedraw()
  {
    var handler = NeedRedraw;
    handler?.Invoke(this, EventArgs.Empty);
  }
}
