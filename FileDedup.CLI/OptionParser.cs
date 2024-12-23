namespace FileDedup.CLI;
public class OptionParser<T>(IEnumerable<OptionDefinition<T>> optionsDefinitions) where T : new()
{
    public bool TryParseOptions(string[] args, out T dedupOptions, out IReadOnlyList<string> errors)
    {
        ArgumentNullException.ThrowIfNull(args);

        var founderrors = new List<string>();

        dedupOptions = new T();

        for (int i = 0; i < args.Length; i++)
        {
            var currentOption = args[i];

            if (!TryGetoptionDefinition(currentOption, out OptionDefinition<T> optionDefinition))
            {
                founderrors.Add($"Invalid argument $'{currentOption}'");
                continue;
            }

            if (!TrySetOption(optionDefinition, dedupOptions, args, ref i, out var error))
            {
                founderrors.Add(error);
            }
        }

        errors = founderrors;

        return errors.Count == 0;
    }

    private static bool TrySetOption(OptionDefinition<T> optionDefinition, T dedupOptions, string[] args, ref int i, out string error)
    {
        string? arg = default;

        if (optionDefinition is OptionWithParameterDefinition<T> && args.Length < i + 2)
        {
            error = $"No argument provided for {optionDefinition.LongName}/{optionDefinition.ShortName}";
            return false;
        }
        else if (optionDefinition is OptionWithParameterDefinition<T>)
        {
            i++;
            arg = args[i];
        }

        return optionDefinition.TrySetOption(dedupOptions, arg, out error);
    }

    private bool TryGetoptionDefinition(string arg, out OptionDefinition<T> optionDefinition)
    {
        var definition = optionsDefinitions.SingleOrDefault(x => x.LongName == arg || x.ShortName == arg);

        if (definition == null)
        {
            optionDefinition = OptionDefinition<T>.Empty;
            return false;
        }

        optionDefinition = definition;

        return true;
    }
}