using FileDedup.Core;

namespace FileDedup.CLI;
public class CliTool(OptionParser<DedupOptions> optionsparser, FileDeduper fileDeduper)
{
    public async Task ExecuteAsync(string[] args)
    {
        ArgumentNullException.ThrowIfNull(args);

        if (args.Length == 0)
        {
            Console.WriteLine(Help);
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
    private static string Help => @"
*** FileDedup ***
Usage: FileDedup.CLI.exe path [pathtodirectory] [recursive] [clean]
Path and path to directory must be specified
Defaults are: not recursive and no cleaning
    
USE AT YOUR OWN RISK!!!
";
}