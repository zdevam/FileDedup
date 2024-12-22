using System.Security.Cryptography;

namespace FileDedup.Integration;
internal class FileHasher(IFileSystem fileSystem) : IFileHasher
{
    private const int BufferSize = 65_536;

    public Task<string> GetHashAsync(Core.FileInfo fileInfo)
    {
        return GetHashAsync(fileInfo, fileInfo.Size);
    }

    private async Task<string> GetHashAsync(Core.FileInfo fileInfo, long maxBytes)
    {
        using var sha256 = SHA256.Create();
        using var stream = fileSystem.GetFileReadStream(fileInfo);

        var buffer = new byte[BufferSize];
        int totalBytesRead = 0;

        while (totalBytesRead < maxBytes)
        {
            int bytesRead = await stream.ReadAsync(buffer.AsMemory(0, BufferSize));
            totalBytesRead += bytesRead;

            sha256.TransformBlock(buffer, 0, bytesRead, null, 0);
        }

        var hash = sha256.TransformFinalBlock(buffer, 0, 0);

        return Convert.ToBase64String(hash);
    }
}