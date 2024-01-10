using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Manga
{
    public class AllMangasModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<UserModel> _userManager;
        private readonly CheckUserSettings _checkUserSettings;

        public AllMangasModel(MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment, UserManager<UserModel> userManager, CheckUserSettings checkUserSettings)
        {
            this.context = mangaNNovelAuthDBContext;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _checkUserSettings = checkUserSettings;
        }

        public IEnumerable<AssociatedNames> associatedNames { get; set; }
        public IEnumerable<MangaModel> GetAllBooks { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            GetAllBooks = await mangaRepository.GetAllUserModelAsync(currentUser);
            return Page();
        }
    }
}