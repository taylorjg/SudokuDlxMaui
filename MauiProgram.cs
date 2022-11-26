using MetroLog.MicrosoftExtensions;
using CommunityToolkit.Maui;
using SudokuDlxMaui.Demos.Sudoku;
using SudokuDlxMaui.Demos.Pentominoes;
using SudokuDlxMaui.Demos.NQueens;
using SudokuDlxMaui.Demos.DraughtboardPuzzle;

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

    builder.Logging.AddStreamingFileLogger(options =>
      {
        options.RetainDays = 2;
        options.FolderPath = Path.Combine(FileSystem.CacheDirectory, "MetroLogs");
      });

    builder.Services.AddSingleton<INavigationService, NavigationService>();

    builder.Services.AddTransient<HomePageView>();
    builder.Services.AddTransient<HomePageViewModel>();

    builder.Services.AddTransient<SudokuDemoPageView>();
    builder.Services.AddTransient<SudokuDemoPageViewModel>();

    builder.Services.AddTransient<PentominoesDemoPageView>();
    builder.Services.AddTransient<PentominoesDemoPageViewModel>();

    builder.Services.AddTransient<NQueensDemoPageView>();
    builder.Services.AddTransient<NQueensDemoPageViewModel>();

    builder.Services.AddTransient<DraughtboardPuzzleDemoPageView>();
    builder.Services.AddTransient<DraughtboardPuzzleDemoPageViewModel>();

    builder.Services.AddTransient<SudokuThumbnailDrawable>();
    builder.Services.AddTransient<PentominoesThumbnailDrawable>();
    builder.Services.AddTransient<NQueensThumbnailDrawable>();
    builder.Services.AddTransient<DraughtboardPuzzleThumbnailDrawable>();

    builder.Services.AddTransient<SudokuDemo>();
    builder.Services.AddTransient<PentominoesDemo>();
    builder.Services.AddTransient<NQueensDemo>();
    builder.Services.AddTransient<DraughtboardPuzzleDemo>();

    Routing.RegisterRoute("SudokuDemoPage", typeof(SudokuDemoPageView));
    Routing.RegisterRoute("PentominoesDemoPage", typeof(PentominoesDemoPageView));
    Routing.RegisterRoute("NQueensDemoPage", typeof(NQueensDemoPageView));
    Routing.RegisterRoute("DraughtboardPuzzleDemoPage", typeof(DraughtboardPuzzleDemoPageView));

    return builder.Build();
  }
}
