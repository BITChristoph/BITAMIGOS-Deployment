namespace BITAMIGOS.Deployment.Helpers;

public static class ConsoleHelper
{
    public static void Header()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine("==============================================");
        Console.WriteLine("        BITAMIGOS Deployment Tool");
        Console.WriteLine("               Version 1.0");
        Console.WriteLine("==============================================");

        Console.ResetColor();

        Console.WriteLine();
    }
}