namespace FolderSynchronizer.src.Services;

public interface ILogger
{
    void LogError(string message, string directoriePath);
    void LogInfo(string message, string directoriePath);
    void LogWarning(string message);
}
