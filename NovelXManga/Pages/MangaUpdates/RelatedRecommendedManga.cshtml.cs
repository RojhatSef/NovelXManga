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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var mangaToUpdate = await context.mangaModels
                                             .Include(m => m.relatedSeries)
                                             .Include(m => m.RecommendedMangaModels)
                                             .FirstOrDefaultAsync(m => m.MangaID == id);

            if (mangaToUpdate == null)
            {
                return NotFound("Manga not found.");
            }

            // Update or create new list for related manga
            if (SelectedRelatedMangaIds != null && SelectedRelatedMangaIds.Any())
            {
                var relatedManga = await context.mangaModels
                                                .Where(m => SelectedRelatedMangaIds.Contains(m.MangaID))
                                                .ToListAsync();
                mangaToUpdate.relatedSeries = relatedManga;
            }
            else
            {
                mangaToUpdate.relatedSeries.Clear();  // Clear existing entries if no manga is selected
                mangaToUpdate.relatedSeries = new List<MangaModel>();  // Re-initialize if needed
            }

            // Update or create new list for recommended manga
            if (SelectedRecommendedMangaIds != null && SelectedRecommendedMangaIds.Any())
            {
                var recommendedManga = await context.mangaModels
                                                    .Where(m => SelectedRecommendedMangaIds.Contains(m.MangaID))
                                                    .ToListAsync();
                mangaToUpdate.RecommendedMangaModels = recommendedManga;
            }
            else
            {
                mangaToUpdate.RecommendedMangaModels.Clear();  // Clear existing entries if no manga is selected
                mangaToUpdate.RecommendedMangaModels = new List<MangaModel>();  // Re-initialize if needed
            }

            context.Update(mangaToUpdate);
            await context.SaveChangesAsync();

            // Redirect to the page before, which seems to be the current manga details page
            return RedirectToPage("/Manga/CurrentManga", new { id = id });
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

            // Load minimal manga data for dropdowns or lists
            RelatedMangaOptions = await GetAllMangaMinimalAsync();
            RecommendedMangaOptions = await GetAllMangaMinimalAsync();
            SelectedRelatedMangaIds = _MangaModel.relatedSeries.Select(rs => rs.MangaID).ToArray();
            SelectedRecommendedMangaIds = _MangaModel.RecommendedMangaModels.Select(rm => rm.MangaID).ToArray();

            return Page();
        }
    }
}