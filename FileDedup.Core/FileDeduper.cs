namespace FileDedup.Core;
public class FileDeduper(DuplicateReportGenerator reportGenerator, FileCleaner fileCleaner, IEnumerable<IDuplicateReportWriter> duplicateReportWriters)
{
    public async Task ExecuteAsync(DedupOptions options)
    {
        if (options.Path == null)
        {
            throw new ArgumentNullException(options.Path);
        }

        var report = await reportGenerator.GenerateAsync(options.Path, options.Recursive);

        foreach (var writer in duplicateReportWriters)
        {
            await writer.WriteAsync(report);
        }

        if (options.Clean)
        {
            await fileCleaner.Cleanup(report);
        }
    }
}