using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class DemoPageBaseView : ContentPage
{
  private ILogger<DemoPageBaseView> _logger;

  public DemoPageBaseView(ILogger<DemoPageBaseView> logger, DemoPageBaseViewModel viewModel)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    InitializeComponent();
    BindingContext = viewModel;
    viewModel.NeedRedraw += (o, e) => OnNeedRedraw();
  }

  private void GraphicsView_OnSizeChanged(object sender, EventArgs e)
  {
    _logger.LogInformation("GraphicsView_OnSizeChanged");
    var graphicsViewWrapper = (Grid)GetTemplateChild("graphicsViewWrapper");
    var graphicsView = (GraphicsView)GetTemplateChild("graphicsView");
    var gvww = graphicsViewWrapper.Width;
    var gvwh = graphicsViewWrapper.Height;
    var gvsize = Math.Min(gvww, gvwh);
    _logger.LogInformation($"GraphicsView_OnSizeChanged {gvww}x{gvwh}, {gvsize}");
    graphicsView.WidthRequest = gvsize;
    graphicsView.HeightRequest = gvsize;
    OnNeedRedraw();
  }

  private void OnNeedRedraw()
  {
    var graphicsView = (GraphicsView)GetTemplateChild("graphicsView");
    graphicsView.Invalidate();
  }
}
