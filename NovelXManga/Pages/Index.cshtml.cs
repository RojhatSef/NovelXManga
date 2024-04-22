using MangaAccessService;
using MangaAccessService.DTO;
using MangaAccessService.DTO.IndexDto;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NovelXManga.Pages.SearchFilter;
using System.Text.Json;
using System.Web;

namespace NovelXManga.Pages
{
    //LAYOUTPAGE needs it's own CSS, Index needs it's own CSS.
    public class IndexModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<IndexModel> _logger;
        private readonly CheckUserSettings _checkUserSettings;
        private readonly UserManager<UserModel> userManager;

        [BindProperty]
        [TempData]
        public string SucessFulManga { get; set; }

        [BindProperty]
        public int ReadingUsersCount { get; set; }

        [BindProperty]
        public UserSettings UserSettings { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment, MangaNNovelAuthDBContext context, CheckUserSettings checkUserSettings, UserManager<UserModel> userManager)
        {
            _logger = logger;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
            _checkUserSettings = checkUserSettings;
            this.userManager = userManager;
        }

        // Part of the search field

        public async Task<JsonResult> OnGetSearchMangaAsync(string searchTerm)
        {
            searchTerm = HttpUtility.HtmlEncode(searchTerm);
            int maxResults = 30;

            var mangaResults = await context.mangaModels
                .Where(m => m.MangaName.Contains(searchTerm) || m.ISBN10.Contains(searchTerm) || m.ISBN13.Contains(searchTerm))
                .Select(m => new SearchResult { Id = m.MangaID, Name = m.MangaName, PhotoPath = m.PhotoPath, Type = "Manga" })
                .Take(30).ToListAsync();

            var authorResults = await context.authorModels
                .Where(a => a.FirstName.Contains(searchTerm) || a.LastName.Contains(searchTerm))
                .Select(a => new SearchResult { Id = a.AuthorID, Name = $"{a.FirstName} {a.LastName}", PhotoPath = a.PhotoPath, Type = "Author" })
                .Take(30).ToListAsync();

            var artistResults = await context.artistModels
                .Where(a => a.FirstName.Contains(searchTerm) || a.LastName.Contains(searchTerm))
                .Select(a => new SearchResult { Id = a.ArtistId, Name = $"{a.FirstName} {a.LastName}", PhotoPath = a.PhotoPath, Type = "Artist" })
                .Take(30).ToListAsync();

            var voiceActorResults = await context.voiceActorModels
                .Where(v => v.FirstName.Contains(searchTerm) || v.LastName.Contains(searchTerm))
                .Select(v => new SearchResult { Id = v.VoiceActorId, Name = $"{v.FirstName} {v.LastName}", PhotoPath = v.PhotoPath, Type = "VoiceActor" })
                .Take(30).ToListAsync();

            List<SearchResult> finalResults = new List<SearchResult>();

            if (mangaResults.Count >= maxResults)
            {
                finalResults.AddRange(mangaResults.Take(maxResults));
            }
            else
            {
                finalResults.AddRange(mangaResults);
                int remaining = maxResults - mangaResults.Count;

                var authorsToAdd = Math.Min(remaining, authorResults.Count);
                finalResults.AddRange(authorResults.Take(authorsToAdd));
                remaining -= authorsToAdd;

                var artistsToAdd = Math.Min(remaining, artistResults.Count);
                finalResults.AddRange(artistResults.Take(artistsToAdd));
                remaining -= artistsToAdd;

                var voiceActorsToAdd = Math.Min(remaining, voiceActorResults.Count);
                finalResults.AddRange(voiceActorResults.Take(voiceActorsToAdd));
            }

            var groupedResults = finalResults.GroupBy(r => r.Type)
                                             .ToDictionary(g => g.Key, g => g.ToList());

            return new JsonResult(groupedResults, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        public List<IndexDtoManga> AllBooksList { get; set; }
        public IEnumerable<AssociatedNames> associatedNames { get; set; }

        //public IEnumerable<MangaModel> GetAllBooks { get; set; }
        public IEnumerable<WallPapers> WallPapers { get; set; }

        public IEnumerable<IndexDtoManga> GetAllBooks { get; set; }
        public List<WallPapers> WallpaperList { get; set; }

        public async Task OnGetAsync()
        {
            UserSettings = await _checkUserSettings.GetUserSettingsAsync(User);
            UserModel user = await userManager.GetUserAsync(User);
            // If UserSettings is null (i.e., the user is not logged in or no settings are saved), set defaults
            if (UserSettings == null)
            {
                // Set default UserSettings values for a guest or when settings are not found
                UserSettings = new UserSettings
                {
                    DarkModeEnabled = true, // Default to dark mode
                                            // Set other default values as needed
                };
            }
            // Pass the dark mode setting to the frontend via ViewData or a model property
            ViewData["IsDarkModeEnabled"] = UserSettings.DarkModeEnabled;
            // Continue with your existing code
            GetAllBooks = await mangaRepository.IndexMangaDtoIncludedAsync(user);
            AllBooksList = GetAllBooks.ToList();
            WallPapers = await context.WallPapers.ToListAsync();
            WallpaperList = WallPapers.ToList();
        }

        private async Task<List<UserListDto>> GetUsersInListAsync(int mangaId, DbSet<MangaModel> mangaDbSet)
        {
            return await mangaDbSet
          .AsNoTracking()
          .Where(m => m.MangaID == mangaId)
          .SelectMany(m => m.userModels) // Use the correct navigation property name
          .Distinct() // Ensure distinct users are selected
          .Select(u => new UserListDto { UserId = u.Id })
          .ToListAsync();
        }

        private async Task<List<UserListDto>> GetFavoriteUsersInListAsync(int mangaId)
        {
            return await context.favoritBookLists
                .AsNoTracking()
                .Where(fl => fl.MangaModelId == mangaId)
                .SelectMany(fl => fl.UserModels)
                .Distinct()
                .Select(u => new UserListDto { UserId = u.Id })
                .ToListAsync();
        }

        private async Task<List<UserListDto>> GetCompletedUsersInListAsync(int mangaId)
        {
            return await context.completedBookLists
                .AsNoTracking()
                .Where(fl => fl.MangaModelId == mangaId)
                .SelectMany(fl => fl.UserModels)
                .Distinct()
                .Select(u => new UserListDto { UserId = u.Id })
                .ToListAsync();
        }

        private async Task<List<UserListDto>> GetWishUsersInListAsync(int mangaId)
        {
            return await context.wishBookLists
                .AsNoTracking()
                .Where(fl => fl.MangaModelId == mangaId)
                .SelectMany(fl => fl.UserModels)
                .Distinct()
                .Select(u => new UserListDto { UserId = u.Id })
                .ToListAsync();
        }

        private async Task RecalculateReadingUsersCount(int mangaId)
        {
            // Fetch user lists associated with the manga
            var readingListUserIds = await GetUsersInListAsync(mangaId, context.mangaModels); // Adjusted to use the method
            var favoriteListUserIds = await GetFavoriteUsersInListAsync(mangaId); // Specific method for favorite lists
            var completedListUserIds = await GetCompletedUsersInListAsync(mangaId); // Specific method for completed lists
            var wishListUserIds = await GetWishUsersInListAsync(mangaId);

            // Calculate unique user count
            ReadingUsersCount = readingListUserIds
                .Union(favoriteListUserIds)
                .Union(completedListUserIds)
                .Union(wishListUserIds)
                .GroupBy(u => u.UserId)
                .Select(group => group.First())
                .Count();
        }
    }
}