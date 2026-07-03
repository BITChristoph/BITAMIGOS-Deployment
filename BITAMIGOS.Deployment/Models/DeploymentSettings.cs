namespace BITAMIGOS.Deployment.Models;

public class DeploymentSettings
{
    public string? ImagePath { get; set; }

    public string? UsbDrive { get; set; }

    public int SelectedDisk { get; set; }

    public bool Confirmed { get; set; }
}