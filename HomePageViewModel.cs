using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace SudokuDlxMaui;

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

  [RelayCommand]
  private async void NavigateToSudoku()
  {
    _logger.LogInformation("NavigateToSudoku");
    await NavigateToDemo(DemoNames.Sudoku);
  }

  [RelayCommand]
  private async void NavigateToPentominoes()
  {
    _logger.LogInformation("NavigateToPentominoes");
    await NavigateToDemo(DemoNames.Pentominoes);
  }

  private Task NavigateToDemo(string demoName)
  {
    var parameters = new Dictionary<string, object> { { "demoName", demoName } };
    return _navigationService.GoToAsync("DemoPage", parameters);
  }
}
