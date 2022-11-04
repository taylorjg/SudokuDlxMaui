using MetroLog.MicrosoftExtensions;
using CommunityToolkit.Maui;

namespace SudokuDlxMaui;

public delegate IDlxLibDemo DlxLibDemoFactory(DemoName demoName);

public static class MauiProgram
{

  public static MauiApp CreateMauiApp()
  {
    var builder = MauiApp.CreateBuilder();

    builder
      .UseMauiApp<App>()
      .UseMauiCommunityToolkit()
      .ConfigureFonts(fonts =>
      {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
      });

    builder.Logging.AddInMemoryLogger(_ => { });

    builder.Services.AddTransient<HomePage>();

    builder.Services.AddTransient<DemoPage>();
    builder.Services.AddTransient<DemoPageViewModel>();

    builder.Services.AddTransient<DlxLibDemoSudoku>();
    builder.Services.AddTransient<DlxLibDemoPentominoes>();

    builder.Services.AddTransient<DlxLibDemoFactory>(serviceProvider => demoName =>
    {
      return demoName switch
      {
        DemoName.Sudoku => serviceProvider.GetRequiredService<DlxLibDemoSudoku>(),
        DemoName.Pentominoes => serviceProvider.GetRequiredService<DlxLibDemoPentominoes>(),
        _ => throw new Exception($"Unknown demo name: {demoName}")
      };
    });

    Routing.RegisterRoute("DemoPage", typeof(DemoPage));

    return builder.Build();
  }
}
