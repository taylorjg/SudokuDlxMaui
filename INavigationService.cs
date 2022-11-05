namespace SudokuDlxMaui;

public interface INavigationService
{
  public Task GoToAsync(string route, Dictionary<string, object> parameters);
}
