using MetroLog.MicrosoftExtensions;
using CommunityToolkit.Maui;

namespace SudokuDlxMaui;

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

    Routing.RegisterRoute("DemoPage", typeof(DemoPage));

    return builder.Build();
  }
}
