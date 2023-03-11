using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaAccessService.Migrations
{
    public partial class Character : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artistModels", x => x.ArtistId);
                });

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
                    ForgottPasswordFavoritAnimal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForgottPasswordFavActor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForgottPasswordFavoritPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userPhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nameInNativeLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    placeOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zodiac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAcc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    groupScanlationID = table.Column<int>(type: "int", nullable: true),
                    MangaModelId = table.Column<int>(type: "int", nullable: true),
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
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorModels", x => x.AuthorID);
                });

            migrationBuilder.CreateTable(
                name: "BuyPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyWebsite = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chapterModels",
                columns: table => new
                {
                    chapterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateRelease = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedChapter = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    FamousQuote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    EyeColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HairColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abilities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hobbies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dislikes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Likes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalityTraits = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.CharacterId);
                });

            migrationBuilder.CreateTable(
                name: "completedBookLists",
                columns: table => new
                {
                    CompletedBookListID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaModelId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_completedBookLists", x => x.CompletedBookListID);
                });

            migrationBuilder.CreateTable(
                name: "droppedBookLists",
                columns: table => new
                {
                    DroppedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaModelId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_droppedBookLists", x => x.DroppedId);
                });

            migrationBuilder.CreateTable(
                name: "favoritBookLists",
                columns: table => new
                {
                    FavoritBookListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaModelId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favoritBookLists", x => x.FavoritBookListId);
                });

            migrationBuilder.CreateTable(
                name: "GenresModels",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isChecked = table.Column<bool>(type: "bit", nullable: false),
                    GenreDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TagHeavy = table.Column<int>(type: "int", nullable: true),
                    MangaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenresModels", x => x.GenresId);
                });

            migrationBuilder.CreateTable(
                name: "PostModels",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postComment = table.Column<string>(type: "nvarchar(750)", maxLength: 750, nullable: false),
                    score = table.Column<double>(type: "float", nullable: true),
                    CommentPostedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentPostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostModels", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_PostModels_PostModels_ParentPostId",
                        column: x => x.ParentPostId,
                        principalTable: "PostModels",
                        principalColumn: "PostId");
                });

            migrationBuilder.CreateTable(
                name: "readingLists",
                columns: table => new
                {
                    ReadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaModelId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_readingLists", x => x.ReadId);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookScore = table.Column<int>(type: "int", nullable: false),
                    voteReview = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(750)", maxLength: 750, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewID);
                });

            migrationBuilder.CreateTable(
                name: "TagModels",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isChecked = table.Column<bool>(type: "bit", nullable: false),
                    TagDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TagHeavy = table.Column<int>(type: "int", nullable: true),
                    MangaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagModels", x => x.TagId);
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
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voiceActorModels", x => x.VoiceActorId);
                });

            migrationBuilder.CreateTable(
                name: "wishBookLists",
                columns: table => new
                {
                    WishBookListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaModelId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wishBookLists", x => x.WishBookListId);
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
                name: "CompletedBookListUserModel",
                columns: table => new
                {
                    CompletedListCompletedBookListID = table.Column<int>(type: "int", nullable: false),
                    UserModelsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedBookListUserModel", x => new { x.CompletedListCompletedBookListID, x.UserModelsId });
                    table.ForeignKey(
                        name: "FK_CompletedBookListUserModel_AspNetUsers_UserModelsId",
                        column: x => x.UserModelsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompletedBookListUserModel_completedBookLists_CompletedListCompletedBookListID",
                        column: x => x.CompletedListCompletedBookListID,
                        principalTable: "completedBookLists",
                        principalColumn: "CompletedBookListID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DroppedBookListUserModel",
                columns: table => new
                {
                    DroppedListDroppedId = table.Column<int>(type: "int", nullable: false),
                    UserModelsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DroppedBookListUserModel", x => new { x.DroppedListDroppedId, x.UserModelsId });
                    table.ForeignKey(
                        name: "FK_DroppedBookListUserModel_AspNetUsers_UserModelsId",
                        column: x => x.UserModelsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DroppedBookListUserModel_droppedBookLists_DroppedListDroppedId",
                        column: x => x.DroppedListDroppedId,
                        principalTable: "droppedBookLists",
                        principalColumn: "DroppedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoritBookListUserModel",
                columns: table => new
                {
                    FavoritListFavoritBookListId = table.Column<int>(type: "int", nullable: false),
                    UserModelsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritBookListUserModel", x => new { x.FavoritListFavoritBookListId, x.UserModelsId });
                    table.ForeignKey(
                        name: "FK_FavoritBookListUserModel_AspNetUsers_UserModelsId",
                        column: x => x.UserModelsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoritBookListUserModel_favoritBookLists_FavoritListFavoritBookListId",
                        column: x => x.FavoritListFavoritBookListId,
                        principalTable: "favoritBookLists",
                        principalColumn: "FavoritBookListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostModelUserModel",
                columns: table => new
                {
                    PostModelPostId = table.Column<int>(type: "int", nullable: false),
                    UserModelId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostModelUserModel", x => new { x.PostModelPostId, x.UserModelId });
                    table.ForeignKey(
                        name: "FK_PostModelUserModel_AspNetUsers_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostModelUserModel_PostModels_PostModelPostId",
                        column: x => x.PostModelPostId,
                        principalTable: "PostModels",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReadingListUserModel",
                columns: table => new
                {
                    ReadingListReadId = table.Column<int>(type: "int", nullable: false),
                    UserModelsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingListUserModel", x => new { x.ReadingListReadId, x.UserModelsId });
                    table.ForeignKey(
                        name: "FK_ReadingListUserModel_AspNetUsers_UserModelsId",
                        column: x => x.UserModelsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReadingListUserModel_readingLists_ReadingListReadId",
                        column: x => x.ReadingListReadId,
                        principalTable: "readingLists",
                        principalColumn: "ReadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewUserModel",
                columns: table => new
                {
                    ReviewsReviewID = table.Column<int>(type: "int", nullable: false),
                    UserModelsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewUserModel", x => new { x.ReviewsReviewID, x.UserModelsId });
                    table.ForeignKey(
                        name: "FK_ReviewUserModel_AspNetUsers_UserModelsId",
                        column: x => x.UserModelsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewUserModel_Reviews_ReviewsReviewID",
                        column: x => x.ReviewsReviewID,
                        principalTable: "Reviews",
                        principalColumn: "ReviewID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserModelWishBookList",
                columns: table => new
                {
                    UserModelsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WishListWishBookListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModelWishBookList", x => new { x.UserModelsId, x.WishListWishBookListId });
                    table.ForeignKey(
                        name: "FK_UserModelWishBookList_AspNetUsers_UserModelsId",
                        column: x => x.UserModelsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserModelWishBookList_wishBookLists_WishListWishBookListId",
                        column: x => x.WishListWishBookListId,
                        principalTable: "wishBookLists",
                        principalColumn: "WishBookListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogModelPostModel",
                columns: table => new
                {
                    BlogModelsBlogModelId = table.Column<int>(type: "int", nullable: false),
                    postsModelPostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogModelPostModel", x => new { x.BlogModelsBlogModelId, x.postsModelPostId });
                    table.ForeignKey(
                        name: "FK_BlogModelPostModel_blogModels_BlogModelsBlogModelId",
                        column: x => x.BlogModelsBlogModelId,
                        principalTable: "blogModels",
                        principalColumn: "BlogModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogModelPostModel_PostModels_postsModelPostId",
                        column: x => x.postsModelPostId,
                        principalTable: "PostModels",
                        principalColumn: "PostId",
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
                    MonthRead = table.Column<int>(type: "int", nullable: true),
                    WeekRead = table.Column<int>(type: "int", nullable: true),
                    YearRead = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseYear = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndingYear = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BookAddedToDB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BlogModelId = table.Column<int>(type: "int", nullable: false),
                    GroupScanlatingID = table.Column<int>(type: "int", nullable: true),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletedBookListID = table.Column<int>(type: "int", nullable: true),
                    DroppedBookListDroppedId = table.Column<int>(type: "int", nullable: true),
                    FavoritBookListId = table.Column<int>(type: "int", nullable: true),
                    ReadingListReadId = table.Column<int>(type: "int", nullable: true),
                    WishBookListId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_mangaModels_completedBookLists_CompletedBookListID",
                        column: x => x.CompletedBookListID,
                        principalTable: "completedBookLists",
                        principalColumn: "CompletedBookListID");
                    table.ForeignKey(
                        name: "FK_mangaModels_droppedBookLists_DroppedBookListDroppedId",
                        column: x => x.DroppedBookListDroppedId,
                        principalTable: "droppedBookLists",
                        principalColumn: "DroppedId");
                    table.ForeignKey(
                        name: "FK_mangaModels_favoritBookLists_FavoritBookListId",
                        column: x => x.FavoritBookListId,
                        principalTable: "favoritBookLists",
                        principalColumn: "FavoritBookListId");
                    table.ForeignKey(
                        name: "FK_mangaModels_readingLists_ReadingListReadId",
                        column: x => x.ReadingListReadId,
                        principalTable: "readingLists",
                        principalColumn: "ReadId");
                    table.ForeignKey(
                        name: "FK_mangaModels_wishBookLists_WishBookListId",
                        column: x => x.WishBookListId,
                        principalTable: "wishBookLists",
                        principalColumn: "WishBookListId");
                });

            migrationBuilder.CreateTable(
                name: "ArtistModelMangaModel",
                columns: table => new
                {
                    ArtistModelsArtistId = table.Column<int>(type: "int", nullable: false),
                    MangaModelsMangaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistModelMangaModel", x => new { x.ArtistModelsArtistId, x.MangaModelsMangaID });
                    table.ForeignKey(
                        name: "FK_ArtistModelMangaModel_artistModels_ArtistModelsArtistId",
                        column: x => x.ArtistModelsArtistId,
                        principalTable: "artistModels",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistModelMangaModel_mangaModels_MangaModelsMangaID",
                        column: x => x.MangaModelsMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorModelMangaModel",
                columns: table => new
                {
                    AuthormodelsAuthorID = table.Column<int>(type: "int", nullable: false),
                    MangaModelsMangaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorModelMangaModel", x => new { x.AuthormodelsAuthorID, x.MangaModelsMangaID });
                    table.ForeignKey(
                        name: "FK_AuthorModelMangaModel_authorModels_AuthormodelsAuthorID",
                        column: x => x.AuthormodelsAuthorID,
                        principalTable: "authorModels",
                        principalColumn: "AuthorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorModelMangaModel_mangaModels_MangaModelsMangaID",
                        column: x => x.MangaModelsMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuyPageMangaModel",
                columns: table => new
                {
                    BuyPagesId = table.Column<int>(type: "int", nullable: false),
                    _MangaModelsMangaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyPageMangaModel", x => new { x.BuyPagesId, x._MangaModelsMangaID });
                    table.ForeignKey(
                        name: "FK_BuyPageMangaModel_BuyPages_BuyPagesId",
                        column: x => x.BuyPagesId,
                        principalTable: "BuyPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuyPageMangaModel_mangaModels__MangaModelsMangaID",
                        column: x => x._MangaModelsMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "groupScanlatingModels",
                columns: table => new
                {
                    GroupScanlatingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MangaModelId = table.Column<int>(type: "int", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_groupScanlatingModels_mangaModels_MangaModelId",
                        column: x => x.MangaModelId,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID");
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
                name: "MangaModelReview",
                columns: table => new
                {
                    MangaModelsMangaID = table.Column<int>(type: "int", nullable: false),
                    reviewsReviewID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaModelReview", x => new { x.MangaModelsMangaID, x.reviewsReviewID });
                    table.ForeignKey(
                        name: "FK_MangaModelReview_mangaModels_MangaModelsMangaID",
                        column: x => x.MangaModelsMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaModelReview_Reviews_reviewsReviewID",
                        column: x => x.reviewsReviewID,
                        principalTable: "Reviews",
                        principalColumn: "ReviewID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "MangaModelUserModel",
                columns: table => new
                {
                    MangaModelsMangaID = table.Column<int>(type: "int", nullable: false),
                    userModelsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaModelUserModel", x => new { x.MangaModelsMangaID, x.userModelsId });
                    table.ForeignKey(
                        name: "FK_MangaModelUserModel_AspNetUsers_userModelsId",
                        column: x => x.userModelsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaModelUserModel_mangaModels_MangaModelsMangaID",
                        column: x => x.MangaModelsMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MangaModelVoiceActorModel",
                columns: table => new
                {
                    MangaModelsMangaID = table.Column<int>(type: "int", nullable: false),
                    VoiceActorsVoiceActorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaModelVoiceActorModel", x => new { x.MangaModelsMangaID, x.VoiceActorsVoiceActorId });
                    table.ForeignKey(
                        name: "FK_MangaModelVoiceActorModel_mangaModels_MangaModelsMangaID",
                        column: x => x.MangaModelsMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaModelVoiceActorModel_voiceActorModels_VoiceActorsVoiceActorId",
                        column: x => x.VoiceActorsVoiceActorId,
                        principalTable: "voiceActorModels",
                        principalColumn: "VoiceActorId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "GroupScanlatingModelMangaModel",
                columns: table => new
                {
                    GroupScanlatingID = table.Column<int>(type: "int", nullable: false),
                    MangaModelsMangaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupScanlatingModelMangaModel", x => new { x.GroupScanlatingID, x.MangaModelsMangaID });
                    table.ForeignKey(
                        name: "FK_GroupScanlatingModelMangaModel_groupScanlatingModels_GroupScanlatingID",
                        column: x => x.GroupScanlatingID,
                        principalTable: "groupScanlatingModels",
                        principalColumn: "GroupScanlatingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupScanlatingModelMangaModel_mangaModels_MangaModelsMangaID",
                        column: x => x.MangaModelsMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID",
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
                name: "OfficalWebsites",
                columns: table => new
                {
                    OfficalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficalWebsiteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Line = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Naver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Languages_",
                columns: table => new
                {
                    languageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MangaID = table.Column<int>(type: "int", nullable: true),
                    OfficalWebsiteOfficalID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages_", x => x.languageId);
                    table.ForeignKey(
                        name: "FK_Languages__OfficalWebsites_OfficalWebsiteOfficalID",
                        column: x => x.OfficalWebsiteOfficalID,
                        principalTable: "OfficalWebsites",
                        principalColumn: "OfficalID");
                });

            migrationBuilder.CreateTable(
                name: "BuyPageLanguages",
                columns: table => new
                {
                    BuyPagesId = table.Column<int>(type: "int", nullable: false),
                    _LanguageslanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyPageLanguages", x => new { x.BuyPagesId, x._LanguageslanguageId });
                    table.ForeignKey(
                        name: "FK_BuyPageLanguages_BuyPages_BuyPagesId",
                        column: x => x.BuyPagesId,
                        principalTable: "BuyPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuyPageLanguages_Languages___LanguageslanguageId",
                        column: x => x._LanguageslanguageId,
                        principalTable: "Languages_",
                        principalColumn: "languageId",
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

            migrationBuilder.CreateIndex(
                name: "IX_ArtistModelMangaModel_MangaModelsMangaID",
                table: "ArtistModelMangaModel",
                column: "MangaModelsMangaID");

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
                name: "IX_AuthorModelMangaModel_MangaModelsMangaID",
                table: "AuthorModelMangaModel",
                column: "MangaModelsMangaID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogModelPostModel_postsModelPostId",
                table: "BlogModelPostModel",
                column: "postsModelPostId");

            migrationBuilder.CreateIndex(
                name: "IX_blogModels_UserModelId",
                table: "blogModels",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyPageLanguages__LanguageslanguageId",
                table: "BuyPageLanguages",
                column: "_LanguageslanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyPageMangaModel__MangaModelsMangaID",
                table: "BuyPageMangaModel",
                column: "_MangaModelsMangaID");

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
                name: "IX_CompletedBookListUserModel_UserModelsId",
                table: "CompletedBookListUserModel",
                column: "UserModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_DroppedBookListUserModel_UserModelsId",
                table: "DroppedBookListUserModel",
                column: "UserModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritBookListUserModel_UserModelsId",
                table: "FavoritBookListUserModel",
                column: "UserModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_GenresModelMangaModel_MangaModelsMangaID",
                table: "GenresModelMangaModel",
                column: "MangaModelsMangaID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupScanlatingModelMangaModel_MangaModelsMangaID",
                table: "GroupScanlatingModelMangaModel",
                column: "MangaModelsMangaID");

            migrationBuilder.CreateIndex(
                name: "IX_groupScanlatingModels_MangaModelId",
                table: "groupScanlatingModels",
                column: "MangaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupScanlatingModelUserModel_userModelsId",
                table: "GroupScanlatingModelUserModel",
                column: "userModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages__OfficalWebsiteOfficalID",
                table: "Languages_",
                column: "OfficalWebsiteOfficalID");

            migrationBuilder.CreateIndex(
                name: "IX_LanguagesMangaModel_MangaModelsMangaID",
                table: "LanguagesMangaModel",
                column: "MangaModelsMangaID");

            migrationBuilder.CreateIndex(
                name: "IX_MangaModelMangaModel_relatedSeriesMangaID",
                table: "MangaModelMangaModel",
                column: "relatedSeriesMangaID");

            migrationBuilder.CreateIndex(
                name: "IX_MangaModelReview_reviewsReviewID",
                table: "MangaModelReview",
                column: "reviewsReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_mangaModels_BlogModelId",
                table: "mangaModels",
                column: "BlogModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mangaModels_CompletedBookListID",
                table: "mangaModels",
                column: "CompletedBookListID");

            migrationBuilder.CreateIndex(
                name: "IX_mangaModels_DroppedBookListDroppedId",
                table: "mangaModels",
                column: "DroppedBookListDroppedId");

            migrationBuilder.CreateIndex(
                name: "IX_mangaModels_FavoritBookListId",
                table: "mangaModels",
                column: "FavoritBookListId");

            migrationBuilder.CreateIndex(
                name: "IX_mangaModels_ReadingListReadId",
                table: "mangaModels",
                column: "ReadingListReadId");

            migrationBuilder.CreateIndex(
                name: "IX_mangaModels_WishBookListId",
                table: "mangaModels",
                column: "WishBookListId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaModelTagModel_TagsModelsTagId",
                table: "MangaModelTagModel",
                column: "TagsModelsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaModelUserModel_userModelsId",
                table: "MangaModelUserModel",
                column: "userModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaModelVoiceActorModel_VoiceActorsVoiceActorId",
                table: "MangaModelVoiceActorModel",
                column: "VoiceActorsVoiceActorId");

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
                name: "IX_PostModels_ParentPostId",
                table: "PostModels",
                column: "ParentPostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostModelUserModel_UserModelId",
                table: "PostModelUserModel",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingListUserModel_UserModelsId",
                table: "ReadingListUserModel",
                column: "UserModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewUserModel_UserModelsId",
                table: "ReviewUserModel",
                column: "UserModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserModelWishBookList_WishListWishBookListId",
                table: "UserModelWishBookList",
                column: "WishListWishBookListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistModelMangaModel");

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
                name: "AuthorModelMangaModel");

            migrationBuilder.DropTable(
                name: "BlogModelPostModel");

            migrationBuilder.DropTable(
                name: "BuyPageLanguages");

            migrationBuilder.DropTable(
                name: "BuyPageMangaModel");

            migrationBuilder.DropTable(
                name: "ChapterModelGroupScanlatingModel");

            migrationBuilder.DropTable(
                name: "CharacterCharacter");

            migrationBuilder.DropTable(
                name: "CharacterMangaModel");

            migrationBuilder.DropTable(
                name: "CompletedBookListUserModel");

            migrationBuilder.DropTable(
                name: "DroppedBookListUserModel");

            migrationBuilder.DropTable(
                name: "FavoritBookListUserModel");

            migrationBuilder.DropTable(
                name: "GenresModelMangaModel");

            migrationBuilder.DropTable(
                name: "GroupScanlatingModelMangaModel");

            migrationBuilder.DropTable(
                name: "GroupScanlatingModelUserModel");

            migrationBuilder.DropTable(
                name: "LanguagesMangaModel");

            migrationBuilder.DropTable(
                name: "MangaModelMangaModel");

            migrationBuilder.DropTable(
                name: "MangaModelReview");

            migrationBuilder.DropTable(
                name: "MangaModelTagModel");

            migrationBuilder.DropTable(
                name: "MangaModelUserModel");

            migrationBuilder.DropTable(
                name: "MangaModelVoiceActorModel");

            migrationBuilder.DropTable(
                name: "PostModelUserModel");

            migrationBuilder.DropTable(
                name: "ReadingListUserModel");

            migrationBuilder.DropTable(
                name: "ReviewUserModel");

            migrationBuilder.DropTable(
                name: "UserModelWishBookList");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BuyPages");

            migrationBuilder.DropTable(
                name: "chapterModels");

            migrationBuilder.DropTable(
                name: "GenresModels");

            migrationBuilder.DropTable(
                name: "Languages_");

            migrationBuilder.DropTable(
                name: "TagModels");

            migrationBuilder.DropTable(
                name: "PostModels");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "OfficalWebsites");

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
                name: "completedBookLists");

            migrationBuilder.DropTable(
                name: "droppedBookLists");

            migrationBuilder.DropTable(
                name: "favoritBookLists");

            migrationBuilder.DropTable(
                name: "readingLists");

            migrationBuilder.DropTable(
                name: "wishBookLists");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
