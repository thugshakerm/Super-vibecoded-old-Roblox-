using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Roblox.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialPlatform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "platform");

            migrationBuilder.CreateTable(
                name: "asset_uploads",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: false),
                    AssetType = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    State = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    OriginalFileName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ExpectedContentType = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    MaximumContentLength = table.Column<long>(type: "bigint", nullable: false),
                    PrivateObjectKey = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UploadedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    FailureCode = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset_uploads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "assets",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ModerationState = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "client_installations",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserAccountId = table.Column<long>(type: "bigint", nullable: false),
                    Channel = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Version = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    State = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_installations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "client_releases",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Channel = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Version = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_releases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "currency_transactions",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserAccountId = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    TransactionType = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currency_transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "datastore_entries",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UniverseId = table.Column<long>(type: "bigint", nullable: false),
                    StoreName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    EntryKey = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    JsonValue = table.Column<string>(type: "jsonb", nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datastore_entries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "game_launch_tickets",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserAccountId = table.Column<long>(type: "bigint", nullable: false),
                    PlaceId = table.Column<long>(type: "bigint", nullable: false),
                    PrivateServerId = table.Column<Guid>(type: "uuid", nullable: true),
                    NonceHash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    IssuedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ConsumedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_launch_tickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "game_servers",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlaceId = table.Column<long>(type: "bigint", nullable: false),
                    PrivateServerId = table.Column<Guid>(type: "uuid", nullable: true),
                    State = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    PlayerCount = table.Column<int>(type: "integer", nullable: false),
                    MaxPlayers = table.Column<int>(type: "integer", nullable: false),
                    LeaseExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_servers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "launch_audit_events",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LaunchTicketId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_launch_audit_events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "launcher_authorizations",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserAccountId = table.Column<long>(type: "bigint", nullable: false),
                    LaunchTicketId = table.Column<Guid>(type: "uuid", nullable: false),
                    TokenHash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    RedeemedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_launcher_authorizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "limited_asset_instances",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    OwnerUserId = table.Column<long>(type: "bigint", nullable: false),
                    SerialNumber = table.Column<int>(type: "integer", nullable: false),
                    IsForSale = table.Column<bool>(type: "boolean", nullable: false),
                    AskingPrice = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_limited_asset_instances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "limited_sales",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    LimitedAssetInstanceId = table.Column<long>(type: "bigint", nullable: false),
                    SellerUserId = table.Column<long>(type: "bigint", nullable: false),
                    BuyerUserId = table.Column<long>(type: "bigint", nullable: false),
                    SalePrice = table.Column<long>(type: "bigint", nullable: false),
                    SoldAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_limited_sales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "private_server_links",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PrivateServerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CodeHash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    RevokedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_private_server_links", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "private_servers",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UniverseId = table.Column<long>(type: "bigint", nullable: false),
                    OwnerUserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    State = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_private_servers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "render_jobs",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Kind = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    TargetId = table.Column<long>(type: "bigint", nullable: false),
                    AppearanceOrAssetVersion = table.Column<long>(type: "bigint", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    RequestedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LeaseExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    FailureCode = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    OutputObjectKey = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_render_jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "thumbnails",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    State = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ErrorCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ObjectKey = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thumbnails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "universes",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_universes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_accounts",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    NormalizedUsername = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_sessions",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserAccountId = table.Column<long>(type: "bigint", nullable: false),
                    TokenHash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    RevokedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "asset_versions",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    VersionNumber = table.Column<int>(type: "integer", nullable: false),
                    ObjectKey = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    ContentType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ContentLength = table.Column<long>(type: "bigint", nullable: false),
                    Sha256 = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset_versions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_asset_versions_assets_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "platform",
                        principalTable: "assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_packages",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientReleaseId = table.Column<long>(type: "bigint", nullable: false),
                    Version = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    FileName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ObjectKey = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    Sha256 = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ContentLength = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_packages_client_releases_ClientReleaseId",
                        column: x => x.ClientReleaseId,
                        principalSchema: "platform",
                        principalTable: "client_releases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "places",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UniverseId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    AccessType = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    IsRootPlace = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_places", x => x.Id);
                    table.ForeignKey(
                        name: "FK_places_universes_UniverseId",
                        column: x => x.UniverseId,
                        principalSchema: "platform",
                        principalTable: "universes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "password_credentials",
                schema: "platform",
                columns: table => new
                {
                    UserAccountId = table.Column<long>(type: "bigint", nullable: false),
                    Algorithm = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    WorkFactor = table.Column<int>(type: "integer", nullable: false),
                    Salt = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Hash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_password_credentials", x => x.UserAccountId);
                    table.ForeignKey(
                        name: "FK_password_credentials_user_accounts_UserAccountId",
                        column: x => x.UserAccountId,
                        principalSchema: "platform",
                        principalTable: "user_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "place_versions",
                schema: "platform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlaceId = table.Column<long>(type: "bigint", nullable: false),
                    VersionNumber = table.Column<int>(type: "integer", nullable: false),
                    ObjectKey = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    Sha256 = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ContentLength = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_place_versions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_place_versions_places_PlaceId",
                        column: x => x.PlaceId,
                        principalSchema: "platform",
                        principalTable: "places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_asset_uploads_CreatorUserId_State_ExpiresAt",
                schema: "platform",
                table: "asset_uploads",
                columns: new[] { "CreatorUserId", "State", "ExpiresAt" });

            migrationBuilder.CreateIndex(
                name: "IX_asset_versions_AssetId_VersionNumber",
                schema: "platform",
                table: "asset_versions",
                columns: new[] { "AssetId", "VersionNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assets_CreatorUserId_CreatedAt",
                schema: "platform",
                table: "assets",
                columns: new[] { "CreatorUserId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_assets_ModerationState_IsPublic",
                schema: "platform",
                table: "assets",
                columns: new[] { "ModerationState", "IsPublic" });

            migrationBuilder.CreateIndex(
                name: "IX_client_installations_UserAccountId_Channel",
                schema: "platform",
                table: "client_installations",
                columns: new[] { "UserAccountId", "Channel" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_packages_ClientReleaseId",
                schema: "platform",
                table: "client_packages",
                column: "ClientReleaseId");

            migrationBuilder.CreateIndex(
                name: "IX_client_releases_Channel_Version",
                schema: "platform",
                table: "client_releases",
                columns: new[] { "Channel", "Version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_currency_transactions_CorrelationId",
                schema: "platform",
                table: "currency_transactions",
                column: "CorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_currency_transactions_UserAccountId_Currency_CreatedAt",
                schema: "platform",
                table: "currency_transactions",
                columns: new[] { "UserAccountId", "Currency", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_datastore_entries_UniverseId_StoreName_EntryKey",
                schema: "platform",
                table: "datastore_entries",
                columns: new[] { "UniverseId", "StoreName", "EntryKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_game_launch_tickets_NonceHash",
                schema: "platform",
                table: "game_launch_tickets",
                column: "NonceHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_game_launch_tickets_UserAccountId_ExpiresAt",
                schema: "platform",
                table: "game_launch_tickets",
                columns: new[] { "UserAccountId", "ExpiresAt" });

            migrationBuilder.CreateIndex(
                name: "IX_game_servers_PlaceId_State_LeaseExpiresAt",
                schema: "platform",
                table: "game_servers",
                columns: new[] { "PlaceId", "State", "LeaseExpiresAt" });

            migrationBuilder.CreateIndex(
                name: "IX_game_servers_PrivateServerId",
                schema: "platform",
                table: "game_servers",
                column: "PrivateServerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_launch_audit_events_LaunchTicketId_CreatedAt",
                schema: "platform",
                table: "launch_audit_events",
                columns: new[] { "LaunchTicketId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_launcher_authorizations_TokenHash",
                schema: "platform",
                table: "launcher_authorizations",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_launcher_authorizations_UserAccountId_ExpiresAt",
                schema: "platform",
                table: "launcher_authorizations",
                columns: new[] { "UserAccountId", "ExpiresAt" });

            migrationBuilder.CreateIndex(
                name: "IX_limited_asset_instances_AssetId_SerialNumber",
                schema: "platform",
                table: "limited_asset_instances",
                columns: new[] { "AssetId", "SerialNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_limited_asset_instances_OwnerUserId_IsForSale",
                schema: "platform",
                table: "limited_asset_instances",
                columns: new[] { "OwnerUserId", "IsForSale" });

            migrationBuilder.CreateIndex(
                name: "IX_limited_sales_AssetId_SoldAt",
                schema: "platform",
                table: "limited_sales",
                columns: new[] { "AssetId", "SoldAt" });

            migrationBuilder.CreateIndex(
                name: "IX_limited_sales_LimitedAssetInstanceId",
                schema: "platform",
                table: "limited_sales",
                column: "LimitedAssetInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_place_versions_PlaceId_VersionNumber",
                schema: "platform",
                table: "place_versions",
                columns: new[] { "PlaceId", "VersionNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_places_UniverseId",
                schema: "platform",
                table: "places",
                column: "UniverseId",
                unique: true,
                filter: "\"IsRootPlace\" = TRUE");

            migrationBuilder.CreateIndex(
                name: "IX_private_server_links_CodeHash",
                schema: "platform",
                table: "private_server_links",
                column: "CodeHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_private_server_links_PrivateServerId_RevokedAt",
                schema: "platform",
                table: "private_server_links",
                columns: new[] { "PrivateServerId", "RevokedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_private_servers_OwnerUserId_CreatedAt",
                schema: "platform",
                table: "private_servers",
                columns: new[] { "OwnerUserId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_private_servers_UniverseId_State",
                schema: "platform",
                table: "private_servers",
                columns: new[] { "UniverseId", "State" });

            migrationBuilder.CreateIndex(
                name: "IX_render_jobs_Kind_TargetId_AppearanceOrAssetVersion",
                schema: "platform",
                table: "render_jobs",
                columns: new[] { "Kind", "TargetId", "AppearanceOrAssetVersion" });

            migrationBuilder.CreateIndex(
                name: "IX_render_jobs_State_RequestedAt",
                schema: "platform",
                table: "render_jobs",
                columns: new[] { "State", "RequestedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_thumbnails_AssetId_Width_Height",
                schema: "platform",
                table: "thumbnails",
                columns: new[] { "AssetId", "Width", "Height" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_universes_CreatorUserId_CreatedAt",
                schema: "platform",
                table: "universes",
                columns: new[] { "CreatorUserId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_user_accounts_NormalizedUsername",
                schema: "platform",
                table: "user_accounts",
                column: "NormalizedUsername",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_sessions_TokenHash",
                schema: "platform",
                table: "user_sessions",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_sessions_UserAccountId_ExpiresAt",
                schema: "platform",
                table: "user_sessions",
                columns: new[] { "UserAccountId", "ExpiresAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asset_uploads",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "asset_versions",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "client_installations",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "client_packages",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "currency_transactions",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "datastore_entries",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "game_launch_tickets",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "game_servers",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "launch_audit_events",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "launcher_authorizations",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "limited_asset_instances",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "limited_sales",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "password_credentials",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "place_versions",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "private_server_links",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "private_servers",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "render_jobs",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "thumbnails",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "user_sessions",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "assets",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "client_releases",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "user_accounts",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "places",
                schema: "platform");

            migrationBuilder.DropTable(
                name: "universes",
                schema: "platform");
        }
    }
}
