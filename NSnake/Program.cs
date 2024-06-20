Console.CursorVisible = false;
Point startingPoint = new(Console.WindowWidth / 2, Console.WindowHeight / 2);
Snake snake = new(startingPoint, 5, Symbols.Asterisk);

snake.Draw();

while (true)
{
    ConsoleKey key = Console.ReadKey(true).Key;

    if (key == ConsoleKey.Q) break;

    snake.Move(key switch
    {
        ConsoleKey.W or ConsoleKey.UpArrow => Direction.Up,
        ConsoleKey.S or ConsoleKey.DownArrow => Direction.Down,
        ConsoleKey.A or ConsoleKey.LeftArrow => Direction.Left,
        ConsoleKey.D or ConsoleKey.RightArrow => Direction.Right,
        _ => default
    });
}

Console.CursorVisible = true;

Console.ReadKey();