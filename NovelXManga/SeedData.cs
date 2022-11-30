
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
        public async void seedData()
        {
            context.Database.EnsureCreated();
            if (!context.mangaModels.Any())
            {
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/MangaImage/0f3f5666-7cb9-4713-a779-f1e1546a0d5f", User.userPhotoPath);
                var user = new UserModel { UserName = "TestUSer", Email = "TestUser@hotmail.com", userPhotoPath = filePath };
                var result = await userManager.CreateAsync(user, "Rojhat123!");
                var author = new AuthorModel { UserName = "TestAuthor", Email = "TestAuthor@hotmail.com", FirstName = "TestAuthor", LastName = "Sefdin", userPhotoPath = filePath };
                var authorresult = await userManager.CreateAsync(author, "Author123!");

                MasterModel masterModel = new MasterModel
                {
                    MangaModels = new MangaModel
                    {
                        MangaName = "Naruto",
                        AssociatedNames = "Naruto Shippuden",
                        PhotoPath = filePath,
                        ReleaseYear = DateTime.Now,
                        BlogModel = new BlogModel { mangaName = "Naruto" },
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
                        AssociatedNames = "Berserk Max",
                        PhotoPath = "C:\\Users\\Rojhat\\source\\repos\\NovelXManga\\NovelXManga\\wwwroot\\Images\\MangaImage\\0f3f5666-7cb9-4713-a779-f1e1546a0d5f.png",
                        ReleaseYear = DateTime.Now,
                        BlogModel = new BlogModel { mangaName = "Berserk" },
                    },
                    GroupScanlating = null,
                    GroupScanlatingID = null,
                    userModels = null,
                    userId = null,
                };
                IdentityRole identityRole = new IdentityRole
                {
                    Name = "Admin"
                };
                IdentityResult resultRole = await roleManager.CreateAsync(identityRole);
                IdentityRole identityRole2 = new IdentityRole
                {
                    Name = "NormalUser"
                };
                IdentityResult resultRole2 = await roleManager.CreateAsync(identityRole2);
                IdentityRole ownerRole = new IdentityRole
                {
                    Name = "Owner"
                };
                IdentityResult resultRole3 = await roleManager.CreateAsync(ownerRole);

                context.MasterModels.Add(masterModel);
                context.MasterModels.Add(masterModel2);
                context.SaveChanges();


            }
        }
    }
}
