using MetroLog.Maui;
using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class MainPage : ContentPage
{
  private ILogger<MainPage> _logger;

  public MainPage(ILogger<MainPage> logger)
  {
    InitializeComponent();
    BindingContext = new LogController();
    _logger = logger;
  }
}
