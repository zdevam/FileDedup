using FileDedup.Core;
using FileDedup.Integration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileDedup.CLI;

internal class Program
{
    public static async Task Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        var servicecollection = builder.Services;

        servicecollection.AddFileDuper();
        servicecollection.AddFileSystem();
        servicecollection.AddHashComparer();
        servicecollection.AddCliTool();

        var host = builder.Build();

        var sp = host.Services.CreateScope();

        await sp.ServiceProvider.GetRequiredService<CliTool>().ExecuteAsync(args);
    }
}