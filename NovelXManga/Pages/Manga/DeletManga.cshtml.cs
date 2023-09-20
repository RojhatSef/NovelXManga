using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Manga
{
    [Authorize(Roles = "Owner, Admin, AdminControl")]
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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            mangaModel = await mangaRepository.GetModelAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            mangaModel = await mangaRepository.GetOneMangaAllIncludedAsync(id);
            return Page();
        }
    }
}