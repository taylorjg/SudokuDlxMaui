using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;
using System.Windows.Input;

namespace SudokuDlxMaui;

public record AvailableDemo(
  string Name,
  string Route,
  IDrawable ThumbnailDrawable
);

public partial class HomePageViewModel : ObservableObject
{
  private ILogger<HomePageViewModel> _logger;
  private LogController _logController;
  private INavigationService _navigationService;
  private IServiceProvider _serviceProvider;

  public HomePageViewModel(
    ILogger<HomePageViewModel> logger,
    INavigationService navigationService,
    IServiceProvider serviceProvider
  )
  {
    _logger = logger;
    _logController = new LogController();
    _navigationService = navigationService;
    _serviceProvider = serviceProvider;
    _logger.LogInformation("constructor");
    _logger.LogInformation($"FileSystem.CacheDirectory: {FileSystem.CacheDirectory}");
  }

  public ICommand GoToLogsPageCommand { get => _logController.GoToLogsPageCommand; }

  public AvailableDemo[] AvailableDemos
  {
    get
    {
      var thumbnailDrawableSudoku = _serviceProvider.GetService<SudokuDlxMaui.Demos.Sudoku.ThumbnailDrawable>();
      var thumbnailDrawablePentominoes = _serviceProvider.GetService<SudokuDlxMaui.Demos.Pentominoes.ThumbnailDrawable>();
      var thumbnailDrawableNQueens = _serviceProvider.GetService<SudokuDlxMaui.Demos.NQueens.ThumbnailDrawable>();
      var thumbnailDrawableDraughtboardPuzzle = _serviceProvider.GetService<SudokuDlxMaui.Demos.DraughtboardPuzzle.ThumbnailDrawable>();

      return new[] {
        new AvailableDemo(DemoNames.Sudoku, "SudokuDemoPage", thumbnailDrawableSudoku),
        new AvailableDemo(DemoNames.Pentominoes, "PentominoesDemoPage", thumbnailDrawablePentominoes),
        new AvailableDemo(DemoNames.NQueens, "NQueensDemoPage", thumbnailDrawableNQueens),
        new AvailableDemo(DemoNames.DraughtboardPuzzle, "DraughtboardPuzzleDemoPage", thumbnailDrawableDraughtboardPuzzle)
      };
    }
  }

  [RelayCommand]
  private Task NavigateToDemo(string route)
  {
    _logger.LogInformation($"NavigateToDemo route: {route}");
    return _navigationService.GoToAsync(route);
  }
}
