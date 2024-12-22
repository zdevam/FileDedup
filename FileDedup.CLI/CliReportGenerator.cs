using FileDedup.Core;

namespace FileDedup.CLI;
internal class CliReportGenerator : IDuplicateReportWriter
{
    public Task WriteAsync(DuplicateReport duplicateReport)
    {
        if (!duplicateReport.Any())
        {
            Console.WriteLine("No duplicates found!");
            return Task.CompletedTask;
        }

        foreach (var item in duplicateReport)
        {
            Console.WriteLine(item.Key);
            foreach (var file in item.Value)
            {
                Console.WriteLine($"\t{file.FullPath}");
            }

            Console.WriteLine();
        }

        return Task.CompletedTask;
    }
}