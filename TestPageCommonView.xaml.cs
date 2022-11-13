using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class TestPageCommonView : ContentPage
{
  private ILogger<TestPageCommonView> _logger;

  public TestPageCommonView(ILogger<TestPageCommonView> logger, TestPageCommonViewModel viewModel)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    InitializeComponent();
    BindingContext = viewModel;
  }
}
