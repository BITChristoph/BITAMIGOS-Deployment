using BITAMIGOS.Deployment.Helpers;

namespace BITAMIGOS.Deployment.Services;

public static class DismService
{
    public static bool ApplyImage(
        string imageFile,
        int index,
        string windowsDrive)
    {
        if (!File.Exists(imageFile))
        {
            Logger.Error($"Image nicht gefunden: {imageFile}");
            return false;
        }

        if (!Directory.Exists(windowsDrive + @"\"))
        {
            Logger.Error($"Windows Partition nicht gefunden: {windowsDrive}");
            return false;
        }

        Logger.Info("Installiere Windows...");

        string arguments =
            $"/Apply-Image " +
            $"/ImageFile:\"{imageFile}\" " +
            $"/Index:{index} " +
            $"/ApplyDir:{windowsDrive}\\";

        var result = ProcessRunner.Run(
            "dism.exe",
            arguments);

        if (!result.Success)
        {
            Logger.Error("DISM fehlgeschlagen.");
            return false;
        }

        Logger.Success("Windows erfolgreich installiert.");

        return true;
    }
}