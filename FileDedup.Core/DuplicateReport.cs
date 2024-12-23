using System.Collections;

namespace FileDedup.Core;
public sealed class DuplicateReport : IEnumerable<KeyValuePair<Guid, List<FileInfo>>>
{
    private readonly Dictionary<Guid, List<FileInfo>> duplicates = [];

    public void AddDuplicateList(IEnumerable<FileInfo> fileInfos)
    {
        duplicates[Guid.NewGuid()] = fileInfos.ToList();
    }

    public IEnumerator<KeyValuePair<Guid, List<FileInfo>>> GetEnumerator()
    {
        foreach (var item in duplicates)
        {
            yield return KeyValuePair.Create(item.Key, item.Value.ToList());
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}