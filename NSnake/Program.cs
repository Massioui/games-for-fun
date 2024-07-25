Console.CursorVisible = false;
Point startingPoint = new(Console.WindowWidth / Digits.Two, Console.WindowHeight / Digits.Two);
Point fieldSize = new(Console.WindowWidth, Console.WindowHeight);
Snake snake = new(startingPoint, Symbols.Asterisk);

snake.Draw();

Feed feed = GenerateFeed(snake, fieldSize);
feed.Draw();

while (true)
{
  if (Console.KeyAvailable)
  {
    ConsoleKey key = Console.ReadKey(true).Key;
    if (key == ConsoleKey.Q) break;

    Direction newDirection = key.ToDirection() ?? snake.Direction;
    snake.ChangeDirection(newDirection);
  }

  snake.Move();

  if (IsCollisionDetected(snake, feed.Position))
  {
    Console.Beep();
    break;
  }

  if (CanConsumeFeed(snake, feed))
  {
    HandleFeedConsumption(snake, feed);
    feed = GenerateFeed(snake, fieldSize);
    feed.Draw();
    snake.IncreaseSpeed(Digits.Five);
  }

  snake.Draw();

  Task.Delay(snake.Speed).Wait();
}

Console.ReadKey();

bool IsCollisionDetected(Snake snake, Point feedPosition)
{
  bool isCollision = IsBoundaryCollision(snake.Head.Position) || snake.IsSelfCollision(feedPosition);
  return isCollision;
}

bool IsBoundaryCollision(Point snakeHead)
{
  int minX = Digits.Zero;
  int maxX = fieldSize.X;
  int minY = Digits.Zero;
  int maxY = fieldSize.Y;

  bool isXCollision = snakeHead.X == minX || snakeHead.X == maxX;
  bool isYCollision = snakeHead.Y == minY || snakeHead.Y == maxY;

  return isXCollision || isYCollision;
}

bool CanConsumeFeed(Snake snake, Feed feed)
{
  bool isHeadOnFeed = snake.Head.Position == feed.Position;
  return isHeadOnFeed;
}

void HandleFeedConsumption(Snake snake, Feed feed)
{
  snake.EatFeed(feed.Position);
  Console.Beep();

  feed.Erase();
}

Feed GenerateFeed(Snake snake, Point fieldSize)
{
  Point[] snakeBody = snake.GetBodyPositions();
  var feed =
        new Feed(Symbols.Asterisk)
        .Generate(snakeBody, fieldSize);

  return feed;
}
