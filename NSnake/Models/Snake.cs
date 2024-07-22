namespace NSnake.Models;

internal sealed class Snake
{
  public char Symbol { get; private set; }
  public Direction CurrentDirection { get; private set; }
  public Point FieldSize { get; private set; }

  private readonly List<SnakePart> _body;
  private SnakePart _head => _body[^1];
  private SnakePart _tail => _body[Digits.Zero];
  private Feed _feed = null!;

  public Snake(Point startingPoint, Point fieldSize, char symbol = Symbols.Asterisk, Direction direction = Direction.Right)
  {
    Symbol = symbol;
    CurrentDirection = direction;
    FieldSize = fieldSize;

    _body = [new SnakePart(startingPoint, symbol)];

    GenerateFeed();
  }

  public void Draw()
  {
    foreach (var snakePart in _body) snakePart.Draw();
  }

  public void Move(Direction direction)
  {
    if (CurrentDirection.IsOpposite(direction)) direction = CurrentDirection;

    CurrentDirection = direction;

    HandleFeedConsumption();

    SnakePart newHead = CreateHeadInDirection(direction);

    _body.Add(newHead);

    _tail.Erase();

    _body.Remove(_tail);

    newHead.Draw();

    Task.Delay(100).Wait();
  }

  public bool IsSelfCollision()
  {
    List<SnakePart> bodyWithoutHead = _body[..^1];
    List<Point> bodyWithoutHeadPositions = bodyWithoutHead.Select(snakePart => snakePart.Position).ToList();
    var isCollisionDetected = bodyWithoutHeadPositions.Exists(position => position == _head.Position);

    return isCollisionDetected;
  }

  public bool IsBoundaryCollision()
  {
    int minX = Digits.Zero;
    int maxX = FieldSize.X;
    int minY = Digits.Zero;
    int maxY = FieldSize.Y;

    Point snakeHeadPosition = _head.Position;

    bool isXCollision = snakeHeadPosition.X == minX || snakeHeadPosition.X == maxX;
    bool isYCollision = snakeHeadPosition.Y == minY || snakeHeadPosition.Y == maxY;

    return isXCollision || isYCollision;
  }

  private void GenerateFeed()
  {
    _feed =
          new Feed(Symbols.Asterisk)
          .Generate(ExtractBodyPositions(), FieldSize);

    _feed.Draw();
  }

  private Point[] ExtractBodyPositions() =>
      _body.Select(snakePart => snakePart.Position).ToArray();

  private void HandleFeedConsumption()
  {
    bool isHeadOnFeed = _head.Position == _feed.Position;

    if (!isHeadOnFeed) return;

    EatFeed();
    GenerateFeed();
  }

  private void EatFeed()
  {
    Console.Beep();

    _body.Add(new SnakePart(_feed.Position, Symbol));

    _feed.Erase();
  }

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

