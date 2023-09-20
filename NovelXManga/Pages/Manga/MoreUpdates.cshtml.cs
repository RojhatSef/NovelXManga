using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Manga
{
    public class MoreUpdatesModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;

        public MoreUpdatesModel(MangaNNovelAuthDBContext context, IMangaRepository mangaRepository)
        {
            this.context = context;
            this.mangaRepository = mangaRepository;
        }

        [BindProperty]
        public int PageNumber { get; set; }

        [BindProperty]
        public int PageSize { get; set; }

        public List<MangaModel> AllBooksList { get; set; }

        public IEnumerable<MangaModel> GetAllBooks { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 14)
        {
            AllBooksList = (await mangaRepository.GetPaginatedMangaModelsAsync(pageNumber, pageSize)).ToList();
            return Page();
        }
    }
}