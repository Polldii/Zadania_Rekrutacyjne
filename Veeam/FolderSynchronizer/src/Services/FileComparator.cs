using System.Security.Cryptography;

namespace FolderSynchronizer.src.Services;

public class FileComparator
{
    public bool AreFilesIdentical(string filePath1, string filePath2)
    {
       using(var md5 = MD5.Create())
       {
            using(var stream1 = File.OpenRead(filePath1))
            using(var stream2 = File.OpenRead(filePath2))
            {
                var hash1 = md5.ComputeHash(stream1);
                var hash2 = md5.ComputeHash(stream2);
                return hash1.SequenceEqual(hash2);
            }
        }
    }
}
