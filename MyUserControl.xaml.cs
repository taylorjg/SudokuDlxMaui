using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class MyUserControl : ContentView
{
  private ILogger<MyUserControl> _logger;

  public MyUserControl(ILogger<MyUserControl> logger, MyUserControlViewModel viewModel)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    InitializeComponent();
    BindingContext = viewModel;
  }
}
