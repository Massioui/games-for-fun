namespace NSnake.Models;

internal sealed class Snake
{
  public char Symbol { get; private set; }
  public Direction Direction { get; private set; }
  public int Speed { get; private set; }

  private readonly List<SnakePart> _body;
  public SnakePart Head => _body[^1];
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

    var isCollisionDetected = bodyWithoutHeadPositions.Exists(position => position == Head.Position);

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
      Direction.Up => new SnakePart(new Point(Head.Position.X, Head.Position.Y - 1), Symbol),
      Direction.Down => new SnakePart(new Point(Head.Position.X, Head.Position.Y + 1), Symbol),
      Direction.Left => new SnakePart(new Point(Head.Position.X - 1, Head.Position.Y), Symbol),
      Direction.Right => new SnakePart(new Point(Head.Position.X + 1, Head.Position.Y), Symbol),
      _ => throw new InvalidDirectionException(string.Format(ExceptionMessages.InvalidDirection, direction))
    };
  }
}

