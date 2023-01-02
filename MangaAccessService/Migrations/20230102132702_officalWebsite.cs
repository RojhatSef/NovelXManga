using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaAccessService.Migrations
{
    public partial class officalWebsite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Allias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForumName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userPhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nameInNativeLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    placeOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zodiac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    groupScanlationID = table.Column<int>(type: "int", nullable: true),
                    masterId = table.Column<int>(type: "int", nullable: true),
                    postModelID = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "chapterModels",
                columns: table => new
                {
                    chapterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateRelease = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Volumes = table.Column<int>(type: "int", nullable: true),
                    chapterNumber = table.Column<int>(type: "int", nullable: true),
                    chapterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    chapterLinkNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupScanlatingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chapterModels", x => x.chapterID);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Background = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Born = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Death = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    specie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOffResidence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    World = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lawful = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Personality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamousQuote = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.CharacterId);
                });

            migrationBuilder.CreateTable(
                name: "GenresModels",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenreDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TagHeavy = table.Column<int>(type: "int", nullable: true),
                    MangaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenresModels", x => x.GenresId);
                });

            migrationBuilder.CreateTable(
                name: "groupScanlatingModels",
                columns: table => new
                {
                    GroupScanlatingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterID = table.Column<int>(type: "int", nullable: true),
                    userID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Works = table.Column<int>(type: "int", nullable: true),
                    Started = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DissolutionOfCorporation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StudioWebsite = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groupScanlatingModels", x => x.GroupScanlatingID);
                });

            migrationBuilder.CreateTable(
                name: "Languages_",
                columns: table => new
                {
                    languageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficalWebSiteToBuy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MangaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages_", x => x.languageId);
                });

            migrationBuilder.CreateTable(
                name: "MasterModels",
                columns: table => new
                {
                    MasterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupScanlatingID = table.Column<int>(type: "int", nullable: true),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterModels", x => x.MasterID);
                });

            migrationBuilder.CreateTable(
                name: "TagModels",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TagHeavy = table.Column<int>(type: "int", nullable: true),
                    MangaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagModels", x => x.TagId);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "blogModels",
                columns: table => new
                {
                    BlogModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mangaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MangaId = table.Column<int>(type: "int", nullable: false),
                    UserModelId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blogModels", x => x.BlogModelId);
                    table.ForeignKey(
                        name: "FK_blogModels_AspNetUsers_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CharacterCharacter",
                columns: table => new
                {
                    FamilyCharacterId = table.Column<int>(type: "int", nullable: false),
                    Partner_sCharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterCharacter", x => new { x.FamilyCharacterId, x.Partner_sCharacterId });
                    table.ForeignKey(
                        name: "FK_CharacterCharacter_Characters_FamilyCharacterId",
                        column: x => x.FamilyCharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterCharacter_Characters_Partner_sCharacterId",
                        column: x => x.Partner_sCharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                });

            migrationBuilder.CreateTable(
                name: "ChapterModelGroupScanlatingModel",
                columns: table => new
                {
                    GroupScanlatingModelsGroupScanlatingID = table.Column<int>(type: "int", nullable: false),
                    chapterModelschapterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterModelGroupScanlatingModel", x => new { x.GroupScanlatingModelsGroupScanlatingID, x.chapterModelschapterID });
                    table.ForeignKey(
                        name: "FK_ChapterModelGroupScanlatingModel_chapterModels_chapterModelschapterID",
                        column: x => x.chapterModelschapterID,
                        principalTable: "chapterModels",
                        principalColumn: "chapterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChapterModelGroupScanlatingModel_groupScanlatingModels_GroupScanlatingModelsGroupScanlatingID",
                        column: x => x.GroupScanlatingModelsGroupScanlatingID,
                        principalTable: "groupScanlatingModels",
                        principalColumn: "GroupScanlatingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupScanlatingModelUserModel",
                columns: table => new
                {
                    GroupScanlatingID = table.Column<int>(type: "int", nullable: false),
                    userModelsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupScanlatingModelUserModel", x => new { x.GroupScanlatingID, x.userModelsId });
                    table.ForeignKey(
                        name: "FK_GroupScanlatingModelUserModel_AspNetUsers_userModelsId",
                        column: x => x.userModelsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupScanlatingModelUserModel_groupScanlatingModels_GroupScanlatingID",
                        column: x => x.GroupScanlatingID,
                        principalTable: "groupScanlatingModels",
                        principalColumn: "GroupScanlatingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupScanlatingModelMasterModel",
                columns: table => new
                {
                    GroupScanlatingID = table.Column<int>(type: "int", nullable: false),
                    MasterModelsMasterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupScanlatingModelMasterModel", x => new { x.GroupScanlatingID, x.MasterModelsMasterID });
                    table.ForeignKey(
                        name: "FK_GroupScanlatingModelMasterModel_groupScanlatingModels_GroupScanlatingID",
                        column: x => x.GroupScanlatingID,
                        principalTable: "groupScanlatingModels",
                        principalColumn: "GroupScanlatingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupScanlatingModelMasterModel_MasterModels_MasterModelsMasterID",
                        column: x => x.MasterModelsMasterID,
                        principalTable: "MasterModels",
                        principalColumn: "MasterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterModelUserModel",
                columns: table => new
                {
                    MasterModelMasterID = table.Column<int>(type: "int", nullable: false),
                    userModelsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterModelUserModel", x => new { x.MasterModelMasterID, x.userModelsId });
                    table.ForeignKey(
                        name: "FK_MasterModelUserModel_AspNetUsers_userModelsId",
                        column: x => x.userModelsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterModelUserModel_MasterModels_MasterModelMasterID",
                        column: x => x.MasterModelMasterID,
                        principalTable: "MasterModels",
                        principalColumn: "MasterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mangaModels",
                columns: table => new
                {
                    MangaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISBN13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    futureEvents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusInCountryOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletelyTranslated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    orignalWebtoon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalPublisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    score = table.Column<double>(type: "float", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficalLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseYear = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndingYear = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BlogModelId = table.Column<int>(type: "int", nullable: false),
                    MasterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mangaModels", x => x.MangaID);
                    table.ForeignKey(
                        name: "FK_mangaModels_blogModels_BlogModelId",
                        column: x => x.BlogModelId,
                        principalTable: "blogModels",
                        principalColumn: "BlogModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mangaModels_MasterModels_MasterID",
                        column: x => x.MasterID,
                        principalTable: "MasterModels",
                        principalColumn: "MasterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostModels",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    postComment = table.Column<string>(type: "nvarchar(750)", maxLength: 750, nullable: false),
                    score = table.Column<double>(type: "float", nullable: true),
                    CommentPostedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    BlogModelId = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModelId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostModels", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_PostModels_AspNetUsers_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostModels_blogModels_BlogModelId",
                        column: x => x.BlogModelId,
                        principalTable: "blogModels",
                        principalColumn: "BlogModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "artistModels",
                columns: table => new
                {
                    ArtistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reddit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameInNative = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WikiPedia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountOfWork = table.Column<int>(type: "int", nullable: true),
                    WorkingAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtistBorn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArtistDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MangaID = table.Column<int>(type: "int", nullable: true),
                    mangaModelMangaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artistModels", x => x.ArtistId);
                    table.ForeignKey(
                        name: "FK_artistModels_mangaModels_mangaModelMangaID",
                        column: x => x.mangaModelMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID");
                });

            migrationBuilder.CreateTable(
                name: "authorModels",
                columns: table => new
                {
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reddit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameInNative = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WikiPedia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountOfWork = table.Column<int>(type: "int", nullable: true),
                    WorkingAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuthorBorn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuthorDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MangaID = table.Column<int>(type: "int", nullable: true),
                    mangaModelMangaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorModels", x => x.AuthorID);
                    table.ForeignKey(
                        name: "FK_authorModels_mangaModels_mangaModelMangaID",
                        column: x => x.mangaModelMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID");
                });

            migrationBuilder.CreateTable(
                name: "CharacterMangaModel",
                columns: table => new
                {
                    CharactersCharacterId = table.Column<int>(type: "int", nullable: false),
                    MangaModelsMangaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMangaModel", x => new { x.CharactersCharacterId, x.MangaModelsMangaID });
                    table.ForeignKey(
                        name: "FK_CharacterMangaModel_Characters_CharactersCharacterId",
                        column: x => x.CharactersCharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMangaModel_mangaModels_MangaModelsMangaID",
                        column: x => x.MangaModelsMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenresModelMangaModel",
                columns: table => new
                {
                    GenresModelsGenresId = table.Column<int>(type: "int", nullable: false),
                    MangaModelsMangaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenresModelMangaModel", x => new { x.GenresModelsGenresId, x.MangaModelsMangaID });
                    table.ForeignKey(
                        name: "FK_GenresModelMangaModel_GenresModels_GenresModelsGenresId",
                        column: x => x.GenresModelsGenresId,
                        principalTable: "GenresModels",
                        principalColumn: "GenresId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenresModelMangaModel_mangaModels_MangaModelsMangaID",
                        column: x => x.MangaModelsMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguagesMangaModel",
                columns: table => new
                {
                    AllLanguageslanguageId = table.Column<int>(type: "int", nullable: false),
                    MangaModelsMangaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguagesMangaModel", x => new { x.AllLanguageslanguageId, x.MangaModelsMangaID });
                    table.ForeignKey(
                        name: "FK_LanguagesMangaModel_Languages__AllLanguageslanguageId",
                        column: x => x.AllLanguageslanguageId,
                        principalTable: "Languages_",
                        principalColumn: "languageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguagesMangaModel_mangaModels_MangaModelsMangaID",
                        column: x => x.MangaModelsMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MangaModelMangaModel",
                columns: table => new
                {
                    RecommendedMangaModelsMangaID = table.Column<int>(type: "int", nullable: false),
                    relatedSeriesMangaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaModelMangaModel", x => new { x.RecommendedMangaModelsMangaID, x.relatedSeriesMangaID });
                    table.ForeignKey(
                        name: "FK_MangaModelMangaModel_mangaModels_RecommendedMangaModelsMangaID",
                        column: x => x.RecommendedMangaModelsMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaModelMangaModel_mangaModels_relatedSeriesMangaID",
                        column: x => x.relatedSeriesMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID");
                });

            migrationBuilder.CreateTable(
                name: "MangaModelTagModel",
                columns: table => new
                {
                    MangaModelsMangaID = table.Column<int>(type: "int", nullable: false),
                    TagsModelsTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaModelTagModel", x => new { x.MangaModelsMangaID, x.TagsModelsTagId });
                    table.ForeignKey(
                        name: "FK_MangaModelTagModel_mangaModels_MangaModelsMangaID",
                        column: x => x.MangaModelsMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaModelTagModel_TagModels_TagsModelsTagId",
                        column: x => x.TagsModelsTagId,
                        principalTable: "TagModels",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "voiceActorModels",
                columns: table => new
                {
                    VoiceActorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reddit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CharacterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameInNative = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WikiPedia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountOfWork = table.Column<int>(type: "int", nullable: true),
                    WorkingAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VoiceBorn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VoiceDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MangaID = table.Column<int>(type: "int", nullable: true),
                    mangaModelMangaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voiceActorModels", x => x.VoiceActorId);
                    table.ForeignKey(
                        name: "FK_voiceActorModels_mangaModels_mangaModelMangaID",
                        column: x => x.mangaModelMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID");
                });

            migrationBuilder.CreateTable(
                name: "AssociatedNames",
                columns: table => new
                {
                    AssociatedNamesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MangaID = table.Column<int>(type: "int", nullable: true),
                    mangaModelMangaID = table.Column<int>(type: "int", nullable: true),
                    AuthorID = table.Column<int>(type: "int", nullable: true),
                    ArtistId = table.Column<int>(type: "int", nullable: true),
                    VoiceActorId = table.Column<int>(type: "int", nullable: true),
                    CharacterId = table.Column<int>(type: "int", nullable: true),
                    GroupScanlatingModelGroupScanlatingID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociatedNames", x => x.AssociatedNamesId);
                    table.ForeignKey(
                        name: "FK_AssociatedNames_artistModels_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "artistModels",
                        principalColumn: "ArtistId");
                    table.ForeignKey(
                        name: "FK_AssociatedNames_authorModels_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "authorModels",
                        principalColumn: "AuthorID");
                    table.ForeignKey(
                        name: "FK_AssociatedNames_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                    table.ForeignKey(
                        name: "FK_AssociatedNames_groupScanlatingModels_GroupScanlatingModelGroupScanlatingID",
                        column: x => x.GroupScanlatingModelGroupScanlatingID,
                        principalTable: "groupScanlatingModels",
                        principalColumn: "GroupScanlatingID");
                    table.ForeignKey(
                        name: "FK_AssociatedNames_mangaModels_mangaModelMangaID",
                        column: x => x.mangaModelMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID");
                    table.ForeignKey(
                        name: "FK_AssociatedNames_voiceActorModels_VoiceActorId",
                        column: x => x.VoiceActorId,
                        principalTable: "voiceActorModels",
                        principalColumn: "VoiceActorId");
                });

            migrationBuilder.CreateTable(
                name: "OfficalWebsites",
                columns: table => new
                {
                    OfficalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebsiteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficalWebsiteString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MangaID = table.Column<int>(type: "int", nullable: true),
                    mangaModelMangaID = table.Column<int>(type: "int", nullable: true),
                    GroupScanId = table.Column<int>(type: "int", nullable: true),
                    groupScanlatingModelGroupScanlatingID = table.Column<int>(type: "int", nullable: true),
                    AuthorID = table.Column<int>(type: "int", nullable: true),
                    ArtistId = table.Column<int>(type: "int", nullable: true),
                    VoiceActorId = table.Column<int>(type: "int", nullable: true),
                    CharacterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficalWebsites", x => x.OfficalID);
                    table.ForeignKey(
                        name: "FK_OfficalWebsites_artistModels_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "artistModels",
                        principalColumn: "ArtistId");
                    table.ForeignKey(
                        name: "FK_OfficalWebsites_authorModels_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "authorModels",
                        principalColumn: "AuthorID");
                    table.ForeignKey(
                        name: "FK_OfficalWebsites_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                    table.ForeignKey(
                        name: "FK_OfficalWebsites_groupScanlatingModels_groupScanlatingModelGroupScanlatingID",
                        column: x => x.groupScanlatingModelGroupScanlatingID,
                        principalTable: "groupScanlatingModels",
                        principalColumn: "GroupScanlatingID");
                    table.ForeignKey(
                        name: "FK_OfficalWebsites_mangaModels_mangaModelMangaID",
                        column: x => x.mangaModelMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID");
                    table.ForeignKey(
                        name: "FK_OfficalWebsites_voiceActorModels_VoiceActorId",
                        column: x => x.VoiceActorId,
                        principalTable: "voiceActorModels",
                        principalColumn: "VoiceActorId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_artistModels_mangaModelMangaID",
                table: "artistModels",
                column: "mangaModelMangaID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedNames_ArtistId",
                table: "AssociatedNames",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedNames_AuthorID",
                table: "AssociatedNames",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedNames_CharacterId",
                table: "AssociatedNames",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedNames_GroupScanlatingModelGroupScanlatingID",
                table: "AssociatedNames",
                column: "GroupScanlatingModelGroupScanlatingID");

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedNames_mangaModelMangaID",
                table: "AssociatedNames",
                column: "mangaModelMangaID");

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedNames_VoiceActorId",
                table: "AssociatedNames",
                column: "VoiceActorId");

            migrationBuilder.CreateIndex(
                name: "IX_authorModels_mangaModelMangaID",
                table: "authorModels",
                column: "mangaModelMangaID");

            migrationBuilder.CreateIndex(
                name: "IX_blogModels_UserModelId",
                table: "blogModels",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterModelGroupScanlatingModel_chapterModelschapterID",
                table: "ChapterModelGroupScanlatingModel",
                column: "chapterModelschapterID");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterCharacter_Partner_sCharacterId",
                table: "CharacterCharacter",
                column: "Partner_sCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMangaModel_MangaModelsMangaID",
                table: "CharacterMangaModel",
                column: "MangaModelsMangaID");

            migrationBuilder.CreateIndex(
                name: "IX_GenresModelMangaModel_MangaModelsMangaID",
                table: "GenresModelMangaModel",
                column: "MangaModelsMangaID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupScanlatingModelMasterModel_MasterModelsMasterID",
                table: "GroupScanlatingModelMasterModel",
                column: "MasterModelsMasterID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupScanlatingModelUserModel_userModelsId",
                table: "GroupScanlatingModelUserModel",
                column: "userModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguagesMangaModel_MangaModelsMangaID",
                table: "LanguagesMangaModel",
                column: "MangaModelsMangaID");

            migrationBuilder.CreateIndex(
                name: "IX_MangaModelMangaModel_relatedSeriesMangaID",
                table: "MangaModelMangaModel",
                column: "relatedSeriesMangaID");

            migrationBuilder.CreateIndex(
                name: "IX_mangaModels_BlogModelId",
                table: "mangaModels",
                column: "BlogModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mangaModels_MasterID",
                table: "mangaModels",
                column: "MasterID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MangaModelTagModel_TagsModelsTagId",
                table: "MangaModelTagModel",
                column: "TagsModelsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterModelUserModel_userModelsId",
                table: "MasterModelUserModel",
                column: "userModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficalWebsites_ArtistId",
                table: "OfficalWebsites",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficalWebsites_AuthorID",
                table: "OfficalWebsites",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_OfficalWebsites_CharacterId",
                table: "OfficalWebsites",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficalWebsites_groupScanlatingModelGroupScanlatingID",
                table: "OfficalWebsites",
                column: "groupScanlatingModelGroupScanlatingID");

            migrationBuilder.CreateIndex(
                name: "IX_OfficalWebsites_mangaModelMangaID",
                table: "OfficalWebsites",
                column: "mangaModelMangaID");

            migrationBuilder.CreateIndex(
                name: "IX_OfficalWebsites_VoiceActorId",
                table: "OfficalWebsites",
                column: "VoiceActorId");

            migrationBuilder.CreateIndex(
                name: "IX_PostModels_BlogModelId",
                table: "PostModels",
                column: "BlogModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PostModels_UserModelId",
                table: "PostModels",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_voiceActorModels_mangaModelMangaID",
                table: "voiceActorModels",
                column: "mangaModelMangaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "AssociatedNames");

            migrationBuilder.DropTable(
                name: "ChapterModelGroupScanlatingModel");

            migrationBuilder.DropTable(
                name: "CharacterCharacter");

            migrationBuilder.DropTable(
                name: "CharacterMangaModel");

            migrationBuilder.DropTable(
                name: "GenresModelMangaModel");

            migrationBuilder.DropTable(
                name: "GroupScanlatingModelMasterModel");

            migrationBuilder.DropTable(
                name: "GroupScanlatingModelUserModel");

            migrationBuilder.DropTable(
                name: "LanguagesMangaModel");

            migrationBuilder.DropTable(
                name: "MangaModelMangaModel");

            migrationBuilder.DropTable(
                name: "MangaModelTagModel");

            migrationBuilder.DropTable(
                name: "MasterModelUserModel");

            migrationBuilder.DropTable(
                name: "OfficalWebsites");

            migrationBuilder.DropTable(
                name: "PostModels");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "chapterModels");

            migrationBuilder.DropTable(
                name: "GenresModels");

            migrationBuilder.DropTable(
                name: "Languages_");

            migrationBuilder.DropTable(
                name: "TagModels");

            migrationBuilder.DropTable(
                name: "artistModels");

            migrationBuilder.DropTable(
                name: "authorModels");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "groupScanlatingModels");

            migrationBuilder.DropTable(
                name: "voiceActorModels");

            migrationBuilder.DropTable(
                name: "mangaModels");

            migrationBuilder.DropTable(
                name: "blogModels");

            migrationBuilder.DropTable(
                name: "MasterModels");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
