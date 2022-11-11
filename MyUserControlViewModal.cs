using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace SudokuDlxMaui;

public partial class MyUserControlViewModel : ObservableObject
{
  private ILogger<MyUserControlViewModel> _logger;
  private string _selectedWord;

  public MyUserControlViewModel(ILogger<MyUserControlViewModel> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    SelectedWord = "Word 2";
  }

  public string[] Words
  {
    get => Enumerable.Range(1, 20).Select(n => $"Word {n}").ToArray();
  }

  public string SelectedWord {
    get => _selectedWord;
    set {
        _logger.LogInformation($"SelectedWord setter value: {value}");
        SetProperty(ref _selectedWord, value);
    }
  }
}
