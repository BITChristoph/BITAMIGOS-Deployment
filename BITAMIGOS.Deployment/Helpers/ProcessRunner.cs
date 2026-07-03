using System.Diagnostics;
using System.Text;
using BITAMIGOS.Deployment.Models;

namespace BITAMIGOS.Deployment.Helpers;

public static class ProcessRunner
{
    public static ProcessResult Run(string fileName, string arguments)
    {
        Logger.Info($"Starte Prozess: {fileName} {arguments}");

        Stopwatch timer = Stopwatch.StartNew();

        Process process = new();

        process.StartInfo.FileName = fileName;
        process.StartInfo.Arguments = arguments;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;

        StringBuilder output = new();
        StringBuilder error = new();

        process.OutputDataReceived += (_, e) =>
        {
            if (string.IsNullOrWhiteSpace(e.Data))
                return;

            output.AppendLine(e.Data);

            Logger.Info(e.Data);
        };

        process.ErrorDataReceived += (_, e) =>
        {
            if (string.IsNullOrWhiteSpace(e.Data))
                return;

            error.AppendLine(e.Data);

            Logger.Error(e.Data);
        };

        process.Start();

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        process.WaitForExit();

        timer.Stop();

        ProcessResult result = new()
        {
            ExitCode = process.ExitCode,
            Output = output.ToString(),
            Error = error.ToString(),
            Duration = timer.Elapsed
        };

        if (result.Success)
            Logger.Success($"Prozess erfolgreich beendet ({result.Duration.TotalSeconds:F2}s)");
        else
            Logger.Error($"Prozess beendet mit ExitCode {result.ExitCode}");

        return result;
    }
}