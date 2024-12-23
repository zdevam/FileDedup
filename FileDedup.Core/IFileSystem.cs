namespace FileDedup.Core;
public interface IFileSystem
{
    IEnumerable<FileInfo> GetFiles(string directory, bool recursive = false);
    Stream GetFileReadStream(FileInfo fileInfo);

    void DeleteFile(FileInfo fileInfo);
}