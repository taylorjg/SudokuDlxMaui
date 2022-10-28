namespace SudokuDlxMaui;

public enum Orientation
{
  North,
  South,
  East,
  West
}

public record Variation(Orientation Orientation, bool Reflected, Coords[] CoordsList);
public record PieceWithVariations(string Label, Variation[] Variations);

public static class PiecesData
{
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

  private record VariationCandidate(Orientation Orientation, bool Reflected, string[] Pattern);

  private static PieceWithVariations FindUniqueVariations(PieceWithPattern pieceWithPattern)
  {
    var (label, pattern) = pieceWithPattern;

    var reflect = (VariationCandidate vc) =>
      vc with { Reflected = true, Pattern = vc.Pattern.Reflect() };

    var north = new VariationCandidate(Orientation.North, false, pattern);
    var northReflected = reflect(north);

    var east = new VariationCandidate(Orientation.East, false, north.Pattern.RotateCW());
    var eastReflected = reflect(east);

    var south = new VariationCandidate(Orientation.South, false, east.Pattern.RotateCW());
    var southReflected = reflect(south);

    var west = new VariationCandidate(Orientation.West, false, south.Pattern.RotateCW());
    var westReflected = reflect(west);

    var allVariationCandidates = new[] {
      north, northReflected,
      east, eastReflected,
      south, southReflected,
      west, westReflected
    };

    var uniqueVariationCandidates = allVariationCandidates.DistinctBy(vc => string.Join("|", vc.Pattern));

    var makeVariation = (VariationCandidate vc) => new Variation(
      vc.Orientation,
      vc.Reflected,
      PatternCoordsList(vc.Pattern).ToArray()
    );

    var variations = uniqueVariationCandidates.Select(makeVariation);
    return new PieceWithVariations(label, variations.ToArray());
  }

  public static readonly PieceWithVariations[] PiecesWithVariations =
    PiecePatternsData.PiecesWithPatterns.Select(FindUniqueVariations).ToArray();
}
