using MangaAccessService;
using MangaModelService;
using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

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

        [BindProperty]
        [Required(ErrorMessage = "Please select a role.")]
        public string Role { get; set; }

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
                    var user = new UserModel { UserName = Model.Email, Email = Model.Email, Allias = Model.Allias };
                    var result = await userManager.CreateAsync(user, Model.Password);

                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        await signInManager.RefreshSignInAsync(user); // Refresh the sign-in
                        var resultToRole = await userManager.AddToRoleAsync(user, Role);
                        return RedirectToPage("/Index");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    GetAllBooks = await mangaRepository.GetAllModelAsync();
                    AllBooksList = GetAllBooks.ToList();
                    ModelState.AddModelError(string.Empty, "Email is already in use");
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            GetAllBooks = await mangaRepository.GetAllModelAsync();
            AllBooksList = GetAllBooks.ToList();

            return Page();
        }
    }
}