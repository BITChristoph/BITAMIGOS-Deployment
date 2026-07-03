using BITAMIGOS.Deployment.Managers;

namespace BITAMIGOS.Deployment;

internal class Program
{
    static void Main(string[] args)
    {
        Console.Title = "BITAMIGOS Deployment Tool v1.0";

        DeployManager.Run();
    }
}