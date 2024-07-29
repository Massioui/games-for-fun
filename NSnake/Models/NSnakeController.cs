namespace NSnake.Models;

internal sealed class NSnakeController
{
  public Point FieldSize { get; private set; }
  public Point StartingPosition { get; private set; }
  public Snake Snake { get; private set; } = default!;
  public Feed Feed { get; private set; } = default!;
  public Direction Direction { get; private set; }

  private Point _snakeHeadPosition => Snake.GetHead().Position;

  public NSnakeController(Point fieldSize, Point staringPosition, Direction direction = Direction.Right)
  {
    FieldSize = fieldSize;
    StartingPosition = staringPosition;
    Direction = direction;

    Initialize();
  }

  public void Start()
  {
    while (true)
    {
      if (Console.KeyAvailable)
      {
        ConsoleKey key = Console.ReadKey(true).Key;
        if (key == ConsoleKey.Q) break;

        Direction newDirection = key.ToDirection() ?? Snake.Direction;
        Snake.ChangeDirection(newDirection);
      }

      Snake.Move();

      if (IsCollisionDetected())
      {
        Console.Beep();
        break;
      }

      if (CanSnakeConsumeFeed())
      {
        HandleFeedConsumption();
        Snake.IncreaseSpeed(Digits.Five);
      }

      Snake.Draw();

      Task.Delay(Snake.Speed).Wait();
    }
  }

  private void Initialize()
  {
    InitializeSnake();
    InitializeFeed();
  }

  private void InitializeSnake()
  {
    Snake = new Snake(StartingPosition);
    Snake.Draw();
  }

  private void InitializeFeed()
  {
    Feed = GenerateFeed();
    Feed.Draw();
  }

  private Feed GenerateFeed()
  {
    Point[] snakeBody = Snake.GetBodyPositions();
    var feed =
          new Feed(Symbols.Asterisk)
          .Generate(snakeBody, FieldSize);

    return feed;
  }

  private bool IsCollisionDetected()
  {
    bool isCollision = IsBoundaryCollision() || Snake.IsSelfCollision(Feed.Position);
    return isCollision;
  }

  private bool IsBoundaryCollision()
  {
    int minX = Digits.Zero;
    int maxX = FieldSize.X;
    int minY = Digits.Zero;
    int maxY = FieldSize.Y;


    bool isXCollision = _snakeHeadPosition.X == minX || _snakeHeadPosition.X == maxX;
    bool isYCollision = _snakeHeadPosition.Y == minY || _snakeHeadPosition.Y == maxY;

    return isXCollision || isYCollision;
  }

  private bool CanSnakeConsumeFeed()
  {
    bool isHeadOnFeed = _snakeHeadPosition == Feed.Position;
    return isHeadOnFeed;
  }

  private void HandleFeedConsumption()
  {
    Snake.EatFeed(Feed.Position);
    Console.Beep();

    Feed.Erase();

    InitializeFeed();
  }
}
