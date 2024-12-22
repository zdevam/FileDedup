namespace FileDedup.Integration;

internal interface IFileHasher
{
    Task<string> GetHashAsync(Core.FileInfo fileInfo);
}