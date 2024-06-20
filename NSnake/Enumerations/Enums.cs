using System.ComponentModel;

namespace NSnake.Enumerations;

internal enum Direction : ushort
{
    [Description("Move Upwards")]
    Up = 0,

    [Description("Move Downwards")]
    Down = 1,

    [Description("Move Leftwards")]
    Left = 2,

    [Description("Move Rightwards")]
    Right = 3
}