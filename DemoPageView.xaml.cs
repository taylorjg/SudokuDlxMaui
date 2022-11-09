using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class DemoPageView : ContentPage
{
  private ILogger<DemoPageView> _logger;

  public DemoPageView(ILogger<DemoPageView> logger, DemoPageViewModel viewModel)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    InitializeComponent();
    BindingContext = viewModel;
    viewModel.NeedRedraw += (o, e) => OnNeedRedraw();
  }

  private void OnSizeChanged(object sender, EventArgs e)
  {
    _logger.LogInformation($"OnSizeChanged");
    OnNeedRedraw();
  }

  private void OnNeedRedraw()
  {
    GraphicsView.Invalidate();
  }
}
