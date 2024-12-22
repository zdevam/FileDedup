namespace FileDedup.Core;
public sealed class DuplicateReportGenerator(IFileSystem fileSystem, IFileComparer fileComparer)
{
    private readonly IDictionary<long, List<FileInfo>> _fileDictionary = new Dictionary<long, List<FileInfo>>();
    private readonly DuplicateReport _duplicateReport = new();

    public async Task<DuplicateReport> GenerateAsync(string directory, bool recursive)
    {
        foreach (var file in fileSystem.GetFiles(directory, recursive))
        {
            if (!_fileDictionary.TryGetValue(file.Size, out List<FileInfo>? value))
            {
                value = ([]);
                _fileDictionary[file.Size] = value;
            }

            value.Add(file);
        }

        foreach (var fileList in _fileDictionary.Values.Where(x => x.Count > 1))
        {
            await CheckForDupes(fileList);
        }

        return _duplicateReport;
    }

    private async Task CheckForDupes(List<FileInfo> fileList)
    {
        while (fileList.Count > 1)
        {
            var current = fileList[0];
            fileList.Remove(current);

            List<FileInfo> duplicatesList = [current];

            var detectedDuplicates = await fileComparer.AreEqualAsync(current, fileList);

            foreach (var duplicate in detectedDuplicates)
            {
                fileList.Remove(duplicate);
            }

            if (duplicatesList.Count > 1)
            {
                _duplicateReport.AddDuplicateList(duplicatesList);
            }
        }
    }
}