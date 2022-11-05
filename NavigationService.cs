namespace SudokuDlxMaui;

public class NavigationService : INavigationService
{
  public Task GoToAsync(string route, Dictionary<string, object> parameters)
  {
    return Shell.Current.GoToAsync(route, parameters);
  }
}
