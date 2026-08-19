namespace Roblox.Domain.Assets;

/// <summary>
/// A private object-storage identifier, never a filesystem path or browser-facing URL.
/// </summary>
public sealed record ObjectKey
{
    public string Value { get; }

    private ObjectKey(string value) => Value = value;

    public static ObjectKey Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) ||
            value.StartsWith('/') ||
            value.Contains("..", StringComparison.Ordinal) ||
            value.Contains('\\'))
        {
            throw new ArgumentException("Invalid object storage key.", nameof(value));
        }

        return new ObjectKey(value);
    }

    public override string ToString() => Value;
}
