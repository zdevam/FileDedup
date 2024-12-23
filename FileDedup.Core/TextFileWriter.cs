namespace FileDedup.Core;
internal class TextFileWriter : IDuplicateReportWriter
{
    public async Task WriteAsync(DuplicateReport duplicateReport)
    {
        using var writer = new StreamWriter($"duplicateReport_{DateTime.UtcNow:yyyyMMddTHHmmss}.txt");

        foreach (var item in duplicateReport)
        {
            await writer.WriteLineAsync(item.Key.ToString());
            foreach (var file in item.Value)
            {
                await writer.WriteLineAsync($"\t{file.FullPath}");
            }

            await writer.WriteLineAsync();
        }
    }
}