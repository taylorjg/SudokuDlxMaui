using MetroLog.MicrosoftExtensions;
using CommunityToolkit.Maui;
using SudokuDlxMaui.Demos.Sudoku;
using SudokuDlxMaui.Demos.Pentominoes;
using SudokuDlxMaui.Demos.NQueens;

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

    builder.Services.AddSingleton<INavigationService, NavigationService>();

    builder.Services.AddTransient<HomePageView>();
    builder.Services.AddTransient<HomePageViewModel>();

    builder.Services.AddTransient<SudokuDemoPageView>();
    builder.Services.AddTransient<SudokuDemoPageViewModel>();

    builder.Services.AddTransient<PentominoesDemoPageView>();
    builder.Services.AddTransient<PentominoesDemoPageViewModel>();

    builder.Services.AddTransient<NQueensDemoPageView>();
    builder.Services.AddTransient<NQueensDemoPageViewModel>();

    builder.Services.AddTransient<SudokuDemo>();
    builder.Services.AddTransient<PentominoesDemo>();
    builder.Services.AddTransient<NQueensDemo>();

    Routing.RegisterRoute("SudokuDemoPage", typeof(SudokuDemoPageView));
    Routing.RegisterRoute("PentominoesDemoPage", typeof(PentominoesDemoPageView));
    Routing.RegisterRoute("NQueensDemoPage", typeof(NQueensDemoPageView));

    return builder.Build();
  }
}
