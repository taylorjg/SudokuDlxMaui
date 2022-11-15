using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class DemoPageView : ContentPage
{
  private ILogger<DemoPageView> _logger;

  public static BindableProperty FredProperty = BindableProperty.Create(
    "Fred",
    typeof(SudokuDlxMaui.Demos.Sudoku.Puzzle),
    typeof(DemoPageView),
    propertyChanged: (bindable, oldValue, newValue) => {
      var view = bindable as DemoPageView;
      (view.BindingContext as DemoPageViewModel).Fred = newValue as SudokuDlxMaui.Demos.Sudoku.Puzzle;
    }
  );

  public DemoPageView(ILogger<DemoPageView> logger, DemoPageViewModel viewModel)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    InitializeComponent();
    BindingContext = viewModel;
    viewModel.NeedRedraw += (o, e) => OnNeedRedraw();
  }

  private void GraphicsView_OnSizeChanged(object sender, EventArgs e)
  {
    var gvww = GraphicsViewWrapper.Width;
    var gvwh = GraphicsViewWrapper.Height;
    var gvsize = Math.Min(gvww, gvwh);
    _logger.LogInformation($"GraphicsView_OnSizeChanged {gvww}x{gvwh}, {gvsize}");
    GraphicsView.WidthRequest = gvsize;
    GraphicsView.HeightRequest = gvsize;
    OnNeedRedraw();
  }

  private void OnNeedRedraw()
  {
    GraphicsView.Invalidate();
  }
}
