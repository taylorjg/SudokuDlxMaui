using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace SudokuDlxMaui.Demos.Pentominoes;

public partial class PentominoesDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<PentominoesDemoPageViewModel> _logger;

  public PentominoesDemoPageViewModel(
    ILogger<PentominoesDemoPageViewModel> logger,
    PentominoesDlxLibDemo demo,
    ILogger<DemoPageBaseViewModel> loggerBase
  )
    : base(loggerBase)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
  }
}
