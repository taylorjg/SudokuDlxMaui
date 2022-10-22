namespace SudokuDlxMaui;

public record Coords(int Row, int Col);

public record GridValue(Coords Coords, int Value, bool IsInitialValue);

public record Puzzle(string Name, GridValue[] GridValues);

public static class SamplePuzzles
{
  public static readonly Puzzle[] Puzzles = new Puzzle[]
  {
    new Puzzle(
      "Daily Telegraph 27744",
      StringsToGridValues( new string[]
      {
        "6 4 9 7 3",
        "  3    6 ",
        "       18",
        "   18   9",
        "     43  ",
        "7   39   ",
        " 7       ",
        " 4    8  ",
        "9 8 6 4 5"
      }))
  };

  private static GridValue[] StringsToGridValues(string[] strings) =>
    strings.SelectMany((s, row) =>
      s.SelectMany((ch, col) =>
      {
        if (int.TryParse(ch.ToString(), out int value) && value >= 1 && value <= 9)
        {
          var coords = new Coords(row, col);
          var gridValue = new GridValue(coords, value, true);
          return new[] { gridValue };
        }
        return new GridValue[0];
      })).ToArray();
}
