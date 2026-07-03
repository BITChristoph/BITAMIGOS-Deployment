using System.Text.RegularExpressions;
using BITAMIGOS.Deployment.Helpers;
using BITAMIGOS.Deployment.Models;

namespace BITAMIGOS.Deployment.Managers;

public static class ImageManager
{
    public static string? FindImage()
    {
        Logger.Info("Suche Windows-Image...");

        foreach (DriveInfo drive in DriveInfo.GetDrives())
        {
            if (!drive.IsReady)
                continue;

            try
            {
                string wim = Path.Combine(
                    drive.RootDirectory.FullName,
                    "sources",
                    "install.wim");

                if (File.Exists(wim))
                {
                    Logger.Success($"install.wim gefunden: {wim}");
                    return wim;
                }

                string esd = Path.Combine(
                    drive.RootDirectory.FullName,
                    "sources",
                    "install.esd");

                if (File.Exists(esd))
                {
                    Logger.Success($"install.esd gefunden: {esd}");
                    return esd;
                }
            }
            catch (Exception ex)
            {
                Logger.Warning(
                    $"Laufwerk {drive.Name}: {ex.Message}");
            }
        }

        Logger.Error("Kein Windows-Image gefunden.");

        return null;
    }

    public static List<ImageInfo> GetImages(string imageFile)
    {
        List<ImageInfo> images = new();

        if (!File.Exists(imageFile))
        {
            Logger.Error("Image existiert nicht.");

            return images;
        }

        Logger.Info("Lese Windows-Editionen...");

        var result = ProcessRunner.Run(
            "dism.exe",
            $"/Get-WimInfo /WimFile:\"{imageFile}\"");

        if (!result.Success)
        {
            Logger.Error("DISM konnte das Image nicht lesen.");

            return images;
        }

        string[] lines =
            result.Output.Split(
                Environment.NewLine,
                StringSplitOptions.RemoveEmptyEntries);

        int currentIndex = 0;

        string currentName = "";
        foreach (string line in lines)
        {
            string text = line.Trim();

            if (text.StartsWith("Index", StringComparison.OrdinalIgnoreCase))
            {
                Match match = Regex.Match(text, @"(\d+)");

                if (match.Success)
                {
                    currentIndex = int.Parse(match.Value);
                }
            }

            if (text.StartsWith("Name", StringComparison.OrdinalIgnoreCase))
            {
                int pos = text.IndexOf(':');

                if (pos > -1)
                {
                    currentName = text[(pos + 1)..].Trim();

                    if (currentIndex > 0)
                    {
                        images.Add(new ImageInfo
                        {
                            Index = currentIndex,
                            Name = currentName
                        });

                        currentIndex = 0;
                        currentName = "";
                    }
                }
            }
        }

        if (images.Count == 0)
        {
            Logger.Warning("Keine Windows Editionen gefunden.");
        }
        else
        {
            Logger.Success($"{images.Count} Windows Edition(en) gefunden.");
        }

        return images;
    }

    public static int SelectImageIndex(List<ImageInfo> images)
    {
        Console.WriteLine();
        Console.WriteLine("Verfügbare Windows Images");
        Console.WriteLine("-------------------------");

        foreach (ImageInfo image in images)
        {
            Console.WriteLine($"{image.Index} - {image.Name}");
        }

        Console.WriteLine();

        while (true)
        {
            Console.Write("Index auswählen: ");

            string? input = Console.ReadLine();

            if (int.TryParse(input, out int index))
            {
                if (images.Any(i => i.Index == index))
                    return index;
            }

            Logger.Warning("Ungültiger Index.");
        }
    }
}