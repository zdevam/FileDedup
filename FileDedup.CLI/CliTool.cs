using FileDedup.Core;

namespace FileDedup.CLI;
public class CliTool(OptionParser<DedupOptions> optionsparser, FileDeduper fileDeduper)
{
    public async Task ExecuteAsync(string[] args)
    {
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
        }

        await fileDeduper.ExecuteAsync(options);
    }

    private static void PrintHelp()
    {
        Console.WriteLine("HALLUP!");
    }
}