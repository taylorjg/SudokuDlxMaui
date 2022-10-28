using DlxLib;

namespace SudokuDlxMaui;

public record PentominoesInternalRow(string Label, Variation Variation, Coords Location);

public interface IPentominoesSolver
{
  public PentominoesInternalRow[] Solve();
}

public class PentominoesSolver : IPentominoesSolver
{
  private static readonly Coords[] Locations =
    Enumerable.Range(0, 8).SelectMany(row =>
        Enumerable.Range(0, 8).Select(col =>
          new Coords(row, col))).ToArray();

  public PentominoesInternalRow[] Solve()
  {
    var internalRows = BuildInternalRows();
    var matrix = BuildMatrix(internalRows);
    var dlx = new DlxLib.Dlx();
    var solution = dlx.Solve(matrix, r => r, c => c).FirstOrDefault();
    if (solution != null)
    {
      return solution.RowIndexes.Select(rowIndex => internalRows[rowIndex]).ToArray();
    }
    return null;
  }

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
    foreach (var pieceWithVariations in PiecesData.PiecesWithVariations)
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

  private PentominoesInternalRow[] BuildInternalRows()
  {
    return AllPossiblePiecePlacements().Where(IsValidPiecePlacement).ToArray();
  }

  private IEnumerable<int> MakePieceColumns(PentominoesInternalRow internalRow)
  {
    var columns = Enumerable.Repeat(0, PiecePatternsData.PiecesWithPatterns.Length).ToArray();
    var pieceIndex = Array.FindIndex(PiecePatternsData.PiecesWithPatterns, p => p.Label == internalRow.Label);
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

  private int[][] BuildMatrix(PentominoesInternalRow[] internalRows)
  {
    return internalRows.Select(internalRow =>
    {
      var pieceColumns = MakePieceColumns(internalRow);
      var locationColumns = MakeLocationColumns(internalRow);
      return pieceColumns.Concat(locationColumns).ToArray();
    }).ToArray();
  }
}
