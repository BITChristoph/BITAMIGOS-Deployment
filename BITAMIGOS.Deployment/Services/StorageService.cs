using BITAMIGOS.Deployment.Models;

namespace BITAMIGOS.Deployment.Services;

/// <summary>
/// Stellt Informationen über Datenträger und Partitionen bereit.
/// </summary>
public static class StorageService
{
    public static List<DiskInfo> GetDisks()
    {
        return new List<DiskInfo>();
    }

    public static List<PartitionInfo> GetPartitions()
    {
        return new List<PartitionInfo>();
    }

    public static DiskInfo? GetUsbDisk()
    {
        return null;
    }

    public static DiskInfo? GetTargetDisk()
    {
        return null;
    }
}