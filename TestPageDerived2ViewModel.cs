using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace SudokuDlxMaui;

public partial class TestPageDerived2ViewModel : ObservableObject
{
  private ILogger<TestPageDerived2ViewModel> _logger;

  public TestPageDerived2ViewModel(ILogger<TestPageDerived2ViewModel> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public View MyView1
  {
    get
    {
      return new Label()
      {
        Text = "This is derived viewmodel MyView1",
        HorizontalOptions = LayoutOptions.Center,
        VerticalOptions = LayoutOptions.Center
      };
    }
  }

  public View MyView2
  {
    get
    {
      return new Label()
      {
        Text = "This is derived viewmodel MyView2",
        HorizontalOptions = LayoutOptions.Center,
        VerticalOptions = LayoutOptions.Center
      };
    }
  }
}
