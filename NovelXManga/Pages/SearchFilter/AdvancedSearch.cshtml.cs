using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

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
        public string SearchBookTitle { get; set; }

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
        [FromForm(Name = "JsSelectedTags")]
        public List<int> SelectedTags { get; set; }

        [BindProperty]
        [FromForm(Name = "NegativeSelectedTags")]
        public List<int> NegativeSelectedTags { get; set; }

        [BindProperty]
        [FromForm(Name = "PositiveSelectedGenres")]
        public List<int> PositiveSelectedGenres { get; set; }

        [BindProperty]
        [FromForm(Name = "NegativeSelectedGenres")]
        public List<int> NegativeSelectedGenres { get; set; }

        [BindProperty]
        public string TagInclusionMode { get; set; }

        [BindProperty]
        public string TagExclusionMode { get; set; }

        [BindProperty]
        public string GenreInclusionMode { get; set; }

        [BindProperty]
        public string GenreExclusionMode { get; set; }

        public List<TagModel> Tags { get; set; }
        public List<GenresModel> Genres { get; set; }
        public IEnumerable<MangaModel> MangaModels { get; set; }
        public string SearchTerm { get; set; }
        public int CurrentPage { get; set; }
        public int MoreBooksClicks { get; set; }
        public int TotalPages { get; set; }
        public List<int> JsSelectedTags { get; set; }

        public string Sanitize(string input)
        {
            return WebUtility.HtmlEncode(input);
        }

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

        public bool IsYearInRange(DateTime? date, int minYear, int maxYear)
        {
            return date.HasValue && date.Value.Year >= minYear && date.Value.Year <= maxYear;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                CurrentPage = 1; // Set from query string or as a parameter
                MoreBooksClicks = 8;  // Number of items per page
                //Strings searches
                _httpContextAccessor.HttpContext.Session.SetString("TagInclusionMode", TagInclusionMode);
                _httpContextAccessor.HttpContext.Session.SetString("TagExclusionMode", TagExclusionMode);
                _httpContextAccessor.HttpContext.Session.SetString("GenreInclusionMode", GenreInclusionMode);
                _httpContextAccessor.HttpContext.Session.SetString("GenreExclusionMode", GenreExclusionMode);
                //Genres
                PositiveSelectedGenres = PositiveSelectedGenres ?? new List<int>();
                NegativeSelectedGenres = NegativeSelectedGenres ?? new List<int>();
                //tags
                SelectedTags = SelectedTags ?? new List<int>();
                NegativeSelectedTags = NegativeSelectedTags ?? new List<int>();
                var selectedTagsSerialized = JsonSerializer.Serialize(SelectedTags);
                var selectedGenresSerialized = JsonSerializer.Serialize(PositiveSelectedGenres);
                var negativesSlectedTagsSerialized = JsonSerializer.Serialize(NegativeSelectedTags);
                var negativesSelectedGenresSerialized = JsonSerializer.Serialize(NegativeSelectedGenres);

                var search = !string.IsNullOrEmpty(SearchBookTitle) ? HtmlEncoder.Default.Encode(SearchBookTitle) : SearchBookTitle;
                var searchAuthor = !string.IsNullOrEmpty(SearchAuthor) ? HtmlEncoder.Default.Encode(SearchAuthor) : SearchAuthor;
                var searchArtist = !string.IsNullOrEmpty(SearchArtist) ? HtmlEncoder.Default.Encode(SearchArtist) : SearchArtist;
                var searchVoiceActor = !string.IsNullOrEmpty(SearchVoiceActor) ? HtmlEncoder.Default.Encode(SearchVoiceActor) : SearchVoiceActor;
                var searchCharacter = !string.IsNullOrEmpty(SearchCharacter) ? HtmlEncoder.Default.Encode(SearchCharacter) : SearchCharacter;

                // Input Validation
                if (!string.IsNullOrEmpty(search) && !Regex.IsMatch(search, @"^[\p{L}\p{N}\s]+$") ||
                    !string.IsNullOrEmpty(searchAuthor) && !Regex.IsMatch(searchAuthor, @"^[\p{L}\p{N}\s]+$") ||
                    !string.IsNullOrEmpty(searchArtist) && !Regex.IsMatch(searchArtist, @"^[\p{L}\p{N}\s]+$") ||
                    !string.IsNullOrEmpty(searchVoiceActor) && !Regex.IsMatch(searchVoiceActor, @"^[\p{L}\p{N}\s]+$") ||
                    !string.IsNullOrEmpty(searchCharacter) && !Regex.IsMatch(searchCharacter, @"^[\p{L}\p{N}\s]+$"))
                {
                    return Page();
                }

                _httpContextAccessor.HttpContext.Session.SetString("SelectedTags", selectedTagsSerialized);
                _httpContextAccessor.HttpContext.Session.SetString("SelectedGenres", selectedGenresSerialized);

                var query = context.mangaModels
                    .Include(m => m.Authormodels)
                    .Include(m => m.ArtistModels)
                    .Include(m => m.GenresModels)
                    .Include(m => m.TagsModels)
                    .Include(m => m.Characters)
                    .AsNoTracking()
                    .AsQueryable();

                if (!string.IsNullOrEmpty(search))
                    query = query.Where(m => m.MangaName.Contains(search));

                if (!string.IsNullOrEmpty(searchAuthor))
                    query = query.Where(m => m.Authormodels.Any(a => a.FirstName.Contains(searchAuthor) || a.LastName.Contains(searchAuthor)));

                if (!string.IsNullOrEmpty(searchArtist))
                    query = query.Where(m => m.ArtistModels.Any(a => a.FirstName.Contains(searchArtist) || a.LastName.Contains(searchArtist)));

                if (!string.IsNullOrEmpty(searchVoiceActor))
                    query = query.Where(m => m.VoiceActors.Any(a => a.FirstName.Contains(searchVoiceActor) || a.LastName.Contains(searchVoiceActor)));

                if (!string.IsNullOrEmpty(searchCharacter))
                    query = query.Where(m => m.Characters.Any(a => a.CharacterName.Contains(searchCharacter)));

                if (SearchReleaseYearStart.HasValue && IsYearInRange(SearchReleaseYearStart, 0001, DateTime.Now.Year))
                {
                    query = query.Where(m => m.ReleaseYear >= SearchReleaseYearStart.Value);
                }

                if (SearchReleaseYearEnd.HasValue && IsYearInRange(SearchReleaseYearEnd, 0001, DateTime.Now.Year))
                {
                    query = query.Where(m => m.ReleaseYear <= SearchReleaseYearEnd.Value || (m.EndingYear == null && DateTime.Now <= SearchReleaseYearEnd.Value));
                }

                if (PositiveSelectedGenres.Count > 0)
                {
                    if (GenreInclusionMode == "And")
                    {
                        var mangaIds = context.mangaModels
                      .Select(m => new { m.MangaID, GenresId = m.GenresModels.Select(t => t.GenresId) })
                      .AsEnumerable()
                      .Where(m => PositiveSelectedGenres.All(t => m.GenresId.Contains(t)))
                       .Select(m => m.MangaID)
                       .ToList();

                        query = query.Where(m => mangaIds.Contains(m.MangaID));
                    }
                    else // "Or"
                    {
                        query = query.Where(m => m.GenresModels.Any(g => PositiveSelectedGenres.Contains(g.GenresId)));
                    }
                }

                if (SelectedTags.Count > 0)
                {
                    if (TagInclusionMode == "And")
                    {
                        var mangaIds = context.mangaModels
    .Select(m => new { m.MangaID, TagIds = m.TagsModels.Select(t => t.TagId) })
    .AsEnumerable()
    .Where(m => SelectedTags.All(t => m.TagIds.Contains(t)))
    .Select(m => m.MangaID)
    .ToList();

                        query = query.Where(m => mangaIds.Contains(m.MangaID));
                    }
                    else // "Or"
                    {
                        query = query.Where(m => m.TagsModels.Any(t => SelectedTags.Contains(t.TagId)));
                    }
                }
                if (NegativeSelectedGenres.Count > 0)
                {
                    if (GenreExclusionMode == "And")
                    {
                        if (PositiveSelectedGenres.Count == 0)
                        {
                            var excludeMangaIds = context.mangaModels
                                .Select(m => new { m.MangaID, GenresId = m.GenresModels.Select(t => t.GenresId) })
                                .AsEnumerable()
                                .Where(m => !NegativeSelectedGenres.All(t => m.GenresId.Contains(t)))
                                .Select(m => m.MangaID)
                                .ToList();

                            query = query.Where(m => excludeMangaIds.Contains(m.MangaID));
                        }
                        else
                        {
                            query = query.Where(m => !m.GenresModels.Any(g => NegativeSelectedGenres.Contains(g.GenresId)));
                        }
                    }
                    else // "Or"
                    {
                        query = query.Where(m => !m.GenresModels.Any(g => NegativeSelectedGenres.Contains(g.GenresId)));
                    }
                }
                if (NegativeSelectedTags.Count > 0)
                {
                    if (TagExclusionMode == "And")
                    {
                        if (SelectedTags.Count == 0)
                        {
                            var excludeMangaIds = context.mangaModels
                                .Select(m => new { m.MangaID, TagId = m.TagsModels.Select(t => t.TagId) })
                                .AsEnumerable()
                                .Where(m => !NegativeSelectedTags.All(t => m.TagId.Contains(t)))
                                .Select(m => m.MangaID)
                                .ToList();

                            query = query.Where(m => excludeMangaIds.Contains(m.MangaID));
                        }
                        else
                        {
                            query = query.Where(m => !m.TagsModels.Any(g => NegativeSelectedTags.Contains(g.TagId)));
                        }
                    }
                    else // "Or"
                    {
                        query = query.Where(m => !m.TagsModels.Any(g => NegativeSelectedTags.Contains(g.TagId)));
                    }
                }
                var list = query.ToList();
                list = list.Where(m => m.GenresModels.Any(g => PositiveSelectedGenres.Contains(g.GenresId))).ToList();
                list = list.Where(m => m.TagsModels.Any(t => SelectedTags.Contains(t.TagId))).ToList();

                var sql = query.ToQueryString();

                TotalPages = (int)Math.Ceiling(await query.CountAsync() / (double)MoreBooksClicks);
                _httpContextAccessor.HttpContext.Session.SetInt32("TotalPages", TotalPages);
                _httpContextAccessor.HttpContext.Session.SetString("IsSearchClicked", "true");

                query = query.Skip((CurrentPage - 1) * MoreBooksClicks).Take(MoreBooksClicks);
                MangaModels = await query.ToListAsync();

                Tags = await context.TagModels.ToListAsync();
                Genres = await context.GenresModels.ToListAsync();

                return Page();
            }
            catch (Exception ex)
            {
                // Log the exception details.
                // Consider using a logging library like NLog, Serilog, or even ILogger here.

                Tags = await context.TagModels.ToListAsync();
                Genres = await context.GenresModels.ToListAsync();
                return Page(); // Return to the current page to display an error message to the user.
            }
        }

        public async Task<IActionResult> OnGetAsync(string searchString)
        {
            Tags = await context.TagModels.ToListAsync();
            Genres = await context.GenresModels.ToListAsync();
            TagInclusionMode = _httpContextAccessor.HttpContext.Session.GetString("TagInclusionMode") ?? "And";
            TagExclusionMode = _httpContextAccessor.HttpContext.Session.GetString("TagExclusionMode") ?? "Or";
            GenreInclusionMode = _httpContextAccessor.HttpContext.Session.GetString("GenreInclusionMode") ?? "And";
            GenreExclusionMode = _httpContextAccessor.HttpContext.Session.GetString("GenreExclusionMode") ?? "Or";
            // Retrieve selected tags and genres from session
            var selectedTagsSession = _httpContextAccessor.HttpContext.Session.GetString("SelectedTags");
            var selectedGenresSession = _httpContextAccessor.HttpContext.Session.GetString("SelectedGenres");

            if (!string.IsNullOrEmpty(selectedTagsSession))
            {
                SelectedTags = JsonSerializer.Deserialize<List<int>>(selectedTagsSession);
            }

            if (!string.IsNullOrEmpty(selectedGenresSession))
            {
                PositiveSelectedGenres = JsonSerializer.Deserialize<List<int>>(selectedGenresSession);
            }
            //MangaModels = await mangaRepository.GetAllModelAsync();

            return Page();
        }

        public async Task<IActionResult> OnGetBooksPage(int currentPage, int pageSiz)
        {
            try
            {
                var query = context.mangaModels.AsQueryable();
                int totalCount = await query.CountAsync();
                query = query.Skip((currentPage - 1) * pageSiz).Take(pageSiz);
                var mangaModels = await query.ToListAsync();

                int totalPages = (int)Math.Ceiling(totalCount / (double)pageSiz);

                return new JsonResult(new { CurrentPage = currentPage, Books = mangaModels, TotalPages = totalPages });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}