using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Roblox.Application.Assets;
using Roblox.Infrastructure.Assets;
using Roblox.Infrastructure.Identity;
using Roblox.Application.Identity;
using Roblox.Infrastructure.Persistence;
using Roblox.Infrastructure.Rendering;
using Roblox.Application.Rendering;
using Roblox.Application.Games;
using Roblox.Infrastructure.Games;
using Roblox.Application.Servers;
using Roblox.Infrastructure.Servers;
using Roblox.Application.DataStores;
using Roblox.Infrastructure.DataStores;
using Roblox.Application.Economy;
using Roblox.Infrastructure.Economy;

namespace Roblox.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRobloxInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Roblox")
            ?? throw new InvalidOperationException("Connection string 'Roblox' is required.");

        services.AddDbContext<RobloxDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IAssetDeliveryService, AssetDeliveryService>();
        services.AddSingleton<IAssetUploadPolicy, AssetUploadPolicy>();
        services.AddScoped<IAssetUploadStore, EfAssetUploadStore>();
        services.AddScoped<AssetUploadIntakeService>();
        services.AddSingleton<IPasswordHasher, Pbkdf2PasswordHasher>();
        services.AddSingleton<IUsernamePolicy, LegacyUsernamePolicy>();
        services.AddSingleton<ISessionTokenFactory, OpaqueSessionTokenFactory>();
        services.AddScoped<IUserAccountStore, EfUserAccountStore>();
        services.AddScoped<ISessionStore, EfSessionStore>();
        services.AddScoped<AccountRegistrationService>();
        services.AddScoped<AccountSignInService>();
        services.AddScoped<ISessionAuthenticationService, SessionAuthenticationService>();
        services.AddSingleton<IPrivateObjectStorage, LocalPrivateObjectStorage>();
        services.AddScoped<IRenderJobStore, EfRenderJobStore>();
        services.AddScoped<RenderJobScheduler>();
        services.AddScoped<IUniverseStore, EfUniverseStore>();
        services.AddSingleton<ILaunchTicketFactory, OpaqueLaunchTicketFactory>();
        services.AddScoped<IPrivateServerStore, EfPrivateServerStore>();
        services.AddScoped<ILaunchTicketStore, EfLaunchTicketStore>();
        services.AddSingleton<IVipServerLinkCodeGenerator, VipServerLinkCodeGenerator>();
        services.AddScoped<IPrivateServerLinkStore, EfPrivateServerLinkStore>();
        services.AddScoped<VipServerLinkService>();
        services.AddScoped<IDataStoreStore, EfDataStoreStore>();
        services.AddSingleton<IRapCalculator, RecentAveragePriceCalculator>();
        services.AddSingleton(TimeProvider.System);
        return services;
    }
}
