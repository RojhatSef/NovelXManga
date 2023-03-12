using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NovelXManga.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        [TempData]
        public string SucessFulManga { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment, MangaNNovelAuthDBContext context)
        {
            _logger = logger;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
        }

        public IEnumerable<AssociatedNames> associatedNames { get; set; }
        public IEnumerable<MangaModel> GetAllBooks { get; set; }

        public void OnGet()
        {
            GetAllBooks = context.mangaModels.Include(e => e.GenresModels).Include(e => e.TagsModels);
        }
    }
}