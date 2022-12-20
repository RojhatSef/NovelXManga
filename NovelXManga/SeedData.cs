
using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;

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
        public AssociatedNames names(string AssociatedNames)
        {
            AssociatedNames associatedNames = new AssociatedNames();
            associatedNames.nameString = AssociatedNames;

            return associatedNames;
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

                var NewArtist = new ArtistModel
                {
                    FirstName = "Masashi ",
                    LastName = "Kishimoto",
                    Biography = "Graduated from the Faculty of Arts, Kyushu Sangyo University. \r\n\r\nMarried in 2003 and has children.\r\n\r\nKishimoto had a deal with Jump editorial department that he will receive Toriyama Akira's autographed paper as a reward when the serialization of Naruto reaches the third anniversary.\r\n\r\nHe and Oda Eiichiro are both rival and close friend.\r\n\r\nHis younger twin brother KISHIMOTO Seishi is 666 Satan’s author. Their art has been remarked as being very similar and accusations of plagiarism were made, either that Seishi had copied his brother or vice versa. However, Seishi notes in one of the volumes of his manga that the similarities are not intentional and that the occurrence would have been likely because they have been influenced by many of the same things. After several accusations, Masashi Kishimoto has asked that fans stop calling his brother a \"copy-cat\". His former assistants were ITAKURA Yuuichi, TASAKA Ryou and KAJISA Osamu.\r\nWhile IKEMOTO Mikio, TAIRA Kenji and OKUBO Akira were his assistants and now are his collaborators.\r\n\r\nHis favorite manga and anime are Dragon Ball, Yu Yu Hakusho, HUNTER x HUNTER, Ninku, AKIRA, Ghost in the Shell & Jin-Roh"
                   ,
                    BirthPlace = "Japan",
                    Gender = "Male",
                    WorkingAt = "Shueisha"
                };
                var newAuthor = new AuthorModel
                {
                    FirstName = "Masashi ",
                    LastName = "Kishimoto",
                    Biography = "Graduated from the Faculty of Arts, Kyushu Sangyo University. \r\n\r\nMarried in 2003 and has children.\r\n\r\nKishimoto had a deal with Jump editorial department that he will receive Toriyama Akira's autographed paper as a reward when the serialization of Naruto reaches the third anniversary.\r\n\r\nHe and Oda Eiichiro are both rival and close friend.\r\n\r\nHis younger twin brother KISHIMOTO Seishi is 666 Satan’s author. Their art has been remarked as being very similar and accusations of plagiarism were made, either that Seishi had copied his brother or vice versa. However, Seishi notes in one of the volumes of his manga that the similarities are not intentional and that the occurrence would have been likely because they have been influenced by many of the same things. After several accusations, Masashi Kishimoto has asked that fans stop calling his brother a \"copy-cat\". His former assistants were ITAKURA Yuuichi, TASAKA Ryou and KAJISA Osamu.\r\nWhile IKEMOTO Mikio, TAIRA Kenji and OKUBO Akira were his assistants and now are his collaborators.\r\n\r\nHis favorite manga and anime are Dragon Ball, Yu Yu Hakusho, HUNTER x HUNTER, Ninku, AKIRA, Ghost in the Shell & Jin-Roh"
                  ,
                    BirthPlace = "Japan",
                    Gender = "Male",
                    WorkingAt = "Shueisha"
                };
                var ListOfAssiocatedNames = names("Naruto Shippuden");
                var ListOfAssiocatedNames3 = names("Naluto");
                MasterModel masterModel = new MasterModel
                {
                    MangaModels = new MangaModel
                    {
                        MangaName = "Naruto",
                        AssociatedNames = new List<AssociatedNames> { ListOfAssiocatedNames, ListOfAssiocatedNames3 },
                        PhotoPath = filePath,
                        ReleaseYear = DateTime.Now,
                        BlogModel = new BlogModel { mangaName = "Naruto" },
                        OfficialTranslations = "Japanese",
                        Description = "A Kid who got something stuck in his stomach",
                        CompletelyTranslated = "Completed",
                        ArtistModels = new List<ArtistModel> { NewArtist },
                        Authormodels = new List<AuthorModel> { newAuthor },
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
                var ListOfAssiocatednames2 = names("Black Swordsman");
                MasterModel masterModel2 = new MasterModel
                {
                    MangaModels = new MangaModel
                    {
                        MangaName = "Berserk",
                        AssociatedNames = new List<AssociatedNames> { ListOfAssiocatednames2 },
                        PhotoPath = filePath,
                        ReleaseYear = DateTime.Now,
                        BlogModel = new BlogModel { mangaName = "Berserk" },
                        OfficialTranslations = "Japanese",
                        Description = "A broken man",
                        CompletelyTranslated = "Ongoing",
                        ISBN10 = "1506727549",
                        ISBN13 = "978-1506717913",
                        score = 10,
                        ArtistModels = new List<ArtistModel> { },
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


            }

        }
    }
}
