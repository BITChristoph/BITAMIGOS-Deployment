using BITAMIGOS.Deployment.Helpers;

namespace BITAMIGOS.Deployment.Services;

public static class BCDBootService
{
    public static bool CreateBoot(
        string windowsDrive,
        string efiDrive)
    {
        Logger.Info("Erstelle Bootloader...");

        var result = ProcessRunner.Run(
            "bcdboot.exe",
            $"{windowsDrive}\\Windows /s {efiDrive} /f UEFI");

        if (result.Success)
        {
            Logger.Success("Bootloader erfolgreich erstellt.");
            return true;
        }

        Logger.Error("Bootloader konnte nicht erstellt werden.");

        return false;
    }
}