namespace NSnake.Extensions;
internal static class ConsoleKeyExtensions
{
  /// <summary>
  /// Converts a specified <see cref="ConsoleKey"/> to its corresponding <see cref="Direction"/>.
  /// </summary>
  /// <param name="key">The console key to convert.</param>
  /// <returns>
  /// A <see cref="Direction"/> value that corresponds to the specified <paramref name="key"/>.
  /// If the key is not a valid direction key, returns <c>null</c>.
  /// </returns>
  public static Direction? ToDirection(this ConsoleKey key)
  {
    return key switch
    {
      ConsoleKey.W or ConsoleKey.UpArrow => Direction.Up,
      ConsoleKey.S or ConsoleKey.DownArrow => Direction.Down,
      ConsoleKey.A or ConsoleKey.LeftArrow => Direction.Left,
      ConsoleKey.D or ConsoleKey.RightArrow => Direction.Right,
      _ => default
    };
  }
}
