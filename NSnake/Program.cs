using System.ComponentModel;

ValidateRequirements();
Setup();

Console.ReadKey();


static void ValidateRequirements()
{
    ValidateWindowMinSize();
}

static void Setup()
{
    SetGameTitle();

    SetWindowBackground();

    SetFontColor();

    DrawBoundaries();
}

static void ValidateWindowMinSize()
{
    // TODO: move these following variables into global constant file
    int MIN_BUFFER_WIDTH = 120;
    int MIN_BUFFER_HEIGHT = 30;

    int windowBufferWidth = Console.BufferWidth;
    int windowBufferHeight = Console.BufferHeight;

    bool minSizeMeet = windowBufferWidth >= MIN_BUFFER_WIDTH;
    minSizeMeet &= windowBufferHeight >= MIN_BUFFER_HEIGHT;

    if (minSizeMeet) return;

    exitWithInvalidRequirementsError();

    static void exitWithInvalidRequirementsError()
    {
        // TODO: move these following variables into global constant file
        string requirementsNotMeetMessage =
      "Window size doesn't meet the minimum requirements. Please refer to the 'Game Requirements' section in the README file for further information.";
        string readKeyMessage = "Press any key to exit...";

        Console.Title = "Invalid Game Requirements !";
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(requirementsNotMeetMessage);
        SetFontColor();
        Console.WriteLine(readKeyMessage);
        Console.ReadKey();
        Environment.Exit((int)SystemError.INVALID_REQUIREMENTS);
    }
}

static void SetGameTitle()
{
    // TODO: move these following variables into global constant file
    Console.Title = "NSnake | 𓆓";
}

static void SetWindowBackground()
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.Clear();
}

static void SetFontColor()
{
    Console.ForegroundColor = ConsoleColor.Green;
}

static void DrawBoundaries()
{
    //Console.
}


// TODO: move this to global enumerations
enum SystemError : ushort
{
    [Description("The operation completed successfully.")]
    SUCCESS = 0,
    [Description("Minimum requirements are not meet to run the program.")]
    INVALID_REQUIREMENTS = 1000
}
