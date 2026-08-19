namespace Roblox.Domain.Identity;
public sealed class UserProfile
{
 public long UserAccountId { get; private set; }
 public DateOnly BirthDate { get; private set; }
 public UserGender Gender { get; private set; }
 public DateTimeOffset CreatedAt { get; private set; }
 private UserProfile() { }
 internal UserProfile(DateOnly birthDate, UserGender gender, DateTimeOffset now) { BirthDate=birthDate; Gender=gender; CreatedAt=now; }
}
