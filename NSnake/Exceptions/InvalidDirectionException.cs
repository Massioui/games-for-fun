namespace NSnake.Exceptions;
public sealed class InvalidDirectionException : Exception
{
    public InvalidDirectionException() : base() { }

    public InvalidDirectionException(string message) : base(message) { }
}
