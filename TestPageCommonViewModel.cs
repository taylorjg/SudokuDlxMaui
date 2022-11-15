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

  // public View MyView1
  // {
  //   get {
  //     return new Label() {
  //       Text = "This is MyView1",
  //       HorizontalOptions = LayoutOptions.Center,
  //       VerticalOptions = LayoutOptions.Center
  //     };
  //   }
  // }
  // public View MyView2
  // {
  //   get {
  //     return new Label() {
  //       Text = "This is MyView2",
  //       HorizontalOptions = LayoutOptions.Center,
  //       VerticalOptions = LayoutOptions.Center
  //     };
  //   }
  // }
}
