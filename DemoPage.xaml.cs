using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class DemoPage : ContentPage
{
  private ILogger<DemoPage> _logger;

  public DemoPage(ILogger<DemoPage> logger, DemoPageViewModel viewModel)
  {
    _logger = logger;
    _logger.LogInformation("[constructor]");
    InitializeComponent();
    BindingContext = viewModel;
    viewModel.NeedRedraw += (o, e) => OnNeedRedraw();
  }

  private void OnSizeChanged(object sender, EventArgs e)
  {
    _logger.LogInformation($"[OnSizeChanged]");
    OnNeedRedraw();
  }

  private void OnNeedRedraw()
  {
    GraphicsView.Invalidate();
  }
}
