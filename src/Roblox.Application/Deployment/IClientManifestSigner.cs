namespace Roblox.Application.Deployment;
/// <summary>Signs canonical manifest bytes; the launcher verifies before installing files.</summary>
public interface IClientManifestSigner { byte[] Sign(ReadOnlySpan<byte> canonicalManifest); bool Verify(ReadOnlySpan<byte> canonicalManifest, ReadOnlySpan<byte> signature); }
