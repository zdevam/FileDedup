namespace FileDedup.Integration;
public interface IFileSystem
{
    Stream GetFileReadStream(Core.FileInfo fileInfo);
}