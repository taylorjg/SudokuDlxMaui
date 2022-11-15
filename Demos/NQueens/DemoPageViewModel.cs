using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace SudokuDlxMaui.Demos.NQueens;

public partial class NQueensDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<NQueensDemoPageViewModel> _logger;
  private int _selectedSize;

  public NQueensDemoPageViewModel(
    ILogger<NQueensDemoPageViewModel> logger,
    NQueensDemo demo,
    ILogger<DemoPageBaseViewModel> loggerBase
  )
    : base(loggerBase)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
    SelectedSize = 8;
  }

  public int[] Sizes { get => new[] { 4, 5, 6, 7, 8 }; }

  public int SelectedSize
  {
    get => _selectedSize;
    set
    {
      if (value != _selectedSize)
      {
        _logger.LogInformation($"SelectedSize setter value: {value}");
        SetProperty(ref _selectedSize, value);
        DemoSettings = _selectedSize;
        SolutionInternalRows = new object[0];
      }
    }
  }
}
