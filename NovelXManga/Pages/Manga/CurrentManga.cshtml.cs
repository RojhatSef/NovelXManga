using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Manga
{
    public class CurrentMangaModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext Context;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<IdentityUser> userManager;

        public MangaModel CurrentManga { get; set; }

        public CurrentMangaModel(UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment, IMangaRepository mangaRepository, MangaNNovelAuthDBContext mangaNNovelAuthDBContext)
        {
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.mangaRepository = mangaRepository;
            this.Context = mangaNNovelAuthDBContext;
        }

        public void OnGet(int id)
        {
            CurrentManga = mangaRepository.GetOneMangaAllIncluded(id);
        }
    }
}