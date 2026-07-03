using System.Text;
using BITAMIGOS.Deployment.Helpers;

namespace BITAMIGOS.Deployment.Services;

public static class DiskPartService
{
    public static string RunScript(IEnumerable<string> commands)
    {
        string scriptFile = Path.Combine(
            Path.GetTempPath(),
            $"diskpart_{Guid.NewGuid():N}.txt");

        File.WriteAllLines(scriptFile, commands);

        try
        {
            Logger.Info($"DiskPart Script erstellt: {scriptFile}");

            var result = ProcessRunner.Run(
                "diskpart.exe",
                $"/s \"{scriptFile}\"");

            if (!result.Success)
            {
                Logger.Error("DiskPart wurde mit einem Fehler beendet.");
            }

            return result.Output;
        }
        finally
        {
            try
            {
                if (File.Exists(scriptFile))
                    File.Delete(scriptFile);
            }
            catch
            {
                // Ignorieren
            }
        }
    }
}