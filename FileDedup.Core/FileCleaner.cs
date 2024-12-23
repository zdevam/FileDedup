namespace FileDedup.Core;
public sealed class FileCleaner(IFileSystem fileSystem)
{
    public Task Cleanup(DuplicateReport duplicateReport)
    {
        foreach (var list in duplicateReport.Select(x => x.Value))
        {
            foreach (var item in list.Skip(1))
            {
                fileSystem.DeleteFile(item);
            }
        }
        return Task.CompletedTask;
    }
}