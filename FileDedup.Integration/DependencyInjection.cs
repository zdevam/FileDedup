using FileDedup.Core;
using Microsoft.Extensions.DependencyInjection;

namespace FileDedup.Integration;
public static class DependencyInjection
{
    public static IServiceCollection AddFileSystem(this IServiceCollection services)
    {
        return services
            .AddScoped<Core.IFileSystem, FileSystem>();
    }

    public static IServiceCollection AddHashComparer(this IServiceCollection services)
    {
        return services
            .AddScoped<Integration.IFileSystem, FileSystem>()
            .AddScoped<IFileHasher, FileHasher>()
            .AddScoped<IFileComparer, HashComparer>();
    }
}