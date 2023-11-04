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

        public UserRegisterModel(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IMangaRepository mangaRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

            this.mangaRepository = mangaRepository;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //this might be wrong, as we are creating a userModel and we're looking for identityUsers. Need to check this later
                var usermail = await userManager.FindByEmailAsync(Model.Email);
                if (usermail == null)
                {
                    var user = new UserModel { UserName = Model.Email, Email = Model.Email, Allias = Model.Allias, CreatedAcc = DateTime.UtcNow };

                    var result = await userManager.CreateAsync(user, Model.Password);

                    if (result.Succeeded)
                    {
                        // Create and associate UserSettings with the new user
                        var userSettings = new UserSettings
                        {
                            UserModelId = user.Id, // Assuming Id is the primary key for UserModel
                            ShowMatureContent = false, // Set the default value for this setting
                            ReadingDirection = MangaReadingDirection.LeftToRight,
                            DarkModeEnabled = true,

                            FontSize = 14, // Set default value
                            ItemsPerPage = 20, // Set default value

                            // Initialize collections if required
                            BlockedUsers = new HashSet<UserBlock>(),
                            PreferredLanguages = new HashSet<Languages>()
                        };

                        user.UserSettings = userSettings; // Associate UserSettings with the UserModel

                        // Optional: Save the UserSettings if not using cascading adds
                        context.UserSettings.Add(userSettings);

                        await signInManager.SignInAsync(user, isPersistent: false);
                        await signInManager.RefreshSignInAsync(user); // Refresh the sign-in
                        var resultToRole = await userManager.AddToRoleAsync(user, "Owner");

                        // Save any changes made to the context
                        await context.SaveChangesAsync();

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
            GetAllBooks = await mangaRepository.Get10MangaModelAsync();
            AllBooksList = GetAllBooks.ToList();
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