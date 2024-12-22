
namespace FileDedup.CLI;
internal class OptionWithParameterDefinition<T>(string longname, string shortname, Action<T, string?> setoption, Func<string, bool> validateargument, bool isrequired = false)
    : OptionDefinition<T>(longname, shortname, setoption, isrequired) where T : new()
{
    private readonly Func<string, bool> validateArgument = validateargument;

    protected override bool Validate(T options, string? arg, out string error)
    {
        if (!base.Validate(options, arg, out error))
        {
            return false;
        };

        if (arg == null || !validateArgument(arg))
        {
            error = $"Argument for '{LongName}/{ShortName}' is invalid";
            return false;
        }

        return true;
    }
}