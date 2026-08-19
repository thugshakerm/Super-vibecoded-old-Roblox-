using Roblox.Domain.Servers;

namespace Roblox.Application.Servers;

public sealed class VipServerLinkService(
    IVipServerLinkCodeGenerator codeGenerator,
    IPrivateServerLinkStore linkStore,
    TimeProvider timeProvider)
{
    public async Task<string> CreateAsync(
        Guid privateServerId,
        int codeLength,
        DateTimeOffset? expiresAt,
        CancellationToken cancellationToken)
    {
        var now = timeProvider.GetUtcNow();
        var code = codeGenerator.Generate(codeLength);
        var link = new PrivateServerLink(privateServerId, codeGenerator.Hash(code), now, expiresAt);
        await linkStore.AddAsync(link, cancellationToken);
        await linkStore.SaveChangesAsync(cancellationToken);
        return code;
    }
}
