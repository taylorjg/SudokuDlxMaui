using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui;

public partial class TestPageDerived1View : TestPageCommonView
{
  private ILogger<TestPageDerived1View> _logger;

  public TestPageDerived1View(
    ILogger<TestPageDerived1View> logger,
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
