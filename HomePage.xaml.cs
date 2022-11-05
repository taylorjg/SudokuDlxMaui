using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class HomePage : ContentPage
{
  private ILogger<HomePage> _logger;

  public HomePage(ILogger<HomePage> logger, HomePageViewModel viewModel)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    InitializeComponent();
    BindingContext = viewModel;
  }
}
