namespace NSnake.Models;

internal sealed class Snake
{
  public Snake(Point startingPoint, Point fieldSize, int length, char symbol = Symbols.Asterisk, Direction direction = Direction.Right)
  {
    Symbol = symbol;
    CurrentDirection = direction;
    FieldSize = fieldSize;

    _body = Enumerable
        .Range(Digits.Zero, length)
        .Select(x => new SnakePart(startingPoint, symbol))
        .ToList();

    _feed =
        new Feed(Symbols.Asterisk)
        .Generate(ExtractBodyPositions(), fieldSize);

    _feed.Draw();
  }

  public char Symbol { get; private set; }

  public Direction CurrentDirection { get; private set; }

  public Point FieldSize { get; private set; }

  private readonly List<SnakePart> _body;

  private SnakePart _head => _body[^1];

  private SnakePart _tail => _body[Digits.Zero];

  private Feed _feed;

  public void Draw()
  {
    foreach (var snakePart in _body) snakePart.Draw();
  }

  public void Move(Direction direction)
  {
    if (CurrentDirection.IsOpposite(direction)) direction = CurrentDirection;
    
    CurrentDirection = direction;

    bool isHeadOnFeed = _head.Position == _feed.Position;
    if (isHeadOnFeed) EatFeed();

    SnakePart newHead = CreateHeadInDirection(direction);

    _tail.Erase();

    _body.Remove(_tail);  

    _body.Add(newHead);

    newHead.Draw();

    Task.Delay(100).Wait();
  }

  private void EatFeed()
  {
    Console.Beep();

    _body.Add(new SnakePart(_feed.Position, Symbol));

    _feed.Erase();

    _feed =
       new Feed(Symbols.Asterisk)
       .Generate(ExtractBodyPositions(), FieldSize);

    _feed.Draw();
  }

  private Point[] ExtractBodyPositions() =>
      _body.Select(snakePart => snakePart.Position).ToArray();

  public SnakePart CreateHeadInDirection(Direction direction)
  {
    return direction switch
    {
      Direction.Up => new SnakePart(new Point(_head.Position.X, _head.Position.Y - 1), Symbol),
      Direction.Down => new SnakePart(new Point(_head.Position.X, _head.Position.Y + 1), Symbol),
      Direction.Left => new SnakePart(new Point(_head.Position.X - 1, _head.Position.Y), Symbol),
      Direction.Right => new SnakePart(new Point(_head.Position.X + 1, _head.Position.Y), Symbol),
      _ => throw new InvalidDirectionException(string.Format(ExceptionMessages.InvalidDirection, direction))
    };
  }
}

