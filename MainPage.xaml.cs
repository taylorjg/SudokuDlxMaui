using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class MainPage : ContentPage
{
  private ILogger<MainPage> _logger;

  public MainPage(ILogger<MainPage> logger, MainPageViewModel viewModel, SudokuPuzzleDrawable drawable)
  {
    _logger = logger;
    _logger.LogInformation("[constructor]");
    InitializeComponent();
    BindingContext = viewModel;
    SudokuPuzzleGraphicsView.Drawable = drawable;
    viewModel.NeedRedraw += (o, e) => OnNeedRedraw();

    var pentominoesSolver = new PentominoesSolver();
    var solution = pentominoesSolver.Solve();
    if (solution != null)
    {
      _logger.LogInformation($"solution.Length: {solution.Length}");
      foreach (var internalRow in solution)
      {
        _logger.LogInformation($"{internalRow.Label}, {internalRow.Variation.Orientation}, {internalRow.Variation.Reflected}, {internalRow.Location}");
      }
    }
  }

  private void OnSizeChanged(object sender, EventArgs e)
  {
    _logger.LogInformation($"[OnSizeChanged]");
    OnNeedRedraw();
  }

  private void OnNeedRedraw()
  {
    SudokuPuzzleGraphicsView.Invalidate();
  }
}
