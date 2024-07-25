using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCardsmith.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Avatar Name. Seeded."),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Avatar Remote URL. Seeded."),
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
                name: "BlackManas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<int>(type: "int", nullable: false, comment: "Play Card Total Cost"),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Play Card Mana Conor Name"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Play Card Remote URL. Seeded."),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackManas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlueManas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<int>(type: "int", nullable: false, comment: "Play Card Total Cost"),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Play Card Mana Conor Name"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Play Card Remote URL. Seeded."),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlueManas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardFrameColors",
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
                    table.PrimaryKey("PK_CardFrameColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardTypes",
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
                    table.PrimaryKey("PK_CardTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "ColorlessManas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<int>(type: "int", nullable: false, comment: "Play Card Total Cost"),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Play Card Mana Conor Name"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Play Card Remote URL. Seeded."),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorlessManas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Event status.Seeded"),
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
                name: "GameExpansions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Card game expansion title.Seeded"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Card game expansion description. Seeded"),
                    ExpansionSymbolUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Card game expansion symbol. Seeded"),
                    CardsCount = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Numbwer of cards by expansion. Seeded"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameExpansions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GreenManas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<int>(type: "int", nullable: false, comment: "Play Card Total Cost"),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Play Card Mana Conor Name"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Play Card Remote URL. Seeded."),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreenManas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RedManas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<int>(type: "int", nullable: false, comment: "Play Card Total Cost"),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Play Card Mana Conor Name"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Play Card Remote URL. Seeded."),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedManas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WhiteManas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<int>(type: "int", nullable: false, comment: "Play Card Total Cost"),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Play Card Mana Conor Name"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Play Card Remote URL. Seeded."),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhiteManas", x => x.Id);
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
                    Nickname = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    AvatarId = table.Column<int>(type: "int", nullable: true, comment: "Avatar Identifier.Picked after signing in"),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Avatar remote URL.Picked after signing in"),
                    GameRulesId = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Game Rules published by Admin"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                        name: "FK_AspNetUsers_Avatars_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Article image URL"),
                    ArticleCreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Article creator identifier"),
                    ApprovedByAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "Is Article approved by Admin."),
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
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Avatar remote URL.Picked after signing in"),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Information about the artist"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Artist's eamil"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Artist's user id"),
                    ApprovedByAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "Is user artist applicaiton approved by the admin."),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artists_AspNetUsers_UserId",
                        column: x => x.UserId,
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
                    ChatId = table.Column<int>(type: "int", nullable: false)
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
                    EventDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Event Description"),
                    EventCreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Event creator"),
                    RemoteImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Event image url"),
                    EventStatusId = table.Column<int>(type: "int", nullable: false, comment: "Event status"),
                    ApprovedByAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "Is event approved by admin."),
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
                        name: "FK_Events_EventStatuses_EventStatusId",
                        column: x => x.EventStatusId,
                        principalTable: "EventStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameRules",
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
                    PublishedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameRules_AspNetUsers_PublishedById",
                        column: x => x.PublishedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Store name"),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Store location"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Store location"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Store phone"),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Store location"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Store image Url"),
                    ApprovedByAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "Store approved by Admin."),
                    StoreOwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Playcard name"),
                    BlackManaId = table.Column<int>(type: "int", nullable: true, comment: "Mana cost Id"),
                    BlueManaId = table.Column<int>(type: "int", nullable: true, comment: "Mana cost Id"),
                    RedManaId = table.Column<int>(type: "int", nullable: true, comment: "Mana cost Id"),
                    WhiteManaId = table.Column<int>(type: "int", nullable: true, comment: "Mana cost Id"),
                    GreenManaId = table.Column<int>(type: "int", nullable: true, comment: "Mana cost Id"),
                    ColorlessManaId = table.Column<int>(type: "int", nullable: true, comment: "Mana cost Id"),
                    CardFrameColorId = table.Column<int>(type: "int", nullable: true, comment: "Framecolor Id. There is a default value."),
                    CardRemoteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Image of the card saved locally upon creation."),
                    CardTypeId = table.Column<int>(type: "int", nullable: true, comment: "Card type identifier."),
                    AbilitiesAndFlavor = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Card use requirements and effects. Card power description."),
                    Power = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true, comment: "Card will deal damage equal to power."),
                    Toughness = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true, comment: "Card can take damage up to amount equal to its toughness."),
                    GameExpansionId = table.Column<int>(type: "int", nullable: true, comment: "This card is part of which expansion."),
                    IsEventCard = table.Column<bool>(type: "bit", nullable: false, comment: "Has this card been created during an event."),
                    IsBanned = table.Column<bool>(type: "bit", nullable: false, comment: "Has this card been created during an event."),
                    ArtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardSmithId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApprovedByAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "Has this card been approved by admin."),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_AspNetUsers_CardSmithId",
                        column: x => x.CardSmithId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cards_BlackManas_BlackManaId",
                        column: x => x.BlackManaId,
                        principalTable: "BlackManas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_BlueManas_BlueManaId",
                        column: x => x.BlueManaId,
                        principalTable: "BlueManas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_CardFrameColors_CardFrameColorId",
                        column: x => x.CardFrameColorId,
                        principalTable: "CardFrameColors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_CardTypes_CardTypeId",
                        column: x => x.CardTypeId,
                        principalTable: "CardTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_ColorlessManas_ColorlessManaId",
                        column: x => x.ColorlessManaId,
                        principalTable: "ColorlessManas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_GameExpansions_GameExpansionId",
                        column: x => x.GameExpansionId,
                        principalTable: "GameExpansions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_GreenManas_GreenManaId",
                        column: x => x.GreenManaId,
                        principalTable: "GreenManas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_RedManas_RedManaId",
                        column: x => x.RedManaId,
                        principalTable: "RedManas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_WhiteManas_WhiteManaId",
                        column: x => x.WhiteManaId,
                        principalTable: "WhiteManas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventMilestones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Milestone title"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Milestone description"),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Milestone instructions"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Image"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is milestone completed"),
                    RequireArtInput = table.Column<bool>(type: "bit", nullable: false, comment: "Does it require art input"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventMilestones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventMilestones_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameRulesComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Game rule component title. Seeded"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Game rule component desciption. Seeded"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Game rule component image url. Seeded"),
                    GameRulesId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRulesComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameRulesComponents_GameRules_GameRulesId",
                        column: x => x.GameRulesId,
                        principalTable: "GameRules",
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
                    RemoteImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Art URL"),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Art image extension"),
                    ArtIstId = table.Column<int>(type: "int", nullable: true, comment: "Is this art added by MagciCardsmith team."),
                    CardId = table.Column<int>(type: "int", nullable: true, comment: "Any cards using this art."),
                    IsEventArt = table.Column<bool>(type: "bit", nullable: false, comment: "Is art added from an event"),
                    ApprovedByAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "Is art approved by admin"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "User id"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arts_Artists_ArtIstId",
                        column: x => x.ArtIstId,
                        principalTable: "Artists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Arts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Arts_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CardReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Card title"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Card Description"),
                    Review = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "CardReview"),
                    Suggestions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "What can be done to resolve the issue."),
                    CardId = table.Column<int>(type: "int", nullable: false, comment: "Review of which card"),
                    PostedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardReviews_AspNetUsers_PostedByUserId",
                        column: x => x.PostedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CardReviews_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CardsMana",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mana color type."),
                    RemoteImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mana remote image url."),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardsMana", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardsMana_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false, comment: "Vote added to."),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Vote casted by."),
                    Value = table.Column<byte>(type: "tinyint", nullable: false, comment: "Vote value"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Votes_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_Artists_IsDeleted",
                table: "Artists",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_UserId",
                table: "Artists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Arts_ApplicationUserId",
                table: "Arts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Arts_ArtIstId",
                table: "Arts",
                column: "ArtIstId");

            migrationBuilder.CreateIndex(
                name: "IX_Arts_CardId",
                table: "Arts",
                column: "CardId",
                unique: true,
                filter: "[CardId] IS NOT NULL");

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
                name: "IX_BlackManas_IsDeleted",
                table: "BlackManas",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BlueManas_IsDeleted",
                table: "BlueManas",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CardFrameColors_IsDeleted",
                table: "CardFrameColors",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CardReviews_CardId",
                table: "CardReviews",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardReviews_IsDeleted",
                table: "CardReviews",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CardReviews_PostedByUserId",
                table: "CardReviews",
                column: "PostedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_BlackManaId",
                table: "Cards",
                column: "BlackManaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_BlueManaId",
                table: "Cards",
                column: "BlueManaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardFrameColorId",
                table: "Cards",
                column: "CardFrameColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardSmithId",
                table: "Cards",
                column: "CardSmithId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardTypeId",
                table: "Cards",
                column: "CardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_ColorlessManaId",
                table: "Cards",
                column: "ColorlessManaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_EventId",
                table: "Cards",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_GameExpansionId",
                table: "Cards",
                column: "GameExpansionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_GreenManaId",
                table: "Cards",
                column: "GreenManaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_IsDeleted",
                table: "Cards",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_RedManaId",
                table: "Cards",
                column: "RedManaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_WhiteManaId",
                table: "Cards",
                column: "WhiteManaId");

            migrationBuilder.CreateIndex(
                name: "IX_CardsMana_CardId",
                table: "CardsMana",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardsMana_IsDeleted",
                table: "CardsMana",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CardTypes_IsDeleted",
                table: "CardTypes",
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
                name: "IX_ColorlessManas_IsDeleted",
                table: "ColorlessManas",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EventMilestones_EventId",
                table: "EventMilestones",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventMilestones_IsDeleted",
                table: "EventMilestones",
                column: "IsDeleted");

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
                name: "IX_GameExpansions_IsDeleted",
                table: "GameExpansions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_GameRules_IsDeleted",
                table: "GameRules",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_GameRules_PublishedById",
                table: "GameRules",
                column: "PublishedById",
                unique: true,
                filter: "[PublishedById] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GameRulesComponents_GameRulesId",
                table: "GameRulesComponents",
                column: "GameRulesId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRulesComponents_IsDeleted",
                table: "GameRulesComponents",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_GreenManas_IsDeleted",
                table: "GreenManas",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_IsDeleted",
                table: "Messages",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RedManas_IsDeleted",
                table: "RedManas",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_IsDeleted",
                table: "Settings",
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
                name: "IX_Votes_CardId",
                table: "Votes",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WhiteManas_IsDeleted",
                table: "WhiteManas",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Arts");

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
                name: "CardReviews");

            migrationBuilder.DropTable(
                name: "CardsMana");

            migrationBuilder.DropTable(
                name: "ChatUsers");

            migrationBuilder.DropTable(
                name: "EventMilestones");

            migrationBuilder.DropTable(
                name: "GameRulesComponents");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "GameRules");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "BlackManas");

            migrationBuilder.DropTable(
                name: "BlueManas");

            migrationBuilder.DropTable(
                name: "CardFrameColors");

            migrationBuilder.DropTable(
                name: "CardTypes");

            migrationBuilder.DropTable(
                name: "ColorlessManas");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "GameExpansions");

            migrationBuilder.DropTable(
                name: "GreenManas");

            migrationBuilder.DropTable(
                name: "RedManas");

            migrationBuilder.DropTable(
                name: "WhiteManas");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EventStatuses");

            migrationBuilder.DropTable(
                name: "Avatars");
        }
    }
}
