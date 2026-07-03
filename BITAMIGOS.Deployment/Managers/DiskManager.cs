using BITAMIGOS.Deployment.Helpers;
using BITAMIGOS.Deployment.Services;

namespace BITAMIGOS.Deployment.Managers;

public static class DiskManager
{
    public static int SelectDisk()
    {
        Logger.Info("Suche Datenträger...");

        string output = DiskPartService.RunScript(new[]
        {
            "list disk",
            "exit"
        });

        Console.WriteLine(output);

        while (true)
        {
            Console.Write("Datenträgernummer: ");

            string? input = Console.ReadLine();

            if (int.TryParse(input, out int disk))
                return disk;

            Logger.Warning("Ungültige Eingabe.");
        }
    }

    public static bool ConfirmDiskErase(int diskNumber)
    {
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Red;

        Console.WriteLine("ACHTUNG!");

        Console.ResetColor();

        Console.WriteLine($"Datenträger {diskNumber} wird vollständig gelöscht.");

        Console.WriteLine();

        Console.Write("Zum Fortfahren JA eingeben: ");

        string? input = Console.ReadLine();

        return input != null &&
               input.Trim().Equals(
                    "JA",
                    StringComparison.OrdinalIgnoreCase);
    }
}