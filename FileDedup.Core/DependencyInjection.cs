using Microsoft.Extensions.DependencyInjection;

namespace FileDedup.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddFileDuper(this IServiceCollection services)
    {
        return services
            .AddScoped<DuplicateReportGenerator>()
            .AddScoped<FileCleaner>()
            .AddScoped<FileDeduper>()
            .AddScoped<IDuplicateReportWriter, TextFileWriter>();
    }
}