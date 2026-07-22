using BITAMIGOS.Deployment.Helpers;

Console.WriteLine();
Console.Write("PC jetzt neu starten? (J/N): ");

string? input = Console.ReadLine();

if (input?.Equals("J",
    StringComparison.OrdinalIgnoreCase) == true)
{
    Logger.Info("Starte Computer neu...");

    ProcessRunner.Run(
        "wpeutil.exe",
        "reboot");
}