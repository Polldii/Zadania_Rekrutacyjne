using FolderSynchronizer.src.Services;
using System.Collections;

namespace FolderSynchronizer.src;

public class FolderSynchronizer
{
    const string defaultSourcePath = "C:\\Source";
    const string defaultDestinationPath = "C:\\Replica";
    const string defaultLogPath = "C:\\Folder_Synchronizer_Logs";

    private string _sourcePath;
    private string _destinationPath;
    private string _logPath;

    private FileComparator _fileComparator = new();
    private LoggerService _logger = new();

    public IList sourceFiles;
    public IList sourceDirectories;
    public IList destinationFiles;
    public IList destinationDirectories;

    public string SourcePath
    {
        get { return _sourcePath; }
        set 
        {
            if (string.IsNullOrEmpty(value))
                _sourcePath = defaultSourcePath;
            else
                _sourcePath = value;
        }
    }
    public string DestinationPath
    {
        get { return _destinationPath; }
        set
        {
            if (string.IsNullOrEmpty(value))
                _destinationPath = defaultDestinationPath;
            else
                _destinationPath = value;
        }
    }
    public string LogPath
    {
        get { return _logPath; }
        set
        {
            if (string.IsNullOrEmpty(value))
                _logPath = defaultLogPath;
            else
                _logPath = value;
        }
    }


    public FolderSynchronizer(string sourcePath, string destinationPath, string logFilePath)
    {
        SourcePath = sourcePath;
        DestinationPath = destinationPath;
        LogPath = logFilePath;
    }


    public void PrepereFiels()
    {
        _logger.LogWarning("Preparing files for synchronization...");

        if (!Directory.Exists(LogPath))
        {
            Directory.CreateDirectory(LogPath);
            _logger.LogInfo($"Log directory does not exist. Creating: {LogPath}", LogPath);
        }

        if (!Directory.Exists(SourcePath))
        {
            _logger.LogError($"Source directory does not exist: {SourcePath}", LogPath);
            return;
        }

        if (!Directory.Exists(DestinationPath)) 
        { 
            _logger.LogInfo($"Destination directory does not exist. Creating: {DestinationPath}", LogPath);
            Directory.CreateDirectory(DestinationPath);
        }

        _logger.LogInfo($"{Environment.NewLine} Source path: {SourcePath} {Environment.NewLine} Destination path: {DestinationPath} {Environment.NewLine} Log path: {LogPath} {Environment.NewLine}", LogPath);

        sourceFiles = Directory.GetFiles(SourcePath, "*", SearchOption.AllDirectories);
        sourceDirectories = Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories);
        destinationFiles = Directory.GetFiles(DestinationPath, "*", SearchOption.AllDirectories);
        destinationDirectories = Directory.GetDirectories(DestinationPath, "*", SearchOption.AllDirectories);

        StartSynchronization();
    }

    public void StartSynchronization()
    {
        _logger.LogInfo($"{Environment.NewLine}****************************{Environment.NewLine}Starting folder synchronization...", LogPath);

        // Copy and create directories from source to destination
        foreach (string directorie in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
        {
            string relativePath = Path.GetRelativePath(SourcePath, directorie);
            string destinationDirectoriePath = Path.Combine(DestinationPath, relativePath);
            if (!Directory.Exists(destinationDirectoriePath))
            {
                CoppyDirectorie(destinationDirectoriePath);
            }
        }

        // Copy and update files from source to destination
        foreach (string file in sourceFiles)
        {
            string relativePath = Path.GetRelativePath(SourcePath, file);
            string destinationFilePath = Path.Combine(DestinationPath, relativePath);

            if (!File.Exists(destinationFilePath))
            {
                CoppyFile(file, destinationFilePath, null);
            }
            else if (!_fileComparator.AreFilesIdentical(file, destinationFilePath))
            {
                UpdateFile(file, destinationFilePath);
            }
        }

        if (destinationFiles.Count != 0)
        {
            // Delete files from destination that are not in source
            foreach (string file in destinationFiles)
            {
                string relativePath = Path.GetRelativePath(DestinationPath, file);
                string sourceFilePath = Path.Combine(SourcePath, relativePath);
                if (!File.Exists(sourceFilePath))
                {
                    DeleteFile(file);
                }
            }
        }

        if (destinationDirectories.Count != 0)
        {
            // Delete empty directories in destination
            foreach (string directorie in destinationDirectories)
            {
                string relativePath = Path.GetRelativePath(DestinationPath, directorie);
                string sourceDirectoriePath = Path.Combine(SourcePath, relativePath);

                if (!Directory.Exists(sourceDirectoriePath) && Directory.GetFiles(directorie).Length == 0 && Directory.GetDirectories(directorie).Length == 0)
                {
                    DeleteDirectorie(directorie);
                }
            }
        }

        _logger.LogInfo($"Folder synchronization completed successfully.{Environment.NewLine}****************************{Environment.NewLine}", LogPath);
    }

    public void CoppyFile(string sourcefile,string destinationPath, string? message)
    {
        _logger.LogInfo(message != null? message : $"Copying file: {destinationPath}", LogPath);

        File.Copy(sourcefile, destinationPath, true);
    }

    public void CoppyDirectorie(string destinationPath)
    {
        _logger.LogInfo($"Copying directory: {destinationPath}", LogPath);
        Directory.CreateDirectory(destinationPath);
    }

    public void UpdateFile(string sourcefile, string destinationPath)
    {
        CoppyFile(sourcefile, destinationPath, $"Updating file: {destinationPath}");
    }

    public void DeleteFile(string file) 
    { 
        _logger.LogInfo($"Deleting file: {file}", LogPath);

        File.Delete(file);
    }

    public void DeleteDirectorie(string directorie)
    {
        _logger.LogInfo($"Deleting directory: {directorie}", LogPath);

        Directory.Delete(directorie, true);
    }

}
