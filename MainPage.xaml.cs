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
    var v1 = PiecesData.PiecesWithVariations.Select(pwv => $"{pwv.Label},{pwv.Variations.Length}");
    var v2 = string.Join("|", v1);
    _logger.LogInformation(v2);
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
