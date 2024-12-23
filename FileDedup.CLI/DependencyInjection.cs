using FileDedup.Core;
using Microsoft.Extensions.DependencyInjection;

namespace FileDedup.CLI;
internal static class DependencyInjection
{
    public static IServiceCollection AddCliTool(this IServiceCollection servicecollection)
    {
        var list = OptionDefinitions.Definitions;

        return servicecollection
            .AddScoped<CliTool>()
            .AddTransient<IDuplicateReportWriter, CliReportGenerator>()
            .AddSingleton(list)
            .AddSingleton(typeof(OptionParser<>));
    }
}