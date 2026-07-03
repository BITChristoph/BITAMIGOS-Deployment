using BITAMIGOS.Deployment.Helpers;
using BITAMIGOS.Deployment.Services;

namespace BITAMIGOS.Deployment.Managers;

public static class PartitionManager
{
    public static bool PartitionDisk(int diskNumber, string windowsLetter)
    {
        Logger.Info($"Partitioniere Datenträger {diskNumber}...");

        string output = DiskPartService.RunScript(new[]
        {
            $"select disk {diskNumber}",
            "clean",
            "convert gpt",

            "create partition efi size=100",
            "format quick fs=fat32 label=\"System\"",
            "assign letter=S",

            "create partition msr size=16",

            "create partition primary",
            "format quick fs=ntfs label=\"Windows\"",
            $"assign letter={windowsLetter}",

            "list volume",
            "exit"
        });

        Logger.Info(output);

        if (!Directory.Exists(@"S:\"))
        {
            Logger.Error("EFI Partition wurde nicht erstellt.");
            return false;
        }

        if (!Directory.Exists(windowsLetter + @":\"))
        {
            Logger.Error("Windows Partition wurde nicht erstellt.");
            return false;
        }

        Logger.Success("Partitionierung erfolgreich.");

        return true;
    }
}