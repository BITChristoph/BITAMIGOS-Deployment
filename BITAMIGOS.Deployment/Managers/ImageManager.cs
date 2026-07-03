using BITAMIGOS.Deployment.Helpers;

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
                Logger.Warning($"Laufwerk {drive.Name}: {ex.Message}");
            }
        }

        Logger.Error("Kein Windows-Image gefunden.");
        return null;
    }
}