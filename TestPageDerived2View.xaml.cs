using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class TestPageDerived2View : TestPageCommonView
{
  private ILogger<TestPageDerived2View> _logger;

  public TestPageDerived2View(
    ILogger<TestPageDerived2View> logger,
    ILogger<TestPageCommonView> loggerBase,
    TestPageCommonViewModel viewModelBase
    )
    : base(loggerBase, viewModelBase)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    InitializeComponent();
  }
}
