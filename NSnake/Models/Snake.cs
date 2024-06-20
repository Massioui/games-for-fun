namespace NSnake.Models;

internal sealed class Snake
{
    public Snake(Point startingPoint, int length, char symbol = Symbols.Asterisk, Direction direction = Direction.Right)
    {
        Symbol = symbol;
        CurrentDirection = direction;

        _body = Enumerable
            .Range(Digits.Zero, length)
            .Select(x => new SnakePart(startingPoint, symbol))
            .ToArray();
    }

    public char Symbol { get; private set; }
    public Direction CurrentDirection { get; private set; }

    private readonly SnakePart[] _body;
    private SnakePart Head => _body[Digits.Zero];
    private SnakePart Tail => _body[^1];

    public void Draw()
    {
        foreach (var snakePart in _body) snakePart.Draw();
    }

    public void Move(Direction direction)
    {
        CurrentDirection = direction;

        SnakePart newHead = direction switch
        {
            Direction.Up => new SnakePart(new Point(Head.Position.X, Head.Position.Y - 1), Symbol),
            Direction.Down => new SnakePart(new Point(Head.Position.X, Head.Position.Y + 1), Symbol),
            Direction.Left => new SnakePart(new Point(Head.Position.X - 1, Head.Position.Y), Symbol),
            Direction.Right => new SnakePart(new Point(Head.Position.X + 1, Head.Position.Y), Symbol),
            _ => throw new InvalidDirectionException(string.Format(ExceptionMessages.InvalidDirection, direction))
        };


        Tail.Erase();

        // TODO: Should be use List instead of array
        for (var index = _body.Length - 1; index > 0; index--)
        {
            _body[index] = _body[index - 1];
        }
        _body[0] = newHead;


        Draw();

        // TODO: Speed control
        Task.Delay(100).Wait();
    }
}

