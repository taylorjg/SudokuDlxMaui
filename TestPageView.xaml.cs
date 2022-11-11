using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class TestPageView : ContentPage
{
  private ILogger<TestPageView> _logger;

  public TestPageView(ILogger<TestPageView> logger, TestPageViewModel viewModel)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    InitializeComponent();
    BindingContext = viewModel;
  }
}
