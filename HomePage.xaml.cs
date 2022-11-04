using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class HomePage : ContentPage
{
  private ILogger<HomePage> _logger;

  public HomePage(ILogger<HomePage> logger)
  {
    _logger = logger;
    _logger.LogInformation("[constructor]");
    InitializeComponent();
  }

  public async void OnSudokuClick(object s, EventArgs e)
  {
    _logger.LogInformation("OnSudokuClick");
    await NavigateToDemo("sudoku");
  }

  public async void OnPentominoesClick(object s, EventArgs e)
  {
    _logger.LogInformation("OnPentominoesClick");
    await NavigateToDemo("pentominoes");
  }

  private Task NavigateToDemo(string demo)
  {
    var parameters = new Dictionary<string, object> { { "demo", demo } };
    return Shell.Current.GoToAsync("MainPage", parameters);
  }
}
