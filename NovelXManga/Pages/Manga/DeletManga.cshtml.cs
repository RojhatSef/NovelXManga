using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Manga
{
    public class DeletMangaModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;

        public DeletMangaModel(MangaNNovelAuthDBContext context, IMangaRepository mangaRepository)
        {
            this.context = context;
            this.mangaRepository = mangaRepository;
        }

        public MangaModel mangaModel { get; set; }

        public void OnGet(int id)
        {
            mangaModel = mangaRepository.GetManga(id);
        }

        public void OnPost(int id)
        {
            mangaModel = mangaRepository.GetOneMangaAllIncluded(id);
        }
    }
}