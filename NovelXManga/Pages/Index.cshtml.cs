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

        // Part of the Search for the _Layout page. This code is for the Search bar
        //part of the old code
        public JsonResult OnGetSearchManga(string searchTerm)
        {
            var searchResults = context.mangaModels
                                .Where(m => m.MangaName.Contains(searchTerm) || m.ISBN10.Contains(searchTerm) || m.ISBN13.Contains(searchTerm))
                                .Select(m => new { m.MangaID, m.MangaName, m.PhotoPath })
                                .ToList();
            return new JsonResult(searchResults);
        }

        //public JsonResult OnGetSearchManga(string searchTerm)
        //{
        //    var mangaResults = context.mangaModels
        //                    .Where(m => m.MangaName.Contains(searchTerm) || m.ISBN10.Contains(searchTerm) || m.ISBN13.Contains(searchTerm))
        //                    .Select(m => new { m.MangaID, m.MangaName, m.PhotoPath, Type = "Manga" })
        //                    .ToList();

        //    // Similar searches for Authors, Artists, VoiceActors
        //    var authorResults = context.authorModels
        //                    .Where(a => a.FirstName.Contains(searchTerm) || a.LastName.Contains(searchTerm))
        //                    .Select(a => new { a.AuthorID, a.FirstName, a.LastName, Type = "Author" })
        //                    .ToList();

        //    var artistResults = context.artistModels
        //                    .Where(a => a.FirstName.Contains(searchTerm) || a.LastName.Contains(searchTerm))
        //                    .Select(a => new { a.ArtistId, a.FirstName, a.LastName, Type = "Artist" })
        //                    .ToList();

        //    var voiceActorResults = context.voiceActorModels
        //                    .Where(v => v.FirstName.Contains(searchTerm) || v.LastName.Contains(searchTerm))
        //                    .Select(v => new { v.VoiceActorId, v.FirstName, v.LastName, Type = "VoiceActor" })
        //                    .ToList();

        //    var searchResults = new List<object>();
        //    searchResults.AddRange(mangaResults);
        //    searchResults.AddRange(authorResults);
        //    searchResults.AddRange(artistResults);
        //    searchResults.AddRange(voiceActorResults);

        //    return new JsonResult(searchResults);
        //}

        public List<MangaModel> AllBooksList { get; set; }
        public IEnumerable<AssociatedNames> associatedNames { get; set; }
        public IEnumerable<MangaModel> GetAllBooks { get; set; }
        public IEnumerable<WallPapers> WallPapers { get; set; }
        public List<WallPapers> WallpaperList { get; set; }

        public void OnGet()
        {
            GetAllBooks = context.mangaModels.Include(e => e.GenresModels).Include(e => e.TagsModels).Include(e => e.ArtistModels).Include(e => e.Authormodels);
            AllBooksList = GetAllBooks.ToList();
            WallPapers = context.WallPapers;
            WallpaperList = WallPapers.ToList();
        }
    }
}