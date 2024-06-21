namespace NSnake.Models;

internal sealed class Feed
{
    public Feed(char symbol = Symbols.Asterisk)
    {
        Symbol = symbol;
    }

    public char Symbol { get; private set; }
    public Point Position { get; private set; }

    public Feed Generate(Point[] excludedPositions, Point fieldSize)
    {
        Random random = new();

        do
        {
            int x = random.Next(Digits.Zero, fieldSize.X);
            int y = random.Next(Digits.Zero, fieldSize.Y);

            Position = new Point(x, y);

        } while (Array.Exists(excludedPositions, position => position == Position));

        return this;
    }

    public void Draw()
    {
        Console.SetCursorPosition(Position.X, Position.Y);
        Console.Write(Symbol);
    }

    public void Erase()
    {
        Console.SetCursorPosition(Position.X, Position.Y);
        Console.Write(Symbols.WhiteSpace);
    }
}
