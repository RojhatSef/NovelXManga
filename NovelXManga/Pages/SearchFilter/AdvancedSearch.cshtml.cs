using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace NovelXManga.Pages.SearchFilter
{
    public class AdvancedSearchModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;
        private readonly IAuthorRepsitory authorRepsitory;
        private readonly IArtistRepsitory artistRepsitory;
        private readonly ITagRepsitory tagRepsitory;
        private readonly IGenreRepsitory genreRepsitory;
        private readonly IChapterModelRepsitory chapterRepsitory;
        private readonly IAssociatedNamesRepsitory associatedNamesRepsitory;
        private readonly ILanguageRepsitory languageRepsitory;
        private readonly IStudioRepsitory studioRepsitory;
        private readonly IDistributedCache _cache; // remove if not needed
        private readonly IHttpContextAccessor _httpContextAccessor; // remove if not needed

        [BindProperty]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SearchReleaseYearStart { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SearchReleaseYearEnd { get; set; }

        [BindProperty]
        public string Search { get; set; }

        [BindProperty]
        public string SearchAuthor { get; set; }

        [BindProperty]
        public string SearchArtist { get; set; }

        [BindProperty]
        public string SearchVoiceActor { get; set; }

        [BindProperty]
        public string SearchCharacter { get; set; }

        [BindProperty]
        public MangaModel _MangaModel { get; set; }

        [BindProperty]
        [FromForm(Name = "selectedTags")]
        public List<int> SelectedTags { get; set; }

        [BindProperty]
        [FromForm(Name = "selectedGenres")]
        public List<int> SelectedGenres { get; set; }

        public List<TagModel> Tags { get; set; }
        public List<GenresModel> Genres { get; set; }
        public IEnumerable<MangaModel> MangaModels { get; set; }
        public string SearchTerm { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public AdvancedSearchModel(MangaNNovelAuthDBContext context, IMangaRepository mangaRepository, IAuthorRepsitory authorRepsitory, IArtistRepsitory artistRepsitory, ITagRepsitory tagRepsitory, IGenreRepsitory genreRepsitory, IChapterModelRepsitory chapterRepsitory, IAssociatedNamesRepsitory associatedNamesRepsitory, ILanguageRepsitory languageRepsitory, IDistributedCache cache, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.mangaRepository = mangaRepository;
            this.authorRepsitory = authorRepsitory;
            this.artistRepsitory = artistRepsitory;
            this.tagRepsitory = tagRepsitory;
            this.genreRepsitory = genreRepsitory;
            this.chapterRepsitory = chapterRepsitory;
            this.associatedNamesRepsitory = associatedNamesRepsitory;
            this.languageRepsitory = languageRepsitory;
            this.MangaModels = new List<MangaModel>();
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                int pageIndex = 1; // Set from query string or as a parameter
                int pageSize = 8;  // Number of items per page
                SelectedGenres = SelectedGenres ?? new List<int>();
                SelectedTags = SelectedTags ?? new List<int>();
                var selectedTagsSerialized = JsonSerializer.Serialize(SelectedTags); // remove if not needed
                var selectedGenresSerialized = JsonSerializer.Serialize(SelectedGenres); // remove if not needed

                _httpContextAccessor.HttpContext.Session.SetString("SelectedTags", selectedTagsSerialized);// remove if not needed
                _httpContextAccessor.HttpContext.Session.SetString("SelectedGenres", selectedGenresSerialized);// remove if not needed
                var query = context.mangaModels
             .Include(m => m.Authormodels)
             .Include(m => m.ArtistModels)
             .Include(m => m.GenresModels)
             .Include(m => m.TagsModels)
             .Include(m => m.Characters)
             .AsNoTracking()
             .AsQueryable();

                if (!string.IsNullOrEmpty(Search))
                    query = query.Where(m => m.MangaName.Contains(Search));

                if (!string.IsNullOrEmpty(SearchAuthor))
                    query = query.Where(m => m.Authormodels.Any(a => a.FirstName.Contains(SearchAuthor) || a.LastName.Contains(SearchAuthor)));

                if (!string.IsNullOrEmpty(SearchArtist))
                    query = query.Where(m => m.ArtistModels.Any(a => a.FirstName.Contains(SearchArtist) || a.LastName.Contains(SearchArtist)));

                if (!string.IsNullOrEmpty(SearchVoiceActor))
                    query = query.Where(m => m.VoiceActors.Any(a => a.FirstName.Contains(SearchVoiceActor) || a.LastName.Contains(SearchVoiceActor)));

                if (!string.IsNullOrEmpty(SearchCharacter))
                    query = query.Where(m => m.Characters.Any(a => a.CharacterName.Contains(SearchCharacter)));

                if (SearchReleaseYearStart.HasValue)
                    query = query.Where(m => m.ReleaseYear >= SearchReleaseYearStart.Value);

                if (SearchReleaseYearEnd.HasValue)
                    query = query.Where(m => m.ReleaseYear <= SearchReleaseYearEnd.Value);

                if (SelectedGenres.Count > 0)
                {
                    query = query.Where(m => m.GenresModels.Any(g => SelectedGenres.Contains(g.GenresId)));
                }

                if (SelectedTags.Count > 0)
                {
                    query = query.Where(m => m.TagsModels.Any(t => SelectedTags.Contains(t.TagId)));
                }
                var list = query.ToList();
                list = list.Where(m => m.GenresModels.Any(g => SelectedGenres.Contains(g.GenresId))).ToList();
                list = list.Where(m => m.TagsModels.Any(t => SelectedTags.Contains(t.TagId))).ToList();

                var sql = query.ToQueryString();

                TotalPages = (int)Math.Ceiling(await query.CountAsync() / (double)pageSize);
                MangaModels = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                Tags = await context.TagModels.ToListAsync();
                Genres = await context.GenresModels.ToListAsync();

                return Page();
            }
            catch (Exception ex)
            {
                // Log the exception details.
                // Consider using a logging library like NLog, Serilog, or even ILogger here.
                Console.WriteLine($"Exception caught: {ex.Message}");
                Console.WriteLine($"Stacktrace: {ex.StackTrace}");

                return Page(); // Return to the current page to display an error message to the user.
            }
        }

        public async Task<IActionResult> OnGetAsync(string searchString)
        {
            Tags = await context.TagModels.ToListAsync();
            Genres = await context.GenresModels.ToListAsync();

            // Retrieve selected tags and genres from session
            var selectedTagsSession = _httpContextAccessor.HttpContext.Session.GetString("SelectedTags");
            var selectedGenresSession = _httpContextAccessor.HttpContext.Session.GetString("SelectedGenres");

            if (!string.IsNullOrEmpty(selectedTagsSession))
            {
                SelectedTags = JsonSerializer.Deserialize<List<int>>(selectedTagsSession);
            }

            if (!string.IsNullOrEmpty(selectedGenresSession))
            {
                SelectedGenres = JsonSerializer.Deserialize<List<int>>(selectedGenresSession);
            }
            //MangaModels = await mangaRepository.GetAllModelAsync();

            return Page();
        }
    }
}