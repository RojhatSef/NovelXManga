using MangaAccessService;
using MangaModelService;
using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Register
{
    public class UserRegisterModel : PageModel
    {
        private readonly UserManager<UserModel> userManager;
        private readonly SignInManager<UserModel> signInManager;
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;

        [BindProperty]
        public RegisterViewModel Model { get; set; }

        public List<WallPapers> WallpaperList { get; set; }
        public IEnumerable<WallPapers> WallPapers { get; set; }
        public List<MangaModel> AllBooksList { get; set; }
        public IEnumerable<MangaModel> GetAllBooks { get; set; }

        public UserRegisterModel(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IMangaRepository mangaRepository, MangaNNovelAuthDBContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

            this.mangaRepository = mangaRepository;
            this.context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var usermail = await userManager.FindByEmailAsync(Model?.Email); // Added null-conditional operator
                if (usermail == null)
                {
                    var user = new UserModel
                    {
                        UserName = Model.Email,
                        Email = Model.Email,
                        Allias = Model.Allias,
                        CreatedAcc = DateTime.UtcNow
                    };

                    var result = await userManager.CreateAsync(user, Model.Password);
                    if (result.Succeeded)
                    {
                        // Initialize the user's settings after confirming the user is successfully created.
                        var userSettings = new UserSettings
                        {
                            UserModelId = user.Id,
                            ShowMatureContent = false,
                            DarkModeEnabled = true,
                            FontSize = 14,
                            ItemsPerPage = 20,
                        };

                        user.UserSettings = userSettings;
                        context.UserSettings.Add(userSettings);
                        // Save changes to the context
                        await context.SaveChangesAsync(); // Assuming context is not null

                        await signInManager.SignInAsync(user, isPersistent: false); // Assuming signInManager is not null
                        await userManager.AddToRoleAsync(user, "Owner"); // Assuming userManager is not null

                        return RedirectToPage("/Index");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is already in use");
                }
            }

            // Assuming mangaRepository is not null
            GetAllBooks = await mangaRepository.Get10MangaModelAsync();
            AllBooksList = GetAllBooks?.ToList(); // Added null-conditional operator

            return Page();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            GetAllBooks = await mangaRepository.Get10MangaModelAsync();
            AllBooksList = GetAllBooks.ToList();

            return Page();
        }
    }
}