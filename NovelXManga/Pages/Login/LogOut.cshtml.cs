using MangaAccessService;
using MangaAccessService.DTO.LoginRegiForgetDto;
using MangaModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Login
{
    [Authorize]
    [ValidateAntiForgeryToken]
    public class LogOutModel : PageModel
    {
        private readonly SignInManager<UserModel> signInManager;
        private readonly IMangaRepository mangaRepository;
        private readonly UserManager<UserModel> userManager;

        [BindProperty]
        public List<LoginRegiForgetCombineDto> AllBooksList { get; set; }

        public IEnumerable<LoginRegiForgetCombineDto> GetAllBooks { get; set; }

        public LogOutModel(SignInManager<UserModel> signInManager, IMangaRepository mangaRepository, UserManager<UserModel> userManager)
        {
            this.signInManager = signInManager;
            this.mangaRepository = mangaRepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            UserModel user = await userManager.GetUserAsync(User);
            GetAllBooks = await mangaRepository.Get10MangaEssentialMangaDtoIncludedAsync(user);
            AllBooksList = GetAllBooks.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostLogOutAsync()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostDontLogOutAsync()
        {
            return RedirectToPage("/Index");
        }
    }
}