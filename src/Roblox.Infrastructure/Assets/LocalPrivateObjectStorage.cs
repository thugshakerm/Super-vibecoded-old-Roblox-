using Microsoft.Extensions.Configuration;
using Roblox.Application.Assets;
using Roblox.Domain.Assets;

namespace Roblox.Infrastructure.Assets;

/// <summary>
/// Local development implementation. Production will replace this with an S3-compatible provider.
/// Keys are resolved beneath one configured root and cannot escape it.
/// </summary>
public sealed class LocalPrivateObjectStorage(IConfiguration configuration) : IPrivateObjectStorage
{
    private readonly string _root = Path.GetFullPath(
        configuration["Storage:RootPath"] ?? Path.Combine(AppContext.BaseDirectory, "private-storage"));

    public async Task PutAsync(ObjectKey key, Stream content, string contentType, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(content);
        var destination = GetPath(key);
        Directory.CreateDirectory(Path.GetDirectoryName(destination)!);
        await using var output = new FileStream(destination, FileMode.Create, FileAccess.Write, FileShare.None, 81920, true);
        await content.CopyToAsync(output, cancellationToken);
    }

    public Task<Stream?> OpenReadAsync(ObjectKey key, CancellationToken cancellationToken)
    {
        var path = GetPath(key);
        Stream? stream = File.Exists(path)
            ? new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 81920, true)
            : null;
        return Task.FromResult(stream);
    }

    public Task<bool> ExistsAsync(ObjectKey key, CancellationToken cancellationToken) =>
        Task.FromResult(File.Exists(GetPath(key)));

    private string GetPath(ObjectKey key)
    {
        var path = Path.GetFullPath(Path.Combine(_root, key.Value.Replace('/', Path.DirectorySeparatorChar)));
        if (!path.StartsWith(_root + Path.DirectorySeparatorChar, StringComparison.Ordinal) &&
            !string.Equals(path, _root, StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Object key resolved outside the configured storage root.");
        }

        return path;
    }
}
