using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wizzarts.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "Avatar Name. Seeded."),
                    AvatarUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, comment: "Avatar Remote URL. Seeded."),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardGameExpansions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Card game expansion title.Seeded"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Card game expansion description. Seeded"),
                    ExpansionSymbolUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "Card game expansion symbol. Seeded"),
                    CardsCount = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Number of cards by expansion. Seeded"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardGameExpansions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RelationKey = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeckStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Event status.Seeded"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ManaCosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mana color type."),
                    RemoteImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mana remote image url."),
                    Cost = table.Column<int>(type: "int", nullable: false, comment: "Play Card Total Cost"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManaCosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayCardFrameColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Play Card Frame color. Seeded"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Play Remote Image. Seeded"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayCardFrameColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayCardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Card type is."),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayCardTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagHelpActions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagHelpActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagHelpControllers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagHelpControllers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WizzartsCardGame",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardGameRulesId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WizzartsCardGame", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AvatarId = table.Column<int>(type: "int", nullable: true, comment: "Avatar Identifier.Picked after signing in"),
                    AvatarUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "Avatar remote URL.Picked after signing in"),
                    Bio = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "Information about the artist"),
                    AdminFeedBack = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "Admin might want to remind client about name policies, missing profile details, missing avatar..."),
                    RequestFeedback = table.Column<bool>(type: "bit", nullable: false, comment: "Admin Request notification"),
                    HasBeenUpdated = table.Column<bool>(type: "bit", nullable: false, comment: "Customer acknowledged admin request and fulfilled it"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Avatars_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WizzartsGameRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameRulesIntroUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardReading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardReadingIntroUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardNameReading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardNameUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManaCostReading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManaCostUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardTypeReading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardTypeUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SetSymbolReading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SetSymbolUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardTextBoxReading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardTextBoxUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardPowerToughnessReading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardPowToughUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BattleFieldSetUp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BattleFieldIntroUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreaturesInBattle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LibraryInBattle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandsInBattle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraveyardInBattle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandInBattle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameActions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TappingUntapping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CastingSpells = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttackingAndBlocking = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartsOfTheTurn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeginningPhase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstMainPhase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CombatPhase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondMainPhase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndingPhase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Outro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutroUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WizzartsCardGameId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WizzartsGameRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WizzartsGameRules_WizzartsCardGame_WizzartsCardGameId",
                        column: x => x.WizzartsCardGameId,
                        principalTable: "WizzartsCardGame",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WizzartsGameRulesData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Game rule component title. Seeded"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Game rule component description. Seeded"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Game rule component image url. Seeded"),
                    WizzartsCardGameId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WizzartsGameRulesData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WizzartsGameRulesData_WizzartsCardGame_WizzartsCardGameId",
                        column: x => x.WizzartsCardGameId,
                        principalTable: "WizzartsCardGame",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Article title"),
                    ShortDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Article short description"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Article description"),
                    ImageUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "Article image URL"),
                    ApprovedByAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "Is Article approved by Admin."),
                    ForMainPage = table.Column<bool>(type: "bit", nullable: false),
                    ArticleCreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Article creator identifier"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_AspNetUsers_ArticleCreatorId",
                        column: x => x.ArticleCreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Arts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Art Title"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Art Description"),
                    RemoteImageUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "Art url"),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Image extension"),
                    ApprovedByAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "New art piece  has been approved or not"),
                    ForMainPage = table.Column<bool>(type: "bit", nullable: false, comment: "Premium account only"),
                    AddedByMemberId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arts_AspNetUsers_AddedByMemberId",
                        column: x => x.AddedByMemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatUsers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUsers", x => new { x.UserId, x.ChatId });
                    table.ForeignKey(
                        name: "FK_ChatUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatUsers_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Event Title"),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false, comment: "Event Description"),
                    RemoteImageUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "Event image url"),
                    EventStatusId = table.Column<int>(type: "int", nullable: false, comment: "Event status"),
                    ActionId = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Action name"),
                    ControllerId = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Controller name"),
                    ApprovedByAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "Is event approved by admin."),
                    ForMainPage = table.Column<bool>(type: "bit", nullable: false),
                    EventCreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Event creator"),
                    EventCategoryId = table.Column<int>(type: "int", nullable: false, comment: "Event creator"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_EventCreatorId",
                        column: x => x.EventCreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_EventCategories_EventCategoryId",
                        column: x => x.EventCategoryId,
                        principalTable: "EventCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_EventStatuses_EventStatusId",
                        column: x => x.EventStatusId,
                        principalTable: "EventStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_TagHelpActions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "TagHelpActions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_TagHelpControllers_ControllerId",
                        column: x => x.ControllerId,
                        principalTable: "TagHelpControllers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DeckId = table.Column<int>(type: "int", nullable: true),
                    DeckImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EstimatedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Store name"),
                    Country = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Store location"),
                    City = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Store location"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Store phone"),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Store location"),
                    Image = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "Store image Url"),
                    ApprovedByAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "Store approved by Admin."),
                    StoreOwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_AspNetUsers_StoreOwnerId",
                        column: x => x.StoreOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WizzartsTeamMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "Information about the artist"),
                    AvatarUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "Avatar remote URL.Picked after signing in"),
                    WizzartsCardGameId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Wizzarts Team user id"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WizzartsTeamMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WizzartsTeamMembers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WizzartsTeamMembers_WizzartsCardGame_WizzartsCardGameId",
                        column: x => x.WizzartsCardGameId,
                        principalTable: "WizzartsCardGame",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Title"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Description"),
                    Instructions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "Instructions"),
                    ImageUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, comment: "Image"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EventCategoryId = table.Column<int>(type: "int", nullable: false, comment: "Component type according to event type"),
                    ActionId = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Action name"),
                    ControllerId = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Controller name"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventComponents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventComponents_TagHelpActions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "TagHelpActions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventComponents_TagHelpControllers_ControllerId",
                        column: x => x.ControllerId,
                        principalTable: "TagHelpControllers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayCards",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "PlayCard name"),
                    CardFrameColorId = table.Column<int>(type: "int", nullable: true, comment: "Frame color Id. There is a default value."),
                    CardRemoteUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, comment: "Image of the card saved locally upon creation."),
                    CardTypeId = table.Column<int>(type: "int", nullable: true, comment: "Card type identifier."),
                    AbilitiesAndFlavor = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Card use requirements and effects. Card power description."),
                    Power = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true, comment: "Card will deal damage equal to power."),
                    Toughness = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true, comment: "Card can take damage up to amount equal to its toughness."),
                    CardGameExpansionId = table.Column<int>(type: "int", nullable: false, comment: "This card is part of which expansion."),
                    IsEventCard = table.Column<bool>(type: "bit", nullable: false, comment: "Has this card been created during an event?"),
                    ArtId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AddedByMemberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApprovedByAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "Has this card been approved by admin."),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    ForMainPage = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayCards_Arts_ArtId",
                        column: x => x.ArtId,
                        principalTable: "Arts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayCards_AspNetUsers_AddedByMemberId",
                        column: x => x.AddedByMemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayCards_CardGameExpansions_CardGameExpansionId",
                        column: x => x.CardGameExpansionId,
                        principalTable: "CardGameExpansions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayCards_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayCards_PlayCardFrameColors_CardFrameColorId",
                        column: x => x.CardFrameColorId,
                        principalTable: "PlayCardFrameColors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayCards_PlayCardTypes_CardTypeId",
                        column: x => x.CardTypeId,
                        principalTable: "PlayCardTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CardDecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ShippingAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedByMemberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardDecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardDecks_AspNetUsers_CreatedByMemberId",
                        column: x => x.CreatedByMemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardDecks_DeckStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "DeckStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardDecks_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CardComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Card title"),
                    CardFlavor = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Card Description"),
                    Review = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "CardReview"),
                    Suggestions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "What can be done to resolve the issue."),
                    CardId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Review of which card?"),
                    PostedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsAdminComment = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardComments_AspNetUsers_PostedByUserId",
                        column: x => x.PostedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardComments_PlayCards_CardId",
                        column: x => x.CardId,
                        principalTable: "PlayCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CardOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PlayCardId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardOrders_PlayCards_PlayCardId",
                        column: x => x.PlayCardId,
                        principalTable: "PlayCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ManaInCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mana color type."),
                    RemoteImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mana remote image url."),
                    CardId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ManaCostId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManaInCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManaInCard_ManaCosts_ManaCostId",
                        column: x => x.ManaCostId,
                        principalTable: "ManaCosts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ManaInCard_PlayCards_CardId",
                        column: x => x.CardId,
                        principalTable: "PlayCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Vote added to."),
                    AddedByMemberId = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Vote casted by."),
                    Value = table.Column<byte>(type: "tinyint", nullable: false, comment: "Vote value"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_AspNetUsers_AddedByMemberId",
                        column: x => x.AddedByMemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Votes_PlayCards_CardId",
                        column: x => x.CardId,
                        principalTable: "PlayCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeckOfCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeckId = table.Column<int>(type: "int", nullable: false),
                    PlayCardId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckOfCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeckOfCards_CardDecks_DeckId",
                        column: x => x.DeckId,
                        principalTable: "CardDecks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeckOfCards_PlayCards_PlayCardId",
                        column: x => x.PlayCardId,
                        principalTable: "PlayCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleCreatorId",
                table: "Articles",
                column: "ArticleCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_IsDeleted",
                table: "Articles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Arts_AddedByMemberId",
                table: "Arts",
                column: "AddedByMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Arts_IsDeleted",
                table: "Arts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_IsDeleted",
                table: "AspNetRoles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IsDeleted",
                table: "AspNetUsers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_IsDeleted",
                table: "Avatars",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CardComments_CardId",
                table: "CardComments",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardComments_IsDeleted",
                table: "CardComments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CardComments_PostedByUserId",
                table: "CardComments",
                column: "PostedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CardDecks_CreatedByMemberId",
                table: "CardDecks",
                column: "CreatedByMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_CardDecks_IsDeleted",
                table: "CardDecks",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CardDecks_StatusId",
                table: "CardDecks",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CardDecks_StoreId",
                table: "CardDecks",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CardGameExpansions_IsDeleted",
                table: "CardGameExpansions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CardOrders_IsDeleted",
                table: "CardOrders",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CardOrders_OrderId",
                table: "CardOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CardOrders_PlayCardId",
                table: "CardOrders",
                column: "PlayCardId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatId",
                table: "ChatMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_IsDeleted",
                table: "ChatMessages",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_IsDeleted",
                table: "Chats",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsers_ChatId",
                table: "ChatUsers",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsers_IsDeleted",
                table: "ChatUsers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DeckOfCards_DeckId",
                table: "DeckOfCards",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_DeckOfCards_IsDeleted",
                table: "DeckOfCards",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DeckOfCards_PlayCardId",
                table: "DeckOfCards",
                column: "PlayCardId");

            migrationBuilder.CreateIndex(
                name: "IX_DeckStatuses_IsDeleted",
                table: "DeckStatuses",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EventCategories_IsDeleted",
                table: "EventCategories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EventComponents_ActionId",
                table: "EventComponents",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_EventComponents_ControllerId",
                table: "EventComponents",
                column: "ControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_EventComponents_EventId",
                table: "EventComponents",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventComponents_IsDeleted",
                table: "EventComponents",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ActionId",
                table: "Events",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ControllerId",
                table: "Events",
                column: "ControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventCategoryId",
                table: "Events",
                column: "EventCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventCreatorId",
                table: "Events",
                column: "EventCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventStatusId",
                table: "Events",
                column: "EventStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_IsDeleted",
                table: "Events",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EventStatuses_IsDeleted",
                table: "EventStatuses",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ManaCosts_IsDeleted",
                table: "ManaCosts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ManaInCard_CardId",
                table: "ManaInCard",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_ManaInCard_IsDeleted",
                table: "ManaInCard",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ManaInCard_ManaCostId",
                table: "ManaInCard",
                column: "ManaCostId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IsDeleted",
                table: "Orders",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RecipientId",
                table: "Orders",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatuses_IsDeleted",
                table: "OrderStatuses",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PlayCardFrameColors_IsDeleted",
                table: "PlayCardFrameColors",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PlayCards_AddedByMemberId",
                table: "PlayCards",
                column: "AddedByMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayCards_ArtId",
                table: "PlayCards",
                column: "ArtId",
                unique: true,
                filter: "[ArtId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PlayCards_CardFrameColorId",
                table: "PlayCards",
                column: "CardFrameColorId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayCards_CardGameExpansionId",
                table: "PlayCards",
                column: "CardGameExpansionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayCards_CardTypeId",
                table: "PlayCards",
                column: "CardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayCards_EventId",
                table: "PlayCards",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayCards_IsDeleted",
                table: "PlayCards",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PlayCardTypes_IsDeleted",
                table: "PlayCardTypes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_IsDeleted",
                table: "Stores",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_StoreOwnerId",
                table: "Stores",
                column: "StoreOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TagHelpActions_IsDeleted",
                table: "TagHelpActions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TagHelpControllers_IsDeleted",
                table: "TagHelpControllers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_AddedByMemberId",
                table: "Votes",
                column: "AddedByMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CardId",
                table: "Votes",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_IsDeleted",
                table: "Votes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WizzartsCardGame_IsDeleted",
                table: "WizzartsCardGame",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WizzartsGameRules_IsDeleted",
                table: "WizzartsGameRules",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WizzartsGameRules_WizzartsCardGameId",
                table: "WizzartsGameRules",
                column: "WizzartsCardGameId",
                unique: true,
                filter: "[WizzartsCardGameId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WizzartsGameRulesData_IsDeleted",
                table: "WizzartsGameRulesData",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WizzartsGameRulesData_WizzartsCardGameId",
                table: "WizzartsGameRulesData",
                column: "WizzartsCardGameId");

            migrationBuilder.CreateIndex(
                name: "IX_WizzartsTeamMembers_IsDeleted",
                table: "WizzartsTeamMembers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WizzartsTeamMembers_UserId",
                table: "WizzartsTeamMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WizzartsTeamMembers_WizzartsCardGameId",
                table: "WizzartsTeamMembers",
                column: "WizzartsCardGameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CardComments");

            migrationBuilder.DropTable(
                name: "CardOrders");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "ChatUsers");

            migrationBuilder.DropTable(
                name: "DeckOfCards");

            migrationBuilder.DropTable(
                name: "EventComponents");

            migrationBuilder.DropTable(
                name: "ManaInCard");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "WizzartsGameRules");

            migrationBuilder.DropTable(
                name: "WizzartsGameRulesData");

            migrationBuilder.DropTable(
                name: "WizzartsTeamMembers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "CardDecks");

            migrationBuilder.DropTable(
                name: "ManaCosts");

            migrationBuilder.DropTable(
                name: "PlayCards");

            migrationBuilder.DropTable(
                name: "WizzartsCardGame");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "DeckStatuses");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Arts");

            migrationBuilder.DropTable(
                name: "CardGameExpansions");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "PlayCardFrameColors");

            migrationBuilder.DropTable(
                name: "PlayCardTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EventCategories");

            migrationBuilder.DropTable(
                name: "EventStatuses");

            migrationBuilder.DropTable(
                name: "TagHelpActions");

            migrationBuilder.DropTable(
                name: "TagHelpControllers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Avatars");
        }
    }
}
