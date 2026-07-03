namespace BITAMIGOS.Deployment.Helpers;

public static class Logger
{
    private static string _logFile = string.Empty;

    public static void Initialize()
    {
        string logDirectory = Path.Combine(
            AppContext.BaseDirectory,
            "Logs");

        Directory.CreateDirectory(logDirectory);

        _logFile = Path.Combine(
            logDirectory,
            $"Deploy_{DateTime.Now:yyyyMMdd_HHmmss}.log");
    }

    public static void Info(string text)
    {
        Write("INFO", ConsoleColor.White, text);
    }

    public static void Warning(string text)
    {
        Write("WARNING", ConsoleColor.Yellow, text);
    }

    public static void Error(string text)
    {
        Write("ERROR", ConsoleColor.Red, text);
    }

    public static void Success(string text)
    {
        Write("SUCCESS", ConsoleColor.Green, text);
    }

    private static void Write(
        string level,
        ConsoleColor color,
        string text)
    {
        string line =
            $"[{DateTime.Now:HH:mm:ss}] {level,-8} {text}";

        Console.ForegroundColor = color;

        Console.WriteLine(line);

        Console.ResetColor();

        if (!string.IsNullOrWhiteSpace(_logFile))
        {
            File.AppendAllText(
                _logFile,
                line + Environment.NewLine);
        }
    }
}