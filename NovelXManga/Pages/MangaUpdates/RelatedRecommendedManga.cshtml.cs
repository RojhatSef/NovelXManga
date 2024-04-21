using MangaAccessService;
using MangaAccessService.DTO;
using MangaModelService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using System.Text.Json;

namespace NovelXManga.Pages.MangaUpdates
{
    [Authorize(Roles = "Owner, Admin, Updater")]
    public class RelatedRecommendedMangaModel : PageModel
    {
        private readonly IMangaRepository mangaRepository;
        private readonly MangaNNovelAuthDBContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [BindProperty]
        public MangaModel _MangaModel { get; set; }

        public IEnumerable<MangaDTO> RelatedMangaOptions { get; set; } = new List<MangaDTO>();
        public IEnumerable<MangaDTO> RecommendedMangaOptions { get; set; } = new List<MangaDTO>();

        [BindProperty]
        public int[] SelectedRelatedMangaIds { get; set; }

        [BindProperty]
        public int[] SelectedRecommendedMangaIds { get; set; }

        public IEnumerable<MangaModel> MangaModels { get; set; }

        public RelatedRecommendedMangaModel(IMangaRepository mangaRepository, MangaNNovelAuthDBContext context)
        {
            this.mangaRepository = mangaRepository;
            this.context = context;
        }

        public async Task<List<MangaDTO>> GetAllMangaMinimalAsync()
        {
            return await context.mangaModels
                .Select(m => new MangaDTO { MangaID = m.MangaID, MangaName = m.MangaName })
                .ToListAsync();
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            _MangaModel = await context.mangaModels
                                       .Include(m => m.relatedSeries)
                                       .Include(m => m.RecommendedMangaModels)
                                       .FirstOrDefaultAsync(m => m.MangaID == id);

            if (_MangaModel == null)
            {
                return NotFound("Specified manga not found.");
            }

            // Handle related manga session storage
            HandleSessionData("SelectedRelatedMangaIds", _MangaModel.relatedSeries.Select(m => m.MangaID).ToArray());

            // Handle recommended manga session storage
            HandleSessionData("SelectedRecommendedMangaIds", _MangaModel.RecommendedMangaModels.Select(m => m.MangaID).ToArray());

            // Load minimal manga data for dropdowns or lists
            RelatedMangaOptions = await GetAllMangaMinimalAsync();
            RecommendedMangaOptions = await GetAllMangaMinimalAsync();

            return Page();
        }

        private void HandleSessionData(string sessionKey, int[] currentIds)
        {
            var serializedIds = _httpContextAccessor.HttpContext.Session.GetString(sessionKey);
            int[] sessionIds = null;

            if (!string.IsNullOrEmpty(serializedIds))
            {
                sessionIds = JsonSerializer.Deserialize<int[]>(serializedIds);
            }

            if (sessionIds == null || !sessionIds.SequenceEqual(currentIds))
            {
                _httpContextAccessor.HttpContext.Session.SetString(sessionKey, JsonSerializer.Serialize(currentIds));
            }
        }
    }
}