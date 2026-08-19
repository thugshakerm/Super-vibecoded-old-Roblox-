namespace Roblox.Domain.Identity;

/// <summary>
/// Identity record only. Public profile content, avatar data, balances, friends, and inventory
/// are separate capabilities and are deliberately absent at this stage.
/// </summary>
public sealed class UserAccount
{
    public long Id { get; private set; }
    public string Username { get; private set; } = string.Empty;
    public string NormalizedUsername { get; private set; } = string.Empty;
    public UserAccountState State { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public PasswordCredential? PasswordCredential { get; private set; }
    public UserProfile? Profile { get; private set; }

    private UserAccount() { }

    public UserAccount(string username, DateTimeOffset now)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("A username is required.", nameof(username));
        }

        Username = username.Trim();
        NormalizedUsername = NormalizeUsername(Username);
        State = UserAccountState.Active;
        CreatedAt = now;
        UpdatedAt = now;
    }

    public void SetPasswordCredential(
        string algorithm,
        int workFactor,
        string salt,
        string hash,
        DateTimeOffset now)
    {
        PasswordCredential = new PasswordCredential(algorithm, workFactor, salt, hash, now);
        UpdatedAt = now;
    }

    public void SetProfile(DateOnly birthDate, UserGender gender, DateTimeOffset now)
    {
        Profile = new UserProfile(birthDate, gender, now);
        UpdatedAt = now;
    }

    public static string NormalizeUsername(string username) => username.Trim().ToUpperInvariant();
}
