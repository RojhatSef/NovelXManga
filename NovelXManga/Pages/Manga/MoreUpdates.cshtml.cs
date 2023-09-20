using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NovelXManga.Pages.Manga
{
    public class MoreUpdatesModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;

        public MoreUpdatesModel(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public List<MangaModel> AllBooksList { get; set; }

        public IEnumerable<MangaModel> GetAllBooks { get; set; }


        public void OnGet()
        {
            GetAllBooks = context.mangaModels.Include(e => e.GenresModels).Include(e => e.TagsModels).Include(e => e.ArtistModels).Include(e => e.Authormodels);
            AllBooksList = GetAllBooks.ToList();

        }
    }
}