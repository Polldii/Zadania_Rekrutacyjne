namespace FolderSynchronizer.src.Services;

public class LoggerService : ILogger
{
    public void LogError(string message, string directoriePath)
    {
        var errorFilePath = Path.Combine(directoriePath, $"{DateTime.UtcNow.ToString("dd_MM_yyyy_hh_mm_ss")}_error_log.txt");

        File.WriteAllText(errorFilePath, message);
        LogInfo($"Error occurred. More information:  {errorFilePath}  ", directoriePath);
    }

    public void LogInfo(string message, string directoriePath)
    {
        File.AppendAllText(Path.Combine(directoriePath, "log.txt"), $"{DateTime.UtcNow}:  {message} {Environment.NewLine}");
        Console.WriteLine($"{DateTime.UtcNow}:  {message}");
    }

    public void LogWarning(string message)
    {
        Console.WriteLine(DateTime.UtcNow + ": " + message);
    }
}
