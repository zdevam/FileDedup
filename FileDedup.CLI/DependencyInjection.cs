using FileDedup.Core;
using Microsoft.Extensions.DependencyInjection;

namespace FileDedup.CLI;
internal static class DependencyInjection
{
    public static IServiceCollection AddCliTool(this IServiceCollection servicecollection)
    {
        return servicecollection
            .AddScoped<CliTool>()
            .AddTransient<IDuplicateReportWriter, CliReportGenerator>()
            .AddSingleton(OptionDefinitions.Definitions)
            .AddSingleton(typeof(OptionParser<>));
    }
}