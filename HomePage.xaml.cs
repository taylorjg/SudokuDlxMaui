using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class HomePage : ContentPage
{
  private ILogger<HomePage> _logger;

  public HomePage(ILogger<HomePage> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    InitializeComponent();
  }

  public async void OnSudokuClick(object s, EventArgs e)
  {
    _logger.LogInformation("OnSudokuClick");
    await NavigateToDemo(DemoNames.Sudoku);
  }

  public async void OnPentominoesClick(object s, EventArgs e)
  {
    _logger.LogInformation("OnPentominoesClick");
    await NavigateToDemo(DemoNames.Pentominoes);
  }

  private Task NavigateToDemo(string demoName)
  {
    var parameters = new Dictionary<string, object> { { "demoName", demoName } };
    return Shell.Current.GoToAsync("DemoPage", parameters);
  }
}
