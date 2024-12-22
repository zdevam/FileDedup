using System.Collections;

namespace FileDedup.Core;
public sealed class DuplicateReport : IEnumerable<KeyValuePair<Guid, List<FileInfo>>>
{
    private readonly IDictionary<Guid, List<FileInfo>> duplicates = new Dictionary<Guid, List<FileInfo>>();

    public void AddDuplicateList(List<FileInfo> fileInfoList)
    {
        duplicates[Guid.NewGuid()] = fileInfoList;
    }

    public IEnumerator<KeyValuePair<Guid, List<FileInfo>>> GetEnumerator()
    {
        return duplicates.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}