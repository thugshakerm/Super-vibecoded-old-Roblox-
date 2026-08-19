namespace Roblox.Application.Rendering;

public sealed record RccRenderResult(bool Succeeded, Stream? PngContent, string? FailureCode);
