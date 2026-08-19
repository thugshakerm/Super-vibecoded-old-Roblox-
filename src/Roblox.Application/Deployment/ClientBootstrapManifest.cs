namespace Roblox.Application.Deployment;

/// <summary>
/// Launcher-facing manifest with hash metadata. Package objects stay private until the launcher
/// receives an authorized short-lived download URL in a later deployment step.
/// </summary>
public sealed record ClientBootstrapManifest(string Channel, string Version, IReadOnlyList<ClientBootstrapFile> Files);
public sealed record ClientBootstrapFile(string FileName, string Sha256, long ContentLength);
