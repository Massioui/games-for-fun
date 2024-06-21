namespace NSnake.Extensions;

internal static class DirectionExtensions
{
  /// <summary>
  /// Determines whether the specified direction is the opposite of the current direction.
  /// </summary>
  /// <param name="direction">The current direction.</param>
  /// <param name="other">The direction to compare against.</param>
  /// <returns><c>true</c> if the specified direction is opposite of the current direction; otherwise, <c>false</c>.</returns>
  public static bool IsOpposite(this Direction direction, Direction other) =>
    (direction, other) switch
    {
      (Direction.Up, Direction.Down) => true,
      (Direction.Down, Direction.Up) => true,
      (Direction.Left, Direction.Right) => true,
      (Direction.Right, Direction.Left) => true,
      _ => false
    };
}
