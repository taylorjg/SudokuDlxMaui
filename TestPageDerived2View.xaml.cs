using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class TestPageDerived2View : TestPageCommonView
{
  private ILogger<TestPageDerived2View> _logger;

  public TestPageDerived2View(
    ILogger<TestPageDerived2View> logger,
    TestPageDerived2ViewModel viewModel,
    ILogger<TestPageCommonView> loggerBase,
    TestPageCommonViewModel viewModelBase
    )
    : base(loggerBase, viewModelBase)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    InitializeComponent();
    BindingContext = viewModel;
  }

  // public View MyView1
  // {
  //   get
  //   {
  //     return new Label()
  //     {
  //       Text = "This is derived MyView1",
  //       HorizontalOptions = LayoutOptions.Center,
  //       VerticalOptions = LayoutOptions.Center
  //     };
  //   }
  // }

  // public View MyView2
  // {
  //   get
  //   {
  //     return new Label()
  //     {
  //       Text = "This is derived MyView2",
  //       HorizontalOptions = LayoutOptions.Center,
  //       VerticalOptions = LayoutOptions.Center
  //     };
  //   }
  // }
}
