using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
            CurrentManga = Context.mangaModels.Include(e => e.GenresModels).Include(e => e.AllLanguages).Include(e => e.AssociatedNames).Include(e => e.RecommendedMangaModels).Include(e => e.OfficalWebsites).Include(e => e.Authormodels).Include(e => e.ArtistModels).Include(e => e.VoiceActors).Include(e => e.GroupScanlating).Include(e => e.userModels).Include(e => e.StudioModels).Include(e => e.Characters).Include(e => e.reviews).Include(e => e.BuyPages).FirstOrDefault(e => e.MangaID == id);
        }
    }
}