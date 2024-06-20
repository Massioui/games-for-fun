using NSnake.Extensions;

Console.CursorVisible = false;
Point startingPoint = new(Console.WindowWidth / 2, Console.WindowHeight / 2);
Snake snake = new(startingPoint, 5, Symbols.Asterisk);

snake.Draw();

while (true)
{
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