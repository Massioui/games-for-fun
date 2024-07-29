namespace NSnake.Models;

internal sealed class Snake
{
  public char Symbol { get; private set; }
  public Direction Direction { get; private set; }
  public int Speed { get; private set; }

  private readonly List<SnakePart> _body;
  private SnakePart _head => _body[^1];
  private SnakePart _tail => _body[Digits.Zero];

  public Snake(Point startingPoint, char symbol = Symbols.Asterisk, Direction direction = Direction.Right, int speed = Digits.OneHundred)
  {
    Symbol = symbol;
    Direction = direction;
    Speed = speed;

    _body = [new SnakePart(startingPoint, symbol)];
  }

  public void Draw()
  {
    foreach (var snakePart in _body) snakePart.Draw();
  }

  public void ChangeDirection(Direction direction)
  {
    if (direction.IsOpposite(Direction)) return;

    Direction = direction;
  }

  public void IncreaseSpeed(ushort delayReduction)
  {
    Speed = (delayReduction >= Speed) ? Speed : Speed - delayReduction;
  }

  public SnakePart GetHead() => _head;

  public void Move()
  {
    SnakePart newHead = CreateHeadInDirection(Direction);

    _body.Add(newHead);

    _tail.Erase();

    _body.Remove(_tail);
  }

  public bool IsSelfCollision(Point? excludePoint = default)
  {
    List<SnakePart> bodyWithoutHead = _body[..^1];
    List<Point> bodyWithoutHeadPositions = bodyWithoutHead.Select(snakePart => snakePart.Position).ToList();

    if (excludePoint.HasValue)
    {
      bodyWithoutHeadPositions = bodyWithoutHeadPositions.Where(position => position != excludePoint.Value).ToList();
    }

    var isCollisionDetected = bodyWithoutHeadPositions.Exists(position => position == _head.Position);

    return isCollisionDetected;
  }

  public Point[] GetBodyPositions()
  {
    Point[] bodyPositions =
      _body
      .Select(snakePart => snakePart.Position)
      .ToArray();

    return bodyPositions;
  }

  public void EatFeed(Point feedPosition)
  {
    _body.Add(new SnakePart(feedPosition, Symbol));
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

