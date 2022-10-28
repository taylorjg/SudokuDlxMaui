namespace SudokuDlxMaui;

public static class PiecePatternsData
{
  private static readonly string[] F = new string[] {
    " XX",
    "XX ",
    " X "
  };

  private static readonly string[] I = new string[] {
    "X",
    "X",
    "X",
    "X",
    "X"
  };

  private static readonly string[] L = new string[] {
    "X ",
    "X ",
    "X ",
    "XX"
  };

  private static readonly string[] P = new string[] {
    "XX",
    "XX",
    "X "
  };

  private static readonly string[] N = new string[] {
    " X",
    "XX",
    "X ",
    "X "
  };

  private static readonly string[] T = new string[] {
    "XXX",
    " X ",
    " X "
  };

  private static readonly string[] U = new string[] {
    "X X",
    "XXX"
  };

  private static readonly string[] V = new string[] {
    "X  ",
    "X  ",
    "XXX"
  };

  private static readonly string[] W = new string[] {
    "X  ",
    "XX ",
    " XX"
  };

  private static readonly string[] X = new string[] {
    " X ",
    "XXX",
    " X "
  };

  private static readonly string[] Y = new string[] {
    " X",
    "XX",
    " X",
    " X"
  };

  private static readonly string[] Z = new string[] {
    "XX ",
    " X ",
    " XX"
  };

  public static readonly PieceWithPattern[] PiecesWithPatterns =
    new[] {
      new PieceWithPattern(nameof(F), F),
      new PieceWithPattern(nameof(I), I),
      new PieceWithPattern(nameof(L), L),
      new PieceWithPattern(nameof(P), P),
      new PieceWithPattern(nameof(N), N),
      new PieceWithPattern(nameof(T), T),
      new PieceWithPattern(nameof(U), U),
      new PieceWithPattern(nameof(V), V),
      new PieceWithPattern(nameof(W), W),
      new PieceWithPattern(nameof(X), X),
      new PieceWithPattern(nameof(Y), Y),
      new PieceWithPattern(nameof(Z), Z)
    };
}
