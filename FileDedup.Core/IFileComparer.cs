namespace FileDedup.Core;
public interface IFileComparer
{
    Task<IList<FileInfo>> AreEqualAsync(FileInfo file, IList<FileInfo> toCompare);
}