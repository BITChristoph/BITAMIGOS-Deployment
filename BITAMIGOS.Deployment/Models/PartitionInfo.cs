namespace BITAMIGOS.Deployment.Models;

public sealed class PartitionInfo
{
    public int DiskNumber { get; set; }

    public int PartitionNumber { get; set; }

    public string DriveLetter { get; set; } = string.Empty;

    public string FileSystem { get; set; } = string.Empty;

    public long Size { get; set; }

    public bool IsBoot { get; set; }

    public bool IsSystem { get; set; }
}