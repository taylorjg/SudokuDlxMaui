using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace SudokuDlxMaui;

public record AvailableDemo(string DemoName, string Description = "", string ThumbnailUrl = "");

public partial class HomePageViewModel : ObservableObject
{
  private ILogger<HomePageViewModel> _logger;
  private INavigationService _navigationService;

  public HomePageViewModel(ILogger<HomePageViewModel> logger, INavigationService navigationService)
  {
    _logger = logger;
    _navigationService = navigationService;
    _logger.LogInformation("constructor");
  }

  public AvailableDemo[] AvailableDemos
  {
    get => new[] {
      new AvailableDemo(DemoNames.Sudoku),
      new AvailableDemo(DemoNames.Pentominoes),
      new AvailableDemo(DemoNames.NQueens)
    };
  }

  [RelayCommand]
  private Task NavigateToDemo(string demoName)
  {
    _logger.LogInformation($"NavigateToDemo demoName: {demoName}");
    var parameters = new Dictionary<string, object> { { "demoName", demoName } };
    return _navigationService.GoToAsync("DemoPage", parameters);
  }
}
