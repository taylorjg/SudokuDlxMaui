using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class MainPage : ContentPage
{
  private ILogger<MainPage> _logger;

  public MainPage(ILogger<MainPage> logger)
  {
    _logger = logger;
    InitializeComponent();
    var viewModel = new MainPageViewModel(logger);
    viewModel.NeedRedraw += (o, e) => OnNeedRedraw();
    SudokuPuzzleGraphicsView.Drawable = new SudokuPuzzleDrawable(viewModel);
    BindingContext = viewModel;
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
