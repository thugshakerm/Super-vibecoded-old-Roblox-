namespace Roblox.Application.Deployment;
public sealed record BootstrapperManifestEnvelope(ClientBootstrapManifest Manifest, string SignatureAlgorithm, string SignatureBase64);
