using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class MainPage : ContentPage
{
  private ILogger<MainPage> _logger;

  public MainPage(ILogger<MainPage> logger, MainPageViewModel viewModel)
  {
    _logger = logger;
    _logger.LogInformation("[constructor]");
    InitializeComponent();
    BindingContext = viewModel;
    // var drawable = new SudokuPuzzleDrawable(viewModel);
    var drawable = new PentominoesDrawable(viewModel);
    SudokuPuzzleGraphicsView.Drawable = drawable;
    viewModel.NeedRedraw += (o, e) => OnNeedRedraw();
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
