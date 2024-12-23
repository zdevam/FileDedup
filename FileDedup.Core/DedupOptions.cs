namespace FileDedup.Core;
public class DedupOptions
{
    public string? Path { get; set; }

    public bool Clean { get; set; }

    public bool Recursive { get; set; }
}