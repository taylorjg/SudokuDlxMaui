using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace SudokuDlxMaui;

public partial class TestPageViewModel : ObservableObject
{
  private ILogger<TestPageViewModel> _logger;
  private IServiceProvider _serviceProvider;
  private View _perDemoAppRegion;

  public TestPageViewModel(ILogger<TestPageViewModel> logger, IServiceProvider serviceProvider)
  {
    _logger = logger;
    _serviceProvider = serviceProvider;
    _logger.LogInformation("constructor");
    PerDemoAppRegion = _serviceProvider.GetRequiredService<MyUserControl>();
  }

  public View PerDemoAppRegion
  {
    get => _perDemoAppRegion;
    set
    {
      _logger.LogInformation($"PerDemoAppRegion setter value: {value}");
      SetProperty(ref _perDemoAppRegion, value);
    }
  }
}
