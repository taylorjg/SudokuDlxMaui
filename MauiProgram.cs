using MetroLog.MicrosoftExtensions;
using CommunityToolkit.Maui;
using SudokuDlxMaui.Demos.Sudoku;
using SudokuDlxMaui.Demos.Pentominoes;
using SudokuDlxMaui.Demos.NQueens;

namespace SudokuDlxMaui;

public delegate IDlxLibDemo DlxLibDemoFactory(string demoName);

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

    builder.Services.AddTransient<DemoPageView>();
    builder.Services.AddTransient<DemoPageViewModel>();

    builder.Services.AddTransient<TestPageView>();
    builder.Services.AddTransient<TestPageViewModel>();

    builder.Services.AddTransient<TestPageCommonView>();
    builder.Services.AddTransient<TestPageCommonViewModel>();

    builder.Services.AddTransient<TestPageDerived1View>();
    builder.Services.AddTransient<TestPageDerived2View>();

    builder.Services.AddTransient<MyUserControl>();
    builder.Services.AddTransient<MyUserControlViewModel>();

    builder.Services.AddTransient<SudokuDlxLibDemo>();
    builder.Services.AddTransient<PentominoesDlxLibDemo>();
    builder.Services.AddTransient<NQueensDlxLibDemo>();

    builder.Services.AddTransient<DlxLibDemoFactory>(serviceProvider => demoName =>
    {
      return demoName switch
      {
        DemoNames.Sudoku => serviceProvider.GetRequiredService<SudokuDlxLibDemo>(),
        DemoNames.Pentominoes => serviceProvider.GetRequiredService<PentominoesDlxLibDemo>(),
        DemoNames.NQueens => serviceProvider.GetRequiredService<NQueensDlxLibDemo>(),
        _ => throw new Exception($"Unknown demo name: {demoName}")
      };
    });

    Routing.RegisterRoute("DemoPage", typeof(DemoPageView));
    // Routing.RegisterRoute("TestPage", typeof(TestPageView));
    // Routing.RegisterRoute("TestPage", typeof(TestPageCommonView));
    // Routing.RegisterRoute("TestPage", typeof(TestPageDerived1View));
    Routing.RegisterRoute("TestPage", typeof(TestPageDerived2View));

    return builder.Build();
  }
}
