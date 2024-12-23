using FileDedup.Core;

namespace FileDedup.CLI;
public class CliTool(OptionParser<DedupOptions> optionsparser, FileDeduper fileDeduper)
{
    public async Task ExecuteAsync(string[] args)
    {
        ArgumentNullException.ThrowIfNull(args);

        if (args.Length == 0)
        {
            PrintHelp();
            return;
        }

        if (!optionsparser.TryParseOptions(args, out var options, out var errors))
        {
            Console.WriteLine("Errors found in options:");
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }

            return;
        }

        await fileDeduper.ExecuteAsync(options);
    }

    private static void PrintHelp()
    {
        Console.WriteLine("*** FileDedup ***");
        Console.WriteLine("Usage: FileDedup.CLI.exe path [pathtodirectory] [recursive] [clean]");
        Console.WriteLine("Path and path to directory must be specified");
        Console.WriteLine("Defaults are: not recursive and no cleaning");
        Console.WriteLine();
        Console.WriteLine("USE AT YOUR OWN RISK!!!");
    }
}