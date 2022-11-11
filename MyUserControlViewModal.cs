using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace SudokuDlxMaui;

public partial class MyUserControlViewModel : ObservableObject
{
  private ILogger<MyUserControlViewModel> _logger;

  public MyUserControlViewModel(ILogger<MyUserControlViewModel> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }
}
