using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace SudokuDlxMaui;

public partial class TestPageCommonViewModel : ObservableObject
{
  private ILogger<TestPageCommonViewModel> _logger;

  public TestPageCommonViewModel(ILogger<TestPageCommonViewModel> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public string FruitName { get => "Pineapple"; }
}
