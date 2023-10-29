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

        [BindProperty]
        public List<LoginRegiForgetCombineDto> AllBooksList { get; set; }

        public IEnumerable<LoginRegiForgetCombineDto> GetAllBooks { get; set; }

        public LogOutModel(SignInManager<UserModel> signInManager, IMangaRepository mangaRepository)
        {
            this.signInManager = signInManager;
            this.mangaRepository = mangaRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            GetAllBooks = await mangaRepository.Get10MangaEssentialMangaDtoIncludedAsync();
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