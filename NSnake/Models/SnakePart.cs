namespace NSnake.Models;

internal sealed class SnakePart
{
    public SnakePart(Point position, char symbol) {
        Symbol = symbol;
        Position = position;
    }

    public char Symbol { get; private set; }
    public Point Position { get; private set; }

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
