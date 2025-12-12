using FolderSynchronizer.src.Services;

namespace FolderSynchronizer.src;

public class Program
{
    private static string sourcePath;
    private static string destinationPath;
    private static string logPath;
    private static int intervalInSecounds;
    public static void Main(string[] args)
    {
        var logger = new LoggerService();

        if (args.Length < 4)
        {
            logger.LogWarning("Usage: FolderSynchronizer <sourcePath> <destinationPath> <logPath> <intervalInSecounds>");
            return;
        }

        sourcePath = args[0];
        destinationPath = args[1];
        logPath = args[2];
      
        if (!int.TryParse(args[3], out intervalInSecounds)) 
        {
            intervalInSecounds = 5;
        }

        var folderSynchronizer = new FolderSynchronizer(sourcePath, destinationPath, logPath);

        while (true)
        {
            folderSynchronizer.PrepereFiels();
            Thread.Sleep(intervalInSecounds * 1000);
        }
    }
}