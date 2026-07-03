using BITAMIGOS.Deployment.Helpers;

namespace BITAMIGOS.Deployment.Services;

public static class BCDBootService
{
    public static bool CreateBoot(string windowsDrive, string efiDrive)
    {
        Logger.Info("Erstelle Bootloader...");

        if (!Directory.Exists(windowsDrive + @"\Windows"))
        {
            Logger.Error($"Windows-Ordner nicht gefunden: {windowsDrive}\\Windows");
            return false;
        }

        if (!Directory.Exists(efiDrive + @"\"))
        {
            Logger.Error($"EFI-Partition nicht gefunden: {efiDrive}");
            return false;
        }

        string arguments =
            $"{windowsDrive}\\Windows /s {efiDrive} /f UEFI";

        Logger.Info($"bcdboot.exe {arguments}");

        var result = ProcessRunner.Run(
            "bcdboot.exe",
            arguments);

        if (!result.Success)
        {
            Logger.Error("BCDBoot fehlgeschlagen.");
            return false;
        }

        Logger.Success("Bootloader erfolgreich erstellt.");

        return true;
    }
}