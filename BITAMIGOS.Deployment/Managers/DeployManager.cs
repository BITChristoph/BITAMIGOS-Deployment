using BITAMIGOS.Deployment.Helpers;
using BITAMIGOS.Deployment.Services;

namespace BITAMIGOS.Deployment.Managers;

public static class DeployManager
{
    public static void Run()
    {
        Logger.Initialize();

        ConsoleHelper.Header();

        Logger.Info("BITAMIGOS Deployment Tool gestartet.");

        string? image = ImageManager.FindImage();

        if (image == null)
        {
            Logger.Error("Kein Windows Image gefunden.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Image gefunden:");
        Console.WriteLine(image);
        Console.WriteLine();

        Console.Write("ENTER = Image verwenden oder neuen Pfad eingeben: ");

        string? input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input))
            image = input;

        Console.WriteLine();

        Console.Write("Windows Laufwerk [W]: ");

        string? windowsLetter = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(windowsLetter))
            windowsLetter = "W";

        windowsLetter = windowsLetter.ToUpper();

        Console.WriteLine();

        int disk = DiskManager.SelectDisk();

        Console.WriteLine();

        if (!DiskManager.ConfirmDiskErase(disk))
        {
            Logger.Warning("Deployment wurde abgebrochen.");

            Console.ReadKey();

            return;
        }

        if (!PartitionManager.PartitionDisk(
                disk,
                windowsLetter))
        {
            Console.ReadKey();
            return;
        }

        if (!DismService.ApplyImage(
                image,
                1,
                windowsLetter + ":"))
        {
            Console.ReadKey();
            return;
        }

        if (!BCDBootService.CreateBoot(
                windowsLetter + ":",
                "S:"))
        {
            Console.ReadKey();
            return;
        }

        Logger.Success("Deployment erfolgreich abgeschlossen.");

        Console.ReadKey();
    }
}