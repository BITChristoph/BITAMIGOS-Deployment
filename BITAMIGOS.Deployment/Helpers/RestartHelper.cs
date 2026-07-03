using BITAMIGOS.Deployment.Helpers;

namespace BITAMIGOS.Deployment.Helpers;

public static class RestartHelper
{
    public static void AskForRestart()
    {
        Console.WriteLine();
        Console.Write("PC jetzt neu starten? (J/N): ");

        string? input = Console.ReadLine();

        if (input == null)
            return;

        if (input.Equals("J", StringComparison.OrdinalIgnoreCase))
        {
            Logger.Info("Starte Computer neu...");

            ProcessRunner.Run(
                "shutdown.exe",
                "/r /t 0");
        }
    }
}