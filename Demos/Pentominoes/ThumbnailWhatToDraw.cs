namespace SudokuDlxMaui.Demos.Pentominoes;

public class ThumbnailWhatToDraw : IWhatToDraw
{
  public object DemoSettings { get => null; }

  // { Label = F, Variation = Variation { Orientation = South, Reflected = False, CoordsList = SudokuDlxMaui.Demos.Pentominoes.Coords[] }, Location = Coords { Row = 2, Col = 0 } }
  // { Label = I, Variation = Variation { Orientation = East, Reflected = False, CoordsList = SudokuDlxMaui.Demos.Pentominoes.Coords[] }, Location = Coords { Row = 7, Col = 2 } }
  // { Label = L, Variation = Variation { Orientation = South, Reflected = True, CoordsList = SudokuDlxMaui.Demos.Pentominoes.Coords[] }, Location = Coords { Row = 0, Col = 0 } }
  // { Label = N, Variation = Variation { Orientation = North, Reflected = False, CoordsList = SudokuDlxMaui.Demos.Pentominoes.Coords[] }, Location = Coords { Row = 2, Col = 6 } }
  // { Label = P, Variation = Variation { Orientation = East, Reflected = True, CoordsList = SudokuDlxMaui.Demos.Pentominoes.Coords[] }, Location = Coords { Row = 6, Col = 0 } }
  // { Label = T, Variation = Variation { Orientation = East, Reflected = False, CoordsList = SudokuDlxMaui.Demos.Pentominoes.Coords[] }, Location = Coords { Row = 5, Col = 5 } }
  // { Label = U, Variation = Variation { Orientation = East, Reflected = True, CoordsList = SudokuDlxMaui.Demos.Pentominoes.Coords[] }, Location = Coords { Row = 0, Col = 3 } }
  // { Label = V, Variation = Variation { Orientation = East, Reflected = False, CoordsList = SudokuDlxMaui.Demos.Pentominoes.Coords[] }, Location = Coords { Row = 0, Col = 5 } }
  // { Label = W, Variation = Variation { Orientation = North, Reflected = True, CoordsList = SudokuDlxMaui.Demos.Pentominoes.Coords[] }, Location = Coords { Row = 4, Col = 3 } }
  // { Label = X, Variation = Variation { Orientation = North, Reflected = False, CoordsList = SudokuDlxMaui.Demos.Pentominoes.Coords[] }, Location = Coords { Row = 0, Col = 1 } }
  // { Label = Y, Variation = Variation { Orientation = East, Reflected = False, CoordsList = SudokuDlxMaui.Demos.Pentominoes.Coords[] }, Location = Coords { Row = 4, Col = 0 } }
  // { Label = Z, Variation = Variation { Orientation = North, Reflected = True, CoordsList = SudokuDlxMaui.Demos.Pentominoes.Coords[] }, Location = Coords { Row = 1, Col = 5 } }

  public object[] SolutionInternalRows
  {
    get => new[] {
      MakePentominoesInternalRow("F", Orientation.South, false, 2, 0),
      MakePentominoesInternalRow("I", Orientation.East, false, 7, 2),
      MakePentominoesInternalRow("L", Orientation.South, true, 0, 0),
      MakePentominoesInternalRow("N", Orientation.North, false, 2, 6),
      MakePentominoesInternalRow("P", Orientation.East, true, 6, 0),
      MakePentominoesInternalRow("T", Orientation.East, false, 5, 5),
      MakePentominoesInternalRow("U", Orientation.East, true, 0, 3),
      MakePentominoesInternalRow("V", Orientation.East, false, 0, 5),
      MakePentominoesInternalRow("W", Orientation.North, true, 4, 3),
      MakePentominoesInternalRow("X", Orientation.North, false, 0, 1),
      MakePentominoesInternalRow("Y", Orientation.East, false, 4, 0),
      MakePentominoesInternalRow("Z", Orientation.North, true, 1, 5)
    };
  }

  private PentominoesInternalRow MakePentominoesInternalRow(string label, Orientation orientation, bool reflected, int row, int col)
  {
    var piece = Array.Find(Pieces.ThePieces, p => p.Label == label);
    var pattern = piece.Pattern;

    switch (orientation)
    {
      case Orientation.North:
        break;
      case Orientation.South:
        pattern = pattern.RotateCW();
        pattern = pattern.RotateCW();
        break;
      case Orientation.East:
        pattern = pattern.RotateCW();
        break;
      case Orientation.West:
        pattern = pattern.RotateCW();
        pattern = pattern.RotateCW();
        pattern = pattern.RotateCW();
        break;
    }

    if (reflected)
    {
      pattern = pattern.Reflect();
    }

    var coordsList = PatternCoordsList(pattern).ToArray();
    var variation = new Variation(orientation, reflected, coordsList);
    var location = new Coords(row, col);

    return new PentominoesInternalRow(label, variation, location);
  }

  private static IEnumerable<Coords> PatternCoordsList(string[] pattern)
  {
    var rowCount = pattern.Length;
    var colCount = pattern[0].Length;
    foreach (var row in Enumerable.Range(0, rowCount))
    {
      foreach (var col in Enumerable.Range(0, colCount))
      {
        if (pattern[row][col] == 'X')
        {
          yield return new Coords(row, col);
        }
      }
    }
  }
}
