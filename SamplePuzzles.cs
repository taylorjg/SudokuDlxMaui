namespace SudokuDlxMaui;

public record GridValue(Coords Coords, int Value, bool IsInitialValue);

public record Puzzle(string Name, GridValue[] GridValues);

public static class SamplePuzzles
{
  public static readonly Puzzle[] Puzzles = new Puzzle[]
  {
    new Puzzle(
      "Daily Telegraph 27744",
      StringsToGridValues(new string[]
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
      })),
    new Puzzle(
      "Daily Telegraph 27808",
      StringsToGridValues(new string[]
      {
        "   8    7",
        "  6 9 1  ",
        "    14 3 ",
        "  75   18",
        "     89  ",
        "25       ",
        " 6 93    ",
        "  2 6 8  ",
        "4    7   "
      })),
    new Puzzle(
      "Manchester Evening News 6th May 2016 No. 1",
      StringsToGridValues(new string[]
      {
        "8   2 6  ",
        " 92  4  7",
        "4    6 8 ",
        "35  6   1",
        "92 7  45 ",
        "7 62  8  ",
        "  4   29 ",
        " 7 8 2  5",
        "   6  1 4"
      })),
    new Puzzle(
      "Manchester Evening News 6th May 2016 No. 2",
      StringsToGridValues(new string[]
      {
        " 4 13   5",
        "1  25    ",
        "     6   ",
        "2        ",
        "6 8    5 ",
        " 9 6 1  2",
        "  7  8  1",
        "9       3",
        " 13  4 6 "
      })),
    new Puzzle(
      "World's hardest Sudoku",
      StringsToGridValues(new string[]
      {
        "8        ",
        "  36     ",
        " 7  9 2  ",
        " 5   7   ",
        "    457  ",
        "   1   3 ",
        "  1    68",
        "  85   1 ",
        " 9    4  "
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
