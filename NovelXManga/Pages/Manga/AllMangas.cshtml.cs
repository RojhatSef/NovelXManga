using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Manga
{
    public class AllMangasModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AllMangasModel(MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.context = mangaNNovelAuthDBContext;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<AssociatedNames> associatedNames { get; set; }
        public IEnumerable<MangaModel> GetAllBooks { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            GetAllBooks = await mangaRepository.GetAllModelAsync();
            return Page();
        }
    }
}