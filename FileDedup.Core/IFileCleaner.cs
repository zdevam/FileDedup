
namespace FileDedup.Core;

internal interface IFileCleaner
{
    Task Cleanup(DuplicateReport duplicateReport);
}