namespace BITAMIGOS.Deployment.Models;

public class ImageInfo
{
    public int Index { get; set; }

    public string Name { get; set; } = "";

    public override string ToString()
    {
        return $"{Index} - {Name}";
    }
}