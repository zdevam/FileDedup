namespace FileDedup.CLI;
public class OptionParser<T>(List<OptionDefinition<T>> optionsDefinitions) where T : new()
{
    public bool TryParseOptions(string[] args, out T dedupOptions, out List<string> errors)
    {
        errors = [];

        dedupOptions = new T();

        for (int i = 0; i < args.Length; i++)
        {
            var currentOption = args[i];

            if (TryGetoptionDefinition(currentOption, out OptionDefinition<T> optionDefinition)
                && !TrySetOption(optionDefinition, dedupOptions, args, ref i, out var error))
            {
                errors.Add(error);
            }
            else
            {
                errors.Add($"Invalid argument $'{currentOption}'");
                continue;
            }
        }

        return errors.Count == 0;
    }

    private static bool TrySetOption(OptionDefinition<T> optionDefinition, T dedupOptions, string[] args, ref int i, out string error)
    {
        string? arg = default;

        if (optionDefinition is OptionWithParameterDefinition<T> && args.Length < i + 1)
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