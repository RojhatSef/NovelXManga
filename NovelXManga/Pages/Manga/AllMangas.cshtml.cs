using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Manga
{
    public class AllMangasModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext mangaNNovelAuthDBContext;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AllMangasModel(MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.mangaNNovelAuthDBContext = mangaNNovelAuthDBContext;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<MangaModel> GetAllBooks { get; set; }
        public void OnGet()
        {
            IEnumerable<MangaModel> GetAllBook = mangaRepository.GetAllManga();
            GetAllBooks = GetAllBook;

        }
    }
}
