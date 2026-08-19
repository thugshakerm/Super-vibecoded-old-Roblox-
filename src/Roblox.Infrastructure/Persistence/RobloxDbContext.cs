using Microsoft.EntityFrameworkCore;
using Roblox.Domain.Assets;
using Roblox.Domain.Identity;
using Roblox.Domain.Rendering;
using Roblox.Domain.Games;
using Roblox.Domain.Servers;
using Roblox.Domain.DataStores;
using Roblox.Domain.Deployment;
using Roblox.Domain.Economy;

namespace Roblox.Infrastructure.Persistence;

public sealed class RobloxDbContext(DbContextOptions<RobloxDbContext> options) : DbContext(options)
{
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<AssetVersion> AssetVersions => Set<AssetVersion>();
    public DbSet<Thumbnail> Thumbnails => Set<Thumbnail>();
    public DbSet<AssetUpload> AssetUploads => Set<AssetUpload>();
    public DbSet<UserAccount> UserAccounts => Set<UserAccount>();
    public DbSet<PasswordCredential> PasswordCredentials => Set<PasswordCredential>();
    public DbSet<UserSession> UserSessions => Set<UserSession>();
    public DbSet<RenderJob> RenderJobs => Set<RenderJob>();
    public DbSet<Universe> Universes => Set<Universe>();
    public DbSet<Place> Places => Set<Place>();
    public DbSet<PlaceVersion> PlaceVersions => Set<PlaceVersion>();
    public DbSet<PrivateServer> PrivateServers => Set<PrivateServer>();
    public DbSet<GameLaunchTicket> GameLaunchTickets => Set<GameLaunchTicket>();
    public DbSet<DataStoreEntry> DataStoreEntries => Set<DataStoreEntry>();
    public DbSet<PrivateServerLink> PrivateServerLinks => Set<PrivateServerLink>();
    public DbSet<ClientRelease> ClientReleases => Set<ClientRelease>();
    public DbSet<ClientPackage> ClientPackages => Set<ClientPackage>();
    public DbSet<CurrencyTransaction> CurrencyTransactions => Set<CurrencyTransaction>();
    public DbSet<LimitedAssetInstance> LimitedAssetInstances => Set<LimitedAssetInstance>();
    public DbSet<LimitedSale> LimitedSales => Set<LimitedSale>();
    public DbSet<ClientInstallation> ClientInstallations => Set<ClientInstallation>();
    public DbSet<GameServer> GameServers => Set<GameServer>();
    public DbSet<LauncherAuthorization> LauncherAuthorizations => Set<LauncherAuthorization>();
    public DbSet<LaunchAuditEvent> LaunchAuditEvents => Set<LaunchAuditEvent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("platform");

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.ToTable("user_accounts");
            entity.HasKey(user => user.Id);
            entity.Property(user => user.Id).ValueGeneratedOnAdd();
            entity.Property(user => user.Username).HasMaxLength(20).IsRequired();
            entity.Property(user => user.NormalizedUsername).HasMaxLength(20).IsRequired();
            entity.Property(user => user.State).HasConversion<string>().HasMaxLength(32);
            entity.HasIndex(user => user.NormalizedUsername).IsUnique();
            entity.HasOne(user => user.PasswordCredential)
                .WithOne()
                .HasForeignKey<PasswordCredential>(credential => credential.UserAccountId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PasswordCredential>(entity =>
        {
            entity.ToTable("password_credentials");
            entity.HasKey(credential => credential.UserAccountId);
            entity.Property(credential => credential.Algorithm).HasMaxLength(64).IsRequired();
            entity.Property(credential => credential.Salt).HasMaxLength(256).IsRequired();
            entity.Property(credential => credential.Hash).HasMaxLength(256).IsRequired();
        });

        modelBuilder.Entity<UserSession>(entity =>
        {
            entity.ToTable("user_sessions");
            entity.HasKey(session => session.Id);
            entity.Property(session => session.TokenHash).HasMaxLength(64).IsRequired();
            entity.HasIndex(session => session.TokenHash).IsUnique();
            entity.HasIndex(session => new { session.UserAccountId, session.ExpiresAt });
        });

        modelBuilder.Entity<ClientInstallation>(entity =>
        {
            entity.ToTable("client_installations"); entity.HasKey(x => x.Id);
            entity.Property(x => x.Channel).HasMaxLength(64).IsRequired(); entity.Property(x => x.Version).HasMaxLength(128).IsRequired();
            entity.Property(x => x.State).HasConversion<string>().HasMaxLength(32);
            entity.HasIndex(x => new { x.UserAccountId, x.Channel }).IsUnique();
        });
        modelBuilder.Entity<GameServer>(entity =>
        {
            entity.ToTable("game_servers"); entity.HasKey(x => x.Id);
            entity.Property(x => x.State).HasConversion<string>().HasMaxLength(32);
            entity.HasIndex(x => new { x.PlaceId, x.State, x.LeaseExpiresAt });
            entity.HasIndex(x => x.PrivateServerId).IsUnique();
        });
        modelBuilder.Entity<LauncherAuthorization>(entity =>
        {
            entity.ToTable("launcher_authorizations"); entity.HasKey(x => x.Id);
            entity.Property(x => x.TokenHash).HasMaxLength(64).IsRequired();
            entity.HasIndex(x => x.TokenHash).IsUnique(); entity.HasIndex(x => new { x.UserAccountId, x.ExpiresAt });
        });
        modelBuilder.Entity<LaunchAuditEvent>(entity =>
        {
            entity.ToTable("launch_audit_events"); entity.HasKey(x => x.Id);
            entity.Property(x => x.EventCode).HasMaxLength(64).IsRequired(); entity.HasIndex(x => new { x.LaunchTicketId, x.CreatedAt });
        });

        modelBuilder.Entity<ClientRelease>(entity =>
        {
            entity.ToTable("client_releases");
            entity.HasKey(release => release.Id);
            entity.Property(release => release.Channel).HasMaxLength(64).IsRequired();
            entity.Property(release => release.Version).HasMaxLength(128).IsRequired();
            entity.HasIndex(release => new { release.Channel, release.Version }).IsUnique();
            entity.HasMany(release => release.Packages).WithOne().HasForeignKey(package => package.ClientReleaseId);
        });

        modelBuilder.Entity<ClientPackage>(entity =>
        {
            entity.ToTable("client_packages");
            entity.HasKey(package => package.Id);
            entity.Property(package => package.Version).HasMaxLength(128).IsRequired();
            entity.Property(package => package.FileName).HasMaxLength(256).IsRequired();
            entity.Property(package => package.ObjectKey).HasMaxLength(1024).IsRequired();
            entity.Property(package => package.Sha256).HasMaxLength(64).IsRequired();
        });

        modelBuilder.Entity<CurrencyTransaction>(entity =>
        {
            entity.ToTable("currency_transactions");
            entity.HasKey(transaction => transaction.Id);
            entity.Property(transaction => transaction.Currency).HasConversion<string>().HasMaxLength(16);
            entity.Property(transaction => transaction.TransactionType).HasMaxLength(64).IsRequired();
            entity.HasIndex(transaction => new { transaction.UserAccountId, transaction.Currency, transaction.CreatedAt });
            entity.HasIndex(transaction => transaction.CorrelationId);
        });

        modelBuilder.Entity<LimitedAssetInstance>(entity =>
        {
            entity.ToTable("limited_asset_instances");
            entity.HasKey(instance => instance.Id);
            entity.HasIndex(instance => new { instance.AssetId, instance.SerialNumber }).IsUnique();
            entity.HasIndex(instance => new { instance.OwnerUserId, instance.IsForSale });
        });

        modelBuilder.Entity<LimitedSale>(entity =>
        {
            entity.ToTable("limited_sales");
            entity.HasKey(sale => sale.Id);
            entity.HasIndex(sale => new { sale.AssetId, sale.SoldAt });
            entity.HasIndex(sale => sale.LimitedAssetInstanceId);
        });

        modelBuilder.Entity<PrivateServerLink>(entity =>
        {
            entity.ToTable("private_server_links");
            entity.HasKey(link => link.Id);
            entity.Property(link => link.CodeHash).HasMaxLength(64).IsRequired();
            entity.HasIndex(link => link.CodeHash).IsUnique();
            entity.HasIndex(link => new { link.PrivateServerId, link.RevokedAt });
        });

        modelBuilder.Entity<PrivateServer>(entity =>
        {
            entity.ToTable("private_servers");
            entity.HasKey(server => server.Id);
            entity.Property(server => server.Name).HasMaxLength(256).IsRequired();
            entity.Property(server => server.State).HasConversion<string>().HasMaxLength(32);
            entity.HasIndex(server => new { server.UniverseId, server.State });
            entity.HasIndex(server => new { server.OwnerUserId, server.CreatedAt });
        });

        modelBuilder.Entity<GameLaunchTicket>(entity =>
        {
            entity.ToTable("game_launch_tickets");
            entity.HasKey(ticket => ticket.Id);
            entity.Property(ticket => ticket.NonceHash).HasMaxLength(64).IsRequired();
            entity.HasIndex(ticket => ticket.NonceHash).IsUnique();
            entity.HasIndex(ticket => new { ticket.UserAccountId, ticket.ExpiresAt });
        });

        modelBuilder.Entity<DataStoreEntry>(entity =>
        {
            entity.ToTable("datastore_entries");
            entity.HasKey(entry => entry.Id);
            entity.Property(entry => entry.StoreName).HasMaxLength(128).IsRequired();
            entity.Property(entry => entry.EntryKey).HasMaxLength(512).IsRequired();
            entity.Property(entry => entry.JsonValue).HasColumnType("jsonb").IsRequired();
            entity.HasIndex(entry => new { entry.UniverseId, entry.StoreName, entry.EntryKey }).IsUnique();
        });

        modelBuilder.Entity<Universe>(entity =>
        {
            entity.ToTable("universes");
            entity.HasKey(universe => universe.Id);
            entity.Property(universe => universe.Id).ValueGeneratedOnAdd();
            entity.Property(universe => universe.Name).HasMaxLength(256).IsRequired();
            entity.Property(universe => universe.Description).HasMaxLength(4096);
            entity.HasIndex(universe => new { universe.CreatorUserId, universe.CreatedAt });
            entity.HasMany(universe => universe.Places).WithOne().HasForeignKey(place => place.UniverseId);
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.ToTable("places");
            entity.HasKey(place => place.Id);
            entity.Property(place => place.Id).ValueGeneratedOnAdd();
            entity.Property(place => place.Name).HasMaxLength(256).IsRequired();
            entity.Property(place => place.AccessType).HasConversion<string>().HasMaxLength(32);
            // PostgreSQL partial unique index: one root place per universe, unlimited non-root places.
            entity.HasIndex(place => place.UniverseId)
                .HasFilter("\"IsRootPlace\" = TRUE")
                .IsUnique();
            entity.HasMany(place => place.Versions).WithOne().HasForeignKey(version => version.PlaceId);
        });

        modelBuilder.Entity<PlaceVersion>(entity =>
        {
            entity.ToTable("place_versions");
            entity.HasKey(version => version.Id);
            entity.Property(version => version.ObjectKey).HasMaxLength(1024).IsRequired();
            entity.Property(version => version.Sha256).HasMaxLength(64).IsRequired();
            entity.HasIndex(version => new { version.PlaceId, version.VersionNumber }).IsUnique();
        });

        modelBuilder.Entity<RenderJob>(entity =>
        {
            entity.ToTable("render_jobs");
            entity.HasKey(job => job.Id);
            entity.Property(job => job.Kind).HasConversion<string>().HasMaxLength(32);
            entity.Property(job => job.State).HasConversion<string>().HasMaxLength(32);
            entity.Property(job => job.FailureCode).HasMaxLength(128);
            entity.Property(job => job.OutputObjectKey).HasMaxLength(1024);
            entity.HasIndex(job => new { job.State, job.RequestedAt });
            entity.HasIndex(job => new { job.Kind, job.TargetId, job.AppearanceOrAssetVersion });
        });

        modelBuilder.Entity<AssetUpload>(entity =>
        {
            entity.ToTable("asset_uploads"); entity.HasKey(x => x.Id);
            entity.Property(x => x.AssetType).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.State).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.OriginalFileName).HasMaxLength(256).IsRequired();
            entity.Property(x => x.ExpectedContentType).HasMaxLength(128).IsRequired();
            entity.Property(x => x.PrivateObjectKey).HasMaxLength(1024).IsRequired();
            entity.Property(x => x.FailureCode).HasMaxLength(128);
            entity.HasIndex(x => new { x.CreatorUserId, x.State, x.ExpiresAt });
        });

        modelBuilder.Entity<Asset>(entity =>
        {
            entity.ToTable("assets");
            entity.HasKey(asset => asset.Id);
            entity.Property(asset => asset.Id).ValueGeneratedNever();
            entity.Property(asset => asset.Type).HasConversion<string>().HasMaxLength(32);
            entity.Property(asset => asset.Name).HasMaxLength(256).IsRequired();
            entity.Property(asset => asset.ModerationState).HasConversion<string>().HasMaxLength(32);
            entity.HasIndex(asset => new { asset.CreatorUserId, asset.CreatedAt });
            entity.HasIndex(asset => new { asset.ModerationState, asset.IsPublic });
            entity.HasMany(asset => asset.Versions).WithOne().HasForeignKey(version => version.AssetId);
        });

        modelBuilder.Entity<AssetVersion>(entity =>
        {
            entity.ToTable("asset_versions");
            entity.HasKey(version => version.Id);
            entity.Property(version => version.ObjectKey).HasMaxLength(1024).IsRequired();
            entity.Property(version => version.ContentType).HasMaxLength(255).IsRequired();
            entity.Property(version => version.Sha256).HasMaxLength(64).IsRequired();
            entity.HasIndex(version => new { version.AssetId, version.VersionNumber }).IsUnique();
        });

        modelBuilder.Entity<Thumbnail>(entity =>
        {
            entity.ToTable("thumbnails");
            entity.HasKey(thumbnail => thumbnail.Id);
            entity.Property(thumbnail => thumbnail.State).HasConversion<string>().HasMaxLength(32);
            entity.Property(thumbnail => thumbnail.ErrorCode).HasConversion<string>().HasMaxLength(64);
            entity.Property(thumbnail => thumbnail.ObjectKey).HasMaxLength(1024);
            entity.HasIndex(thumbnail => new { thumbnail.AssetId, thumbnail.Width, thumbnail.Height }).IsUnique();
        });
    }
}
