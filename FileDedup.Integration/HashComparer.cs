using FileDedup.Core;

namespace FileDedup.Integration;
internal class HashComparer(IFileHasher hasher) : IFileComparer
{
    public async Task<IList<Core.FileInfo>> AreEqualAsync(Core.FileInfo file, IList<Core.FileInfo> toCompare)
    {
        var matchList = new List<Core.FileInfo>();

        var fileHash = await hasher.GetHashAsync(file);

        foreach (var item in toCompare)
        {
            var itemHash = await hasher.GetHashAsync(item);
            if (itemHash == fileHash)
            {
                matchList.Add(item);
            }
        }

        return matchList;
    }
}