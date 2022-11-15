using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;
using System.Windows.Input;

namespace SudokuDlxMaui;

public record AvailableDemo(
  string DemoName,
  string Route,
  string Description = "",
  string ThumbnailUrl = ""
);

public partial class HomePageViewModel : ObservableObject
{
  private ILogger<HomePageViewModel> _logger;
  private LogController _logController;
  private INavigationService _navigationService;

  public HomePageViewModel(ILogger<HomePageViewModel> logger, INavigationService navigationService)
  {
    _logger = logger;
    _logController = new LogController();
    _navigationService = navigationService;
    _logger.LogInformation("constructor");
  }

  public ICommand GoToLogsPageCommand { get => _logController.GoToLogsPageCommand; }

  public AvailableDemo[] AvailableDemos
  {
    get => new[] {
      new AvailableDemo(DemoNames.Sudoku, "SudokuDemoPage"),
      new AvailableDemo(DemoNames.Pentominoes, "PentominoesDemoPage"),
      new AvailableDemo(DemoNames.NQueens, "NQueensDemoPage"),
    };
  }

  [RelayCommand]
  private Task NavigateToDemo(string route)
  {
    _logger.LogInformation($"NavigateToDemo route: {route}");
    return _navigationService.GoToAsync(route);
  }
}
