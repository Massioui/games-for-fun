Console.CursorVisible = false;
Point startingPoint = new(Console.WindowWidth / Digits.Two, Console.WindowHeight / Digits.Two);
Point fieldSize = new(Console.WindowWidth, Console.WindowHeight);
Snake snake = new(startingPoint, fieldSize, Symbols.Asterisk);

snake.Draw();

while (true)
{
  if (snake.IsSelfCollision()) break;

  if (!Console.KeyAvailable)
  {
    snake.Move(snake.CurrentDirection);
    continue;
  }

  ConsoleKey key = Console.ReadKey(true).Key;

  if (key == ConsoleKey.Q) break;

  snake.Move(key.ToDirection() ?? snake.CurrentDirection);
}

Console.CursorVisible = true;

Console.ReadKey();
