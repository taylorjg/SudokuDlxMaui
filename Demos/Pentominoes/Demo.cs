using Microsoft.Extensions.Logging;

namespace SudokuDlxMaui.Demos.Pentominoes;

public class PentominoesDemo : IDemo
{
  private ILogger<PentominoesDemo> _logger;

  public PentominoesDemo(ILogger<PentominoesDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(DemoPageBaseViewModel demoPageBaseViewModel)
  {
    return new PentominoesDrawable(demoPageBaseViewModel);
  }

  public object[] BuildInternalRows(object demoSettings)
  {
    return AllPossiblePiecePlacements().Where(IsValidPiecePlacement).ToArray();
  }

  public int[][] BuildMatrix(object[] internalRows)
  {
    return (internalRows as PentominoesInternalRow[]).Select(internalRow =>
    {
      var pieceColumns = MakePieceColumns(internalRow);
      var locationColumns = MakeLocationColumns(internalRow);
      return pieceColumns.Concat(locationColumns).ToArray();
    }).ToArray();
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    return null;
  }

  private static readonly Coords[] Locations =
    Enumerable.Range(0, 8).SelectMany(row =>
        Enumerable.Range(0, 8).Select(col =>
          new Coords(row, col))).ToArray();

  private bool IsValidPiecePlacement(PentominoesInternalRow internalRow)
  {
    foreach (var coords in internalRow.Variation.CoordsList)
    {
      var row = internalRow.Location.Row + coords.Row;
      var col = internalRow.Location.Col + coords.Col;
      if (row >= 8 || col >= 8) return false;
      if ((row == 3 || row == 4) && (col == 3 || col == 4)) return false;
    }
    return true;
  }

  private IEnumerable<PentominoesInternalRow> AllPossiblePiecePlacements()
  {
    foreach (var pieceWithVariations in PiecesWithVariations.ThePiecesWithVariations)
    {
      foreach (var variation in pieceWithVariations.Variations)
      {
        foreach (var location in Locations)
        {
          yield return new PentominoesInternalRow(
            pieceWithVariations.Label,
            variation,
            location);
        }
      }
    }
  }

  private IEnumerable<int> MakePieceColumns(PentominoesInternalRow internalRow)
  {
    var columns = Enumerable.Repeat(0, Pieces.ThePieces.Length).ToArray();
    var pieceIndex = Array.FindIndex(Pieces.ThePieces, p => p.Label == internalRow.Label);
    columns[pieceIndex] = 1;
    return columns;
  }

  private IEnumerable<int> MakeLocationColumns(PentominoesInternalRow internalRow)
  {
    var indices = internalRow.Variation.CoordsList.Select(coords =>
    {
      var row = internalRow.Location.Row + coords.Row;
      var col = internalRow.Location.Col + coords.Col;
      return row * 8 + col;
    });
    var columns = Enumerable.Repeat(0, 8 * 8).ToArray();
    foreach (var index in indices)
    {
      columns[index] = 1;
    }
    var indicesToExclude = new[] {
      3 * 8 + 3,
      3 * 8 + 4,
      4 * 8 + 3,
      4 * 8 + 4
    };
    return columns.Where((_, index) => !indicesToExclude.Contains(index));
  }
}