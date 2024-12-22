using FileDedup.Core;

namespace FileDedup.CLI;
internal static class OptionDefinitions
{
    public static readonly List<OptionDefinition<DedupOptions>> Definitions = [Path, Recursive, Clean];

    public static readonly OptionDefinition<DedupOptions> Recursive = new("recursive", "r", (opt, arg) => opt.Recursive = true);
    public static readonly OptionDefinition<DedupOptions> Clean = new("clean", "c", (opt, arg) => opt.Clean = true);

    public static readonly OptionWithParameterDefinition<DedupOptions> Path = new(
        "path",
        "p",
        (opt, arg) => opt.Path = arg,
        arg => Directory.Exists(arg),
        true);
}