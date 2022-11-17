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
  private IDrawable _thumbnailDrawable;

  public HomePageViewModel(ILogger<HomePageViewModel> logger, INavigationService navigationService)
  {
    _logger = logger;
    _logController = new LogController();
    _navigationService = navigationService;
    _logger.LogInformation("constructor");
    _logger.LogInformation($"FileSystem.CacheDirectory: {FileSystem.CacheDirectory}");
    _thumbnailDrawable = new SudokuDlxMaui.Demos.NQueens.NQueensDrawable(
      new SudokuDlxMaui.Demos.NQueens.ThumbnailWhatToDraw()
    );
  }

  public ICommand GoToLogsPageCommand { get => _logController.GoToLogsPageCommand; }

  public AvailableDemo[] AvailableDemos
  {
    get => new[] {
      new AvailableDemo(DemoNames.Sudoku, "SudokuDemoPage", _thumbnailDrawable),
      new AvailableDemo(DemoNames.Pentominoes, "PentominoesDemoPage", _thumbnailDrawable),
      new AvailableDemo(DemoNames.NQueens, "NQueensDemoPage", _thumbnailDrawable),
    };
  }

  [RelayCommand]
  private Task NavigateToDemo(string route)
  {
    _logger.LogInformation($"NavigateToDemo route: {route}");
    return _navigationService.GoToAsync(route);
  }
}
