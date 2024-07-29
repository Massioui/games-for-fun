Console.CursorVisible = false;
Point startingPoint = new(Console.WindowWidth / Digits.Two, Console.WindowHeight / Digits.Two);
Point fieldSize = new(Console.WindowWidth, Console.WindowHeight);


new NSnakeController(fieldSize, startingPoint)
  .Start();

Console.ReadKey();

