namespace SudokuDlxMaui;

public class DemoButton : Button
{
  public static readonly BindableProperty DemoNameProperty =
    BindableProperty.Create(
      nameof(DemoName),
      typeof(string),
      typeof(DemoButton),
      defaultValue: "",
      propertyChanged: OnDemoNameChanged);

  public string DemoName
  {
    get => (string)GetValue(DemoNameProperty);
    set => SetValue(DemoNameProperty, value);
  }

  private static void OnDemoNameChanged(BindableObject bindable, object oldValue, object newValue)
  {
    if (bindable is DemoButton demoButton)
    {
      demoButton.Text = (string)newValue;
      demoButton.CommandParameter = newValue;
    }
  }
}
