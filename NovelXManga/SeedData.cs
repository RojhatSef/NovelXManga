
using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace NovelXManga
{
    public class SeedData
    {
        public UserModel User { get; set; }
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly MangaNNovelAuthDBContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly RoleManager<IdentityRole> roleManager;
        public SeedData(MangaNNovelAuthDBContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IWebHostEnvironment webHostEnvironment, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
            this.roleManager = roleManager;
        }
        public void addAssName()
        {
            var manga1 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Naruto");
            var assName1 = context.AssociatedNames.FirstOrDefault(e => e.nameString == "Naluto");
            var assName2 = context.AssociatedNames.FirstOrDefault(e => e.nameString == "Naruto S");
            var assName3 = context.AssociatedNames.FirstOrDefault(e => e.nameString == "Naruto Shippuden");
            manga1.AssociatedNames = new List<AssociatedNames> { assName1, assName2, assName3 };
            context.mangaModels.Update(manga1);
            context.SaveChanges();

        }
        public void names()
        {
            //var manga2 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Berserk");
            var manga1 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Naruto");

            var artist = context.artistModels.FirstOrDefault(e => e.FirstName == "Masashi");
            var author = context.authorModels.FirstOrDefault(e => e.FirstName == "Masashi");
            AssociatedNames associatedNames = new AssociatedNames();
            associatedNames.nameString = "Naluto";
            associatedNames.MangaID = manga1.MangaID;
            associatedNames.mangaModel = manga1;
            associatedNames.ArtistId = artist.ArtistId;
            associatedNames.ArtistModel = artist;
            AssociatedNames associatedNames2 = new AssociatedNames();
            associatedNames2.nameString = "Naruto S";
            associatedNames2.MangaID = manga1.MangaID;
            associatedNames2.mangaModel = manga1;
            associatedNames2.ArtistId = artist.ArtistId;
            associatedNames2.ArtistModel = artist;
            AssociatedNames associatedNames3 = new AssociatedNames();
            associatedNames3.nameString = "Naruto Shippuden";
            associatedNames3.MangaID = manga1.MangaID;
            associatedNames3.mangaModel = manga1;
            associatedNames3.ArtistId = artist.ArtistId;
            associatedNames3.ArtistModel = artist;

            //AssociatedNames associatedNames3 = new AssociatedNames();
            //associatedNames3.nameString = "Black Swordsman";
            //manga2.AssociatedNames = new List<AssociatedNames> { associatedNames3 };

            context.AssociatedNames.Add(associatedNames);
            context.AssociatedNames.Add(associatedNames2);
            context.AssociatedNames.Add(associatedNames3);
            context.SaveChanges();
            addAssName();

        }
        public void addAuthorToManga()
        {
            //var manga2 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Berserk");
            var manga1 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Naruto");
            var artist = context.artistModels.FirstOrDefault(e => e.FirstName == "Masashi");
            var author = context.authorModels.FirstOrDefault(e => e.FirstName == "Masashi");
            manga1.Authormodels = new List<AuthorModel> { author };
            manga1.ArtistModels = new List<ArtistModel> { artist };
            context.mangaModels.Update(manga1);
            context.SaveChanges();
            names();
        }
        public void CreatingAuthors()
        {
            //var manga2 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Berserk");
            var manga1 = context.mangaModels.FirstOrDefault(e => e.MangaName == "Naruto");


            var NewArtist = new ArtistModel
            {

                FirstName = "Masashi",
                LastName = "Kishimoto",
                Biography = "Graduated from the Faculty of Arts, Kyushu Sangyo University. \r\n\r\nMarried in 2003 and has children.\r\n\r\nKishimoto had a deal with Jump editorial department that he will receive Toriyama Akira's autographed paper as a reward when the serialization of Naruto reaches the third anniversary.\r\n\r\nHe and Oda Eiichiro are both rival and close friend.\r\n\r\nHis younger twin brother KISHIMOTO Seishi is 666 Satan’s author. Their art has been remarked as being very similar and accusations of plagiarism were made, either that Seishi had copied his brother or vice versa. However, Seishi notes in one of the volumes of his manga that the similarities are not intentional and that the occurrence would have been likely because they have been influenced by many of the same things. After several accusations, Masashi Kishimoto has asked that fans stop calling his brother a \"copy-cat\". His former assistants were ITAKURA Yuuichi, TASAKA Ryou and KAJISA Osamu.\r\nWhile IKEMOTO Mikio, TAIRA Kenji and OKUBO Akira were his assistants and now are his collaborators.\r\n\r\nHis favorite manga and anime are Dragon Ball, Yu Yu Hakusho, HUNTER x HUNTER, Ninku, AKIRA, Ghost in the Shell & Jin-Roh"
            ,
                MangaID = manga1.MangaID,
                mangaModel = manga1,
                BirthPlace = "Japan",
                Gender = "Male",
                WorkingAt = "Shueisha",
                AssociatedNames = null
            };
            var newAuthor = new AuthorModel
            {
                FirstName = "Masashi",
                LastName = "Kishimoto",
                Biography = "Graduated from the Faculty of Arts, Kyushu Sangyo University. \r\n\r\nMarried in 2003 and has children.\r\n\r\nKishimoto had a deal with Jump editorial department that he will receive Toriyama Akira's autographed paper as a reward when the serialization of Naruto reaches the third anniversary.\r\n\r\nHe and Oda Eiichiro are both rival and close friend.\r\n\r\nHis younger twin brother KISHIMOTO Seishi is 666 Satan’s author. Their art has been remarked as being very similar and accusations of plagiarism were made, either that Seishi had copied his brother or vice versa. However, Seishi notes in one of the volumes of his manga that the similarities are not intentional and that the occurrence would have been likely because they have been influenced by many of the same things. After several accusations, Masashi Kishimoto has asked that fans stop calling his brother a \"copy-cat\". His former assistants were ITAKURA Yuuichi, TASAKA Ryou and KAJISA Osamu.\r\nWhile IKEMOTO Mikio, TAIRA Kenji and OKUBO Akira were his assistants and now are his collaborators.\r\n\r\nHis favorite manga and anime are Dragon Ball, Yu Yu Hakusho, HUNTER x HUNTER, Ninku, AKIRA, Ghost in the Shell & Jin-Roh"
              ,
                mangaModel = manga1,
                MangaID = manga1.MangaID,
                BirthPlace = "Japan",
                Gender = "Male",
                WorkingAt = "Shueisha",
                AssociatedNames = null
            };

            context.artistModels.Add(NewArtist);
            context.authorModels.Add(newAuthor);

            context.SaveChanges();
            addAuthorToManga();
        }
        public void GenreSeed()
        {

        }
        public enum GenresModel
        {
            None,
            Action,
            Adult,
            Adventure,
            Comedy,
            Drama,
            Ecchi,
            Fantasy,
            [Description("Gender Bender")]
            GenderBender,
            Harem,
            Historical,
            Horror,
            Josei,
            [Description("Martial Arts")]
            MartialArts,
            Mature,
            Mecha,
            Mystery,
            Psychological,
            Romance,
            [Description("School Life")]
            SchoolLife,
            [Description("Sci-fi")]
            Scifi,
            Seinen,
            Shoujo,
            [Description("Shoujo Ai")]
            ShoujoAi,
            Shounen,
            [Description("Shounen Ai")]
            ShounenAi,
            [Description("Slice Of Life")]
            SliceOfLife,
            Smut,
            Sports,
            Supernatural,
            Tragedy,
            Wuxia,
            Xianxia,
            Xuanhuan,
            Yaoi,
            Yuri,
        }
        public async Task seedData()
        {
            context.Database.EnsureCreated();
            if (!context.mangaModels.Any())
            {
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/MangaImage/0f3f5666-7cb9-4713-a779-f1e1546a0d5f");
                int rolesCount = roleManager.Roles.Count();
                // creat an array loop our array add all our roles to the database
                string[] roles = { "Admin", "NormalUser", "Owner", "AdminControl", "Updater", "ShadowBanned", "Scanlators", "ForumAdmin", "Author", "Artist", "Publisher", "Marketing", "SearchEngineOS", "FrontEndDeveloper", "SupportGroups", "BackEndDeveloper", "ProjectManager", "Tester" };
                if (rolesCount == 0)
                {
                    for (int i = 0; i < roles.Length; i++)
                    {
                        if (!await roleManager.RoleExistsAsync(roles[i]))
                        {
                            var role = new IdentityRole();
                            role.Name = roles[i];
                            await roleManager.CreateAsync(role);
                        }
                    }
                }


                var user = new UserModel { UserName = "TestUSer", Email = "TestUser@hotmail.com", userPhotoPath = filePath };
                MasterModel masterModel = new MasterModel
                {
                    MangaModels = new MangaModel
                    {
                        MangaName = "Naruto",

                        PhotoPath = filePath,
                        ReleaseYear = DateTime.Now,
                        BlogModel = new BlogModel { mangaName = "Naruto" },
                        OfficialTranslations = "Japanese",
                        Description = "A Kid who got something stuck in his stomach",
                        CompletelyTranslated = "Completed",
                        OfficalLanguage = "Japanese",
                        ISBN10 = "1569319006",
                        ISBN13 = "978-1569319000",
                        score = 10,
                        EndingYear = DateTime.Now,
                        StatusInCountryOfOrigin = "11, completed",
                        Type = "Manga",
                        OriginalPublisher = "SquareEnix",
                        orignalWebtoon = null,

                    },
                    GroupScanlating = null,
                    GroupScanlatingID = null,
                    userModels = null,
                    userId = null,
                };

                MasterModel masterModel2 = new MasterModel
                {
                    MangaModels = new MangaModel
                    {
                        MangaName = "Berserk",

                        PhotoPath = filePath,
                        ReleaseYear = DateTime.Now,
                        BlogModel = new BlogModel { mangaName = "Berserk" },
                        OfficialTranslations = "Japanese",
                        Description = "A broken man",
                        CompletelyTranslated = "Ongoing",
                        ISBN10 = "1506727549",
                        ISBN13 = "978-1506717913",
                        score = 10,
                        EndingYear = DateTime.Now,
                        StatusInCountryOfOrigin = "11, completed",
                        Type = "Manga",
                        OriginalPublisher = "SquareEnix",
                        orignalWebtoon = null,
                    },
                    GroupScanlating = null,
                    GroupScanlatingID = null,
                    userModels = null,
                    userId = null,
                };
                context.MasterModels.Add(masterModel);
                context.MasterModels.Add(masterModel2);
                context.SaveChanges();

                var result = await userManager.CreateAsync(user, "Rojhat123!");
                if (result.Succeeded)
                { // adds a role owner to the testobject
                    var resultRole = await userManager.AddToRoleAsync(user, "Owner");
                }
                CreatingAuthors();
            }

        }
    }
}
