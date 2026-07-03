namespace BITAMIGOS.Deployment.Models;

public sealed class DiskInfo
{
    public int Number { get; set; }

    public string Model { get; set; } = string.Empty;

    public string DeviceId { get; set; } = string.Empty;

    public string InterfaceType { get; set; } = string.Empty;

    public long Size { get; set; }

    public bool IsUsb { get; set; }

    public string? DriveLetter { get; set; }

    public override string ToString()
    {
        return $"Disk {Number} - {Model}";
    }
}