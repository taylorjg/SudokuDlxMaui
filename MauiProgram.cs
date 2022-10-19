using MetroLog.MicrosoftExtensions;

namespace SudokuDlxMaui;

public static class MauiProgram
{
  public static MauiApp CreateMauiApp()
  {
    var builder = MauiApp.CreateBuilder();

    builder
      .UseMauiApp<App>()
      .ConfigureFonts(fonts =>
      {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
      })
      .Logging.AddInMemoryLogger(_ => { })
      .Services.AddTransient<MainPage>();

    return builder.Build();
  }
}
