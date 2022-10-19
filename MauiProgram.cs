using MetroLog.MicrosoftExtensions;

namespace SudokuDlxMaui;

using DlxLib;

public static class MauiProgram
{
  public static MauiApp CreateMauiApp()
  {
    var matrix = new[,]
    {
        {1, 0, 0, 0},
        {0, 1, 1, 0},
        {1, 0, 0, 1},
        {0, 0, 1, 1},
        {0, 1, 0, 0},
        {0, 0, 1, 0}
    };
    var dlx = new Dlx();
    var firstTwoSolutions = dlx.Solve(matrix).Take(2);

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
