using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<RccServiceHost>();
await builder.Build().RunAsync();

// This is an internal C# adapter host, not the legacy RCCService executable.
// It contains no RCC binary and exposes no public network listener.
internal sealed class RccServiceHost : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;
}
