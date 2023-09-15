using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Login
{
    [Authorize]
    public class LogOutModel : PageModel
    {
        private readonly SignInManager<UserModel> signInManager;
        private readonly IMangaRepository mangaRepository;
        public List<MangaModel> AllBooksList { get; set; }
        public IEnumerable<MangaModel> GetAllBooks { get; set; }

        public LogOutModel(SignInManager<UserModel> signInManager, IMangaRepository mangaRepository)
        {
            this.signInManager = signInManager;
            this.mangaRepository = mangaRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            GetAllBooks = await mangaRepository.GetAllModelAsync();
            AllBooksList = GetAllBooks.Take(10).ToList();
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