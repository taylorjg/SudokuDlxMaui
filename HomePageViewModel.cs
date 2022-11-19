using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;
using System.Windows.Input;

namespace SudokuDlxMaui;

public record AvailableDemo(
  string Name,
  string Route,
  IDrawable ThumbnailDrawable
);

public partial class HomePageViewModel : ObservableObject
{
  private ILogger<HomePageViewModel> _logger;
  private LogController _logController;
  private INavigationService _navigationService;
  private IDrawable _thumbnailDrawableSudoku;
  private IDrawable _thumbnailDrawablePentominoes;
  private IDrawable _thumbnailDrawableNQueens;

  public HomePageViewModel(ILogger<HomePageViewModel> logger, INavigationService navigationService)
  {
    _logger = logger;
    _logController = new LogController();
    _navigationService = navigationService;
    _logger.LogInformation("constructor");
    _logger.LogInformation($"FileSystem.CacheDirectory: {FileSystem.CacheDirectory}");
    _thumbnailDrawableSudoku = new SudokuDlxMaui.Demos.NQueens.NQueensDrawable(
      new SudokuDlxMaui.Demos.NQueens.ThumbnailWhatToDraw()
    );
    _thumbnailDrawablePentominoes = new SudokuDlxMaui.Demos.Pentominoes.PentominoesDrawable(
      new SudokuDlxMaui.Demos.Pentominoes.ThumbnailWhatToDraw()
    );
    _thumbnailDrawableNQueens = new SudokuDlxMaui.Demos.NQueens.NQueensDrawable(
      new SudokuDlxMaui.Demos.NQueens.ThumbnailWhatToDraw()
    );
  }

  public ICommand GoToLogsPageCommand { get => _logController.GoToLogsPageCommand; }

  public AvailableDemo[] AvailableDemos
  {
    get => new[] {
      new AvailableDemo(DemoNames.Sudoku, "SudokuDemoPage", _thumbnailDrawableNQueens),
      new AvailableDemo(DemoNames.Pentominoes, "PentominoesDemoPage", _thumbnailDrawablePentominoes),
      new AvailableDemo(DemoNames.NQueens, "NQueensDemoPage", _thumbnailDrawableNQueens),
    };
  }

  [RelayCommand]
  private Task NavigateToDemo(string route)
  {
    _logger.LogInformation($"NavigateToDemo route: {route}");
    return _navigationService.GoToAsync(route);
  }
}
