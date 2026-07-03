namespace BITAMIGOS.Deployment.Models;

/// <summary>
/// Enthält das Ergebnis eines gestarteten Prozesses.
/// </summary>
public sealed class ProcessResult
{
    public int ExitCode { get; init; }

    public string Output { get; init; } = string.Empty;

    public string Error { get; init; } = string.Empty;

    public TimeSpan Duration { get; init; }

    public bool Success => ExitCode == 0;
}