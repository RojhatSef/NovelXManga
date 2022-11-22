using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaAccessService.Migrations
{
    public partial class NovelXMangaDatabase : Migration
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
                    groupScanlationID = table.Column<int>(type: "int", nullable: true),
                    masterId = table.Column<int>(type: "int", nullable: true),
                    postModelID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtistModel_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtistModel_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtistModel_Fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtistModel_Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtistModel_AmountOfWork = table.Column<int>(type: "int", nullable: true),
                    ArtistBorn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArtistDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArtistModel_Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountOfWork = table.Column<int>(type: "int", nullable: true),
                    AuthorBorn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuthorDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "blogModels",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mangaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mangaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blogModels", x => x.id);
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
                    masterId = table.Column<int>(type: "int", nullable: true),
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
                name: "PostModels",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    BlogModelid = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_PostModels_blogModels_BlogModelid",
                        column: x => x.BlogModelid,
                        principalTable: "blogModels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    GroupScanlatingID = table.Column<int>(type: "int", nullable: false),
                    GroupScanlatingModelsGroupScanlatingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chapterModels", x => x.chapterID);
                    table.ForeignKey(
                        name: "FK_chapterModels_groupScanlatingModels_GroupScanlatingModelsGroupScanlatingID",
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
                name: "mangaModels",
                columns: table => new
                {
                    MangaID = table.Column<int>(type: "int", nullable: false),
                    MangaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssociatedNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlogModelID = table.Column<int>(type: "int", nullable: false),
                    MasterModelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mangaModels", x => x.MangaID);
                    table.ForeignKey(
                        name: "FK_mangaModels_blogModels_BlogModelID",
                        column: x => x.BlogModelID,
                        principalTable: "blogModels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mangaModels_MasterModels_MangaID",
                        column: x => x.MangaID,
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
                name: "GenresModel",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MangaID = table.Column<int>(type: "int", nullable: false),
                    mangaModelMangaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenresModel", x => x.GenresId);
                    table.ForeignKey(
                        name: "FK_GenresModel_mangaModels_mangaModelMangaID",
                        column: x => x.mangaModelMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagModel",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MangaID = table.Column<int>(type: "int", nullable: false),
                    mangaModelMangaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagModel", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_TagModel_mangaModels_mangaModelMangaID",
                        column: x => x.mangaModelMangaID,
                        principalTable: "mangaModels",
                        principalColumn: "MangaID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_chapterModels_GroupScanlatingModelsGroupScanlatingID",
                table: "chapterModels",
                column: "GroupScanlatingModelsGroupScanlatingID");

            migrationBuilder.CreateIndex(
                name: "IX_GenresModel_mangaModelMangaID",
                table: "GenresModel",
                column: "mangaModelMangaID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupScanlatingModelMasterModel_MasterModelsMasterID",
                table: "GroupScanlatingModelMasterModel",
                column: "MasterModelsMasterID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupScanlatingModelUserModel_userModelsId",
                table: "GroupScanlatingModelUserModel",
                column: "userModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_mangaModels_BlogModelID",
                table: "mangaModels",
                column: "BlogModelID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterModelUserModel_userModelsId",
                table: "MasterModelUserModel",
                column: "userModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_PostModels_BlogModelid",
                table: "PostModels",
                column: "BlogModelid");

            migrationBuilder.CreateIndex(
                name: "IX_PostModels_UserModelId",
                table: "PostModels",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TagModel_mangaModelMangaID",
                table: "TagModel",
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
                name: "chapterModels");

            migrationBuilder.DropTable(
                name: "GenresModel");

            migrationBuilder.DropTable(
                name: "GroupScanlatingModelMasterModel");

            migrationBuilder.DropTable(
                name: "GroupScanlatingModelUserModel");

            migrationBuilder.DropTable(
                name: "MasterModelUserModel");

            migrationBuilder.DropTable(
                name: "PostModels");

            migrationBuilder.DropTable(
                name: "TagModel");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "groupScanlatingModels");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "mangaModels");

            migrationBuilder.DropTable(
                name: "blogModels");

            migrationBuilder.DropTable(
                name: "MasterModels");
        }
    }
}
