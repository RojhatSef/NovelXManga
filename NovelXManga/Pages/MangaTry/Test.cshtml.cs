using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NovelXManga.Pages.MangaTry
{
    public class TestModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public TestModel(MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.context = mangaNNovelAuthDBContext;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<AssociatedNames> associatedNames { get; set; }
        public IEnumerable<MangaModel> GetAllBooks { get; set; }

        [BindProperty]
        public MangaModel CurrentManga { get; set; }

        public void OnGet()
        {
            GetAllBooks = context.mangaModels.Include(e => e.GenresModels).Include(e => e.TagsModels);
            CurrentManga = context.mangaModels.Include(e => e.GenresModels).Include(e => e.AllLanguages).Include(e => e.AssociatedNames).Include(e => e.RecommendedMangaModels).Include(e => e.OfficalWebsites).Include(e => e.Authormodels).Include(e => e.ArtistModels).Include(e => e.VoiceActors).Include(e => e.GroupScanlating).Include(e => e.userModels).Include(e => e.StudioModels).Include(e => e.Characters).Include(e => e.reviews).Include(e => e.BuyPages).FirstOrDefault(e => e.MangaID == 1);
        }
    }
}