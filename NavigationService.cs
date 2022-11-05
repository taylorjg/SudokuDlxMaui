namespace SudokuDlxMaui;

public class NavigationService : INavigationService
{
  public Task GoToAsync(string route, IDictionary<string, object> parameters)
  {
    return Shell.Current.GoToAsync(route, parameters);
  }
}
