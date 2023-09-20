using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Login
{
    public class LoginIndexModel : PageModel
    {
        private readonly SignInManager<UserModel> signInManager;
        private readonly UserManager<UserModel> userManager;
        private readonly IMangaRepository mangaRepository;

        [BindProperty]
        public LoginModel LoginModel { get; set; }

        public List<MangaModel> AllBooksList { get; set; }
        public IEnumerable<MangaModel> GetAllBooks { get; set; }
        public string ReturnUrl { get; set; }

        public LoginIndexModel(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager, IMangaRepository mangaRepository)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.mangaRepository = mangaRepository;
        }

        public async Task<IActionResult> OnGetAsync(string? returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");  // or any other appropriate page
            }

            ReturnUrl = returnUrl ?? Url.Content("/");
            ViewData["ReturnUrl"] = ReturnUrl;

            GetAllBooks = await mangaRepository.Get10MangaModelAsync();
            AllBooksList = GetAllBooks.ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");
            }

            returnUrl = returnUrl ?? Url.Content("/");

            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(LoginModel.Email, LoginModel.Password, LoginModel.RememberMe, false);

                if (identityResult.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(LoginModel.Email);
                    await signInManager.RefreshSignInAsync(user);

                    if (string.IsNullOrEmpty(returnUrl) || returnUrl == "/")
                    {
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        return RedirectToPage(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "username or password incorrect");
                }
            }

            GetAllBooks = await mangaRepository.Get10MangaModelAsync();
            AllBooksList = GetAllBooks.ToList();

            ReturnUrl = returnUrl;
            return RedirectToPage(new { ReturnUrl });
        }
    }
}