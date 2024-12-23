namespace FileDedup.Integration;

internal class FileSystem : IFileSystem, Core.IFileSystem
{
    public void DeleteFile(Core.FileInfo fileInfo)
    {
        File.Delete(fileInfo.FullPath);
    }

    public Stream GetFileReadStream(Core.FileInfo fileInfo)
    {
        return File.OpenRead(fileInfo.FullPath);
    }

    public IEnumerable<Core.FileInfo> GetFiles(string directory, bool recursive = false)
    {
        var enumerationOptions = new EnumerationOptions { RecurseSubdirectories = recursive };

        var directoryInfo = new DirectoryInfo(directory);

        foreach (var file in directoryInfo.EnumerateFiles("*", enumerationOptions))
        {
            yield return new Core.FileInfo(file.FullName, file.Name, file.Length);
        }
    }
}