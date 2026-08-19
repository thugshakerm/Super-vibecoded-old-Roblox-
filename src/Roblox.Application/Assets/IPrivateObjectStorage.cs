using Roblox.Domain.Assets;

namespace Roblox.Application.Assets;

/// <summary>Internal object storage only. This contract intentionally has no public URL method.</summary>
public interface IPrivateObjectStorage
{
    Task PutAsync(ObjectKey key, Stream content, string contentType, CancellationToken cancellationToken);
    Task<Stream?> OpenReadAsync(ObjectKey key, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(ObjectKey key, CancellationToken cancellationToken);
}
