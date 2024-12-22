namespace FileDedup.CLI;
public class OptionDefinition<T>(string longname, string shortname, Action<T, string?> setoption, bool isrequired = false)
{
    private readonly Action<T, string?> setOption = setoption;

    private bool isSet = false;
    public string LongName { get; } = longname;
    public string ShortName { get; } = shortname;
    public bool IsRequired { get; } = isrequired;
    public bool TrySetOption(T options, string? arg, out string error)
    {
        if (!Validate(options, arg, out error))
        {
            return false;
        }

        isSet = true;
        setOption(options, arg);

        return true;
    }

    protected virtual bool Validate(T options, string? arg, out string error)
    {
        error = string.Empty;

        if (isSet)
        {
            error = $"Argument '{LongName}/{ShortName}' was provided more than once";
            return false;
        }

        return true;
    }

    public static OptionDefinition<T> Empty => new(string.Empty, string.Empty, (opt, arg) => { });
}