using MangaAccessService;
using MangaAccessService.DTO;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

//bug not FIXED OnGetBooksPage is being called when we do the OnPost.
// This makes the data fetch happen twice. THis is a bug, unsure how to fix it now. Not on high priority.
namespace NovelXManga.Pages.SearchFilter
{
    [ValidateAntiForgeryToken]
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
        private readonly UserManager<UserModel> userManager;
        private readonly IUserSettingsRepository _userSettingsRepository;

        #region Properties

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

        [BindProperty]
        public bool showAdultContentForGuests { get; set; }

        [BindProperty]
        public bool showMatureContentForGuests { get; set; }

        #endregion Properties

        public string Sanitize(string input)
        {
            return WebUtility.HtmlEncode(input);
        }

        public AdvancedSearchModel(MangaNNovelAuthDBContext context, IMangaRepository mangaRepository, IAuthorRepsitory authorRepsitory, IArtistRepsitory artistRepsitory, ITagRepsitory tagRepsitory, IGenreRepsitory genreRepsitory, IChapterModelRepsitory chapterRepsitory, IAssociatedNamesRepsitory associatedNamesRepsitory, ILanguageRepsitory languageRepsitory, IDistributedCache cache, IHttpContextAccessor httpContextAccessor, UserManager<UserModel> userManager)
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
            this.userManager = userManager;
        }

        private void DeserializeAndRetrieveSessionData()
        {
            var selectedTagsSerialized = _httpContextAccessor.HttpContext.Session.GetString("SelectedTags");
            SelectedTags = JsonSerializer.Deserialize<List<int>>(selectedTagsSerialized);

            var selectedGenresSerialized = _httpContextAccessor.HttpContext.Session.GetString("SelectedGenres");
            PositiveSelectedGenres = JsonSerializer.Deserialize<List<int>>(selectedGenresSerialized);

            var negativesSelectedTagsSerialized = _httpContextAccessor.HttpContext.Session.GetString("ExludedSelectedTags");
            NegativeSelectedTags = JsonSerializer.Deserialize<List<int>>(negativesSelectedTagsSerialized);

            var negativesSelectedGenresSerialized = _httpContextAccessor.HttpContext.Session.GetString("ExludedSelectedGenres");
            NegativeSelectedGenres = JsonSerializer.Deserialize<List<int>>(negativesSelectedGenresSerialized);

            TagInclusionMode = _httpContextAccessor.HttpContext.Session.GetString("TagInclusionMode");
            TagExclusionMode = _httpContextAccessor.HttpContext.Session.GetString("TagExclusionMode");
            GenreInclusionMode = _httpContextAccessor.HttpContext.Session.GetString("GenreInclusionMode");
            GenreExclusionMode = _httpContextAccessor.HttpContext.Session.GetString("GenreExclusionMode");
        }

        public bool IsYearInRange(DateTime? date, int minYear, int maxYear)
        {
            return date.HasValue && date.Value.Year >= minYear && date.Value.Year <= maxYear;
        }

        private void InitializeSettingsBookPages()
        {
            CurrentPage = 1;
            //book amounts for the first search click
            MoreBooksClicks = 8;
        }

        private void InitializeSettings()
        {
            PositiveSelectedGenres ??= new List<int>();
            NegativeSelectedGenres ??= new List<int>();
            SelectedTags ??= new List<int>();
            NegativeSelectedTags ??= new List<int>();
        }

        private void SetSessionState()
        {
            //Strings searches
            _httpContextAccessor.HttpContext.Session.SetString("TagInclusionMode", TagInclusionMode);
            _httpContextAccessor.HttpContext.Session.SetString("TagExclusionMode", TagExclusionMode);
            _httpContextAccessor.HttpContext.Session.SetString("GenreInclusionMode", GenreInclusionMode);
            _httpContextAccessor.HttpContext.Session.SetString("GenreExclusionMode", GenreExclusionMode);
        }

        private void SerializeAndStoreSessionData()
        {
            var selectedTagsSerialized = JsonSerializer.Serialize(SelectedTags);
            var selectedGenresSerialized = JsonSerializer.Serialize(PositiveSelectedGenres);
            var negativesSlectedTagsSerialized = JsonSerializer.Serialize(NegativeSelectedTags);
            var negativesSelectedGenresSerialized = JsonSerializer.Serialize(NegativeSelectedGenres);
            _httpContextAccessor.HttpContext.Session.SetString("SelectedTags", selectedTagsSerialized);
            _httpContextAccessor.HttpContext.Session.SetString("SelectedGenres", selectedGenresSerialized);

            //unsure might not needed
            _httpContextAccessor.HttpContext.Session.SetString("ExludedSelectedTags", negativesSlectedTagsSerialized);
            _httpContextAccessor.HttpContext.Session.SetString("ExludedSelectedGenres", negativesSelectedGenresSerialized);
        }

        private string EncodeIfNotEmpty(string input)
        {
            return !string.IsNullOrEmpty(input) ? HtmlEncoder.Default.Encode(input) : input;
        }

        private bool IsInputValid(string input)
        {
            return string.IsNullOrEmpty(input) || Regex.IsMatch(input, @"^[\p{L}\p{N}\s]+$");
        }

        private IQueryable<MangaModel> ApplyAdditionalFilters(IQueryable<MangaModel> query)
        {
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
            return query;
        }

        private IQueryable<MangaModel> ApplySearchFilters(IQueryable<MangaModel> query)
        {
            // Validate and encode search strings
            var search = EncodeIfNotEmpty(SearchBookTitle);
            var searchAuthor = EncodeIfNotEmpty(SearchAuthor);
            var searchArtist = EncodeIfNotEmpty(SearchArtist);
            var searchVoiceActor = EncodeIfNotEmpty(SearchVoiceActor);

            if (!IsInputValid(search) || !IsInputValid(searchAuthor) || !IsInputValid(searchArtist) ||
                !IsInputValid(searchVoiceActor))
            {
                return query.Where(m => false); // Invalid inputs
            }

            // Apply search filters
            if (!string.IsNullOrEmpty(search))
                query = query.Where(m => m.MangaName.Contains(search));

            if (!string.IsNullOrEmpty(searchAuthor))
                query = query.Where(m => m.Authormodels.Any(a => a.FirstName.Contains(searchAuthor) || a.LastName.Contains(searchAuthor)));
            if (!string.IsNullOrEmpty(searchArtist))
                query = query.Where(m => m.ArtistModels.Any(a => a.FirstName.Contains(searchArtist) || a.LastName.Contains(searchArtist)));

            if (!string.IsNullOrEmpty(searchVoiceActor))
                query = query.Where(m => m.VoiceActors.Any(a => a.FirstName.Contains(searchVoiceActor) || a.LastName.Contains(searchVoiceActor)));

            if (SearchReleaseYearStart.HasValue && IsYearInRange(SearchReleaseYearStart, 0001, DateTime.Now.Year))
            {
                query = query.Where(m => m.ReleaseYear >= SearchReleaseYearStart.Value);
            }

            if (SearchReleaseYearEnd.HasValue && IsYearInRange(SearchReleaseYearEnd, 0001, DateTime.Now.Year))
            {
                query = query.Where(m => m.ReleaseYear <= SearchReleaseYearEnd.Value || (m.EndingYear == null && DateTime.Now <= SearchReleaseYearEnd.Value));
            }
            return query;
        }

        private async Task ExecuteQueryAndSetResults(IQueryable<MangaModel> query)
        {
            TotalPages = (int)Math.Ceiling(await query.CountAsync() / (double)MoreBooksClicks);
            _httpContextAccessor.HttpContext.Session.SetInt32("TotalPages", TotalPages);
            _httpContextAccessor.HttpContext.Session.SetString("IsSearchClicked", "true");

            query = query.Skip((CurrentPage - 1) * MoreBooksClicks).Take(MoreBooksClicks);
            MangaModels = await query.ToListAsync();

            Tags = await context.TagModels.ToListAsync();
            Genres = await context.GenresModels.ToListAsync();
        }

        private IQueryable<MangaModel> BuildQueryWithFilters()
        {
            InitializeSettingsBookPages();
            DeserializeAndRetrieveSessionData();
            InitializeSettings();
            SetSessionState();
            SerializeAndStoreSessionData();
            var query = BuildInitialQuery();
            query = ApplySearchFilters(query);
            query = ApplyAdditionalFilters(query);
            return query;
        }

        private IQueryable<MangaModel> BuildInitialQuery()
        {
            return context.mangaModels.AsNoTracking().AsQueryable();
            //return context.mangaModels
            //            .Include(m => m.Authormodels)
            //            .Include(m => m.ArtistModels)
            //            .Include(m => m.GenresModels)
            //            .Include(m => m.TagsModels)

            //            .AsNoTracking()
            //            .AsQueryable();
        }

        private IQueryable<MangaModel> AddIncludes(IQueryable<MangaModel> query)
        {
            return query.Include(m => m.Authormodels)
                        .Include(m => m.ArtistModels)
                        .Include(m => m.GenresModels)
                        .Include(m => m.TagsModels);
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

        public async Task<UserSettingsDTO> GetUserSettingsAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await userManager.GetUserAsync(User);
                var userSettings = await _userSettingsRepository.GetAsync(currentUser.Id);
                return new UserSettingsDTO
                {
                    UserModelId = userSettings.UserModelId,
                    ShowAdultContent = userSettings.showAdultContent,
                    ShowMatureContent = userSettings.ShowMatureContent
                };
            }
            else
            {
                // For guests, use the settings they selected on the page (if provided)
                return new UserSettingsDTO { ShowAdultContent = showAdultContentForGuests, ShowMatureContent = showMatureContentForGuests };
            }
        }

        private string[] adultSensetivContent = {
    "Adult", "Mature", "Ecchi", "Hentai", "Smut","Horror","Psychological",
    "Rape", "Sexual Abuse", "BDSM", "Erotic", "Anal Intercourse",  "Anilingus", "Attempted Murder", "Attempted Rape",
     "Attempted Suicide", "Bathroom Intercourse", "Big Breasts","Blackmail", "Blood and Gore","Bondage","Borderline H",
      "Caught in the Act", "Child Abuse", "Cunnilingus","Double Penetration", "Drunken Intercourse","Dubious Consent",
      "Exhibitionism","Fellatio", "Fetishes","First-Time Intercourse", "Futanari", "Gang Rape","Glasses-Wearing Uke God",
       "Groping", "Group Intercourse", "Handjob","Incest",  "Lust", "Mind Break", "Mind Control", "Murder","Netorare",
        "Netor", "Netori", "Older Seme Younger Uke","Older Uke Younger Seme", "Outdoor Intercourse", "Paizuri", "Titty Fuck", "Panchira",
         "Prostitute", "Prostitution", "Public Sex","Sadist","School Intercourse","Sex Addict", "Sex Friends Become Lovers", "Sex Toy", "Sex", "Sexual Abuse",
           "Straight Seme", "Straight Uke","Suicide", "Threesome", "Undergarment","Urination", "Lolicon", "Shotacon"

}; private string[] MatureSensetivContent = {

    "Adult", "Hentai", "Smut",
    "Rape", "Sexual Abuse",  "Erotic", "Anal Intercourse",  "Anilingus", "Attempted Murder",
     "Bathroom Intercourse", "Big Breasts","Bondage",
      "Caught in the Act",  "Cunnilingus","Double Penetration", "Drunken Intercourse",
      "Exhibitionism","Fellatio", "Fetishes","First-Time Intercourse", "Futanari", "Gang Rape",
       "Groping", "Group Intercourse", "Handjob","Incest",  "Lust", "Mind Break", "Mind Control", "Netorare",
        "Netor", "Netori","Outdoor Intercourse", "Paizuri", "Titty Fuck", "Panchira","Urination",
        "Public Sex","Sadist","School Intercourse","Sex Addict", "Sex Friends Become Lovers", "Sex Toy", "Sex", "Sexual Abuse", "Lolicon", "Shotacon"
};

        private IQueryable<MangaModel> ApplyMatureContentFilter(IQueryable<MangaModel> query, bool showMatureContent)
        {
            if (!showMatureContent)
            {
                // Assuming MatureSensitiveContent is a list of tag names considered mature
                query = query.Where(m => !m.GenresModels.Any(g => MatureSensetivContent.Contains(g.GenreName))
                                         && !m.TagsModels.Any(t => MatureSensetivContent.Contains(t.TagName)));
            }
            return query;
        }

        private IQueryable<MangaModel> ApplyAdultContentFilter(IQueryable<MangaModel> query, bool showAdultContent)
        {
            if (!showAdultContent)
            {
                // Assuming AdultSensitiveContent is a list of tag or genre names considered adult
                query = query.Where(m => !m.GenresModels.Any(g => adultSensetivContent.Contains(g.GenreName))
                                         && !m.TagsModels.Any(t => adultSensetivContent.Contains(t.TagName)));
            }
            return query;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // Reset ItemsAlreadyShown to 0
                _httpContextAccessor.HttpContext.Session.SetInt32("ItemsAlreadyShown", 0);
                InitializeSettingsBookPages();
                InitializeSettings();
                SetSessionState();
                SerializeAndStoreSessionData();

                UserSettingsDTO userSettings = await GetUserSettingsAsync();

                // Store user settings in session for retrieval in OnGetBooksPage
                var userSettingsSerialized = JsonSerializer.Serialize(userSettings);
                _httpContextAccessor.HttpContext.Session.SetString("UserSettings", userSettingsSerialized);

                var query = BuildInitialQuery();

                // Apply content filters based on user settings
                query = ApplyMatureContentFilter(query, userSettings.ShowMatureContent);
                query = ApplyAdultContentFilter(query, userSettings.ShowAdultContent);

                // Apply search and additional filters
                query = ApplySearchFilters(query);
                query = ApplyAdditionalFilters(query);

                // Execute query and set results
                await ExecuteQueryAndSetResults(query);

                return Page();
            }
            catch (Exception ex)
            {
                // Log the exception details and return to the current page
                Tags = await context.TagModels.ToListAsync();
                Genres = await context.GenresModels.ToListAsync();
                return Page();
            }
        }

        // New JS fetch for the more button
        public async Task<IActionResult> OnGetBooksPage(int currentPage, int pageSiz)
        {
            try
            {
                var ItemsAlreadyShown = _httpContextAccessor.HttpContext.Session.GetInt32("ItemsAlreadyShown") ?? 0;
                var userSettingsSerialized = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
                UserSettingsDTO userSettings = userSettingsSerialized != null ? JsonSerializer.Deserialize<UserSettingsDTO>(userSettingsSerialized) : new UserSettingsDTO();

                var query = BuildInitialQuery();

                // Apply content filters based on user settings stored in session
                query = ApplyMatureContentFilter(query, userSettings.ShowMatureContent);
                query = ApplyAdultContentFilter(query, userSettings.ShowAdultContent);

                // Apply search and additional filters stored in session
                DeserializeAndRetrieveSessionData();
                query = ApplySearchFilters(query);
                query = ApplyAdditionalFilters(query);

                int totalCount = await query.CountAsync();

                ItemsAlreadyShown += pageSiz; // Increment the items already shown.
                _httpContextAccessor.HttpContext.Session.SetInt32("ItemsAlreadyShown", ItemsAlreadyShown);

                int totalPages = (int)Math.Ceiling((double)totalCount / pageSiz);

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
                };

                var mangaModels = await query.Skip(ItemsAlreadyShown).Take(pageSiz).ToListAsync();
                var json = JsonSerializer.Serialize(new { CurrentPage = currentPage, Books = mangaModels, TotalPages = totalPages }, options);

                return new JsonResult(new { CurrentPage = currentPage, Books = mangaModels, TotalPages = totalPages });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}

//Old  JS fetch for the more button Does not work properly
//public async Task<IActionResult> OnGetBooksPage(int currentPage, int pageSiz)
//{
//    try
//    {
//        var query = context.mangaModels.AsQueryable();
//        int totalCount = await query.CountAsync();
//        query = query.Skip((currentPage - 1) * pageSiz).Take(pageSiz);
//        var mangaModels = await query.ToListAsync();

//        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSiz);

//        return new JsonResult(new { CurrentPage = currentPage, Books = mangaModels, TotalPages = totalPages });
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Exception: {ex.Message}");
//        return StatusCode(500, new { message = "Internal server error" });
//    }
//}