using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace SudokuDlxMaui.Demos.DraughtboardPuzzle;

public partial class DraughtboardPuzzleDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<DraughtboardPuzzleDemoPageViewModel> _logger;

  public DraughtboardPuzzleDemoPageViewModel(
    ILogger<DraughtboardPuzzleDemoPageViewModel> logger,
    DraughtboardPuzzleDemo demo,
    ILogger<DemoPageBaseViewModel> loggerBase
  )
    : base(loggerBase)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
  }
}
