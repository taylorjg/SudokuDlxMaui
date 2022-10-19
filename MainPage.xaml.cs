using MetroLog.Maui;
using Microsoft.Extensions.Logging;
using DlxLib;

namespace SudokuDlxMaui;

public partial class MainPage : ContentPage
{
  int count = 0;
  ILogger<MainPage> _logger;

  public MainPage(ILogger<MainPage> logger)
  {
    InitializeComponent();
    BindingContext = new LogController();
    _logger = logger;
  }

  private void OnCounterClicked(object sender, EventArgs e)
  {
    count++;

    if (count == 1)
      CounterBtn.Text = $"Clicked {count} time";
    else
      CounterBtn.Text = $"Clicked {count} times";

    _logger.LogInformation($"Button clicked. Count: {count}");

    SemanticScreenReader.Announce(CounterBtn.Text);

    var matrix = new[,]
    {
        {1, 0, 0, 0},
        {0, 1, 1, 0},
        {1, 0, 0, 1},
        {0, 0, 1, 1},
        {0, 1, 0, 0},
        {0, 0, 1, 0}
    };
    var dlx = new Dlx();
    var firstTwoSolutions = dlx.Solve(matrix).Take(2).ToList();
    _logger.LogInformation($"Number of solutions found: {firstTwoSolutions.Count}");
    _logger.LogInformation(String.Join(", ", firstTwoSolutions[0].RowIndexes.Select(n => n.ToString())));
    _logger.LogInformation(String.Join(", ", firstTwoSolutions[1].RowIndexes.Select(n => n.ToString())));
  }
}
