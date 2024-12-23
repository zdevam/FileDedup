namespace FileDedup.Core;
public interface IDuplicateReportWriter
{
    Task WriteAsync(DuplicateReport duplicateReport);
}