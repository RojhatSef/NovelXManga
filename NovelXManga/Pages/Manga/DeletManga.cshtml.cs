using MangaAccessService;
using MangaAccessService.DTO;
using MangaModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NovelXManga.Pages.Manga
{
    [Authorize(Roles = "Owner, Admin, AdminControl")]
    public class DeletMangaModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;

        public DeletMangaModel(MangaNNovelAuthDBContext context, IMangaRepository mangaRepository)
        {
            this.context = context;
            this.mangaRepository = mangaRepository;
        }

        [BindProperty]
        public MangaModel CurrentManga { get; set; }

        [BindProperty]
        public CurrentMangaDto CurrentManga2 { get; set; }

        [BindProperty]
        public CurrentMangaRelatedRecommendedSeriesDTO relatedRecommendedManga { get; set; }

        public async Task<CurrentMangaRelatedRecommendedSeriesDTO> LoadRelatedAndRecommendedSeries(int mangaId)
        {
            // Fetch the MangaModel from the database along with related and recommended series
            var manga = await context.mangaModels
                .Where(m => m.MangaID == mangaId)
                .Include(m => m.relatedSeries)
                .Include(m => m.RecommendedMangaModels)
                .FirstOrDefaultAsync();

            if (manga == null)
            {
                // Handle the case where the manga is not found
                return null;
            }

            // Map the related series to DTOs
            var relatedSeriesDtos = manga.relatedSeries.Select(rs => new CurrentMangaRelatedSeriesDTO
            {
                MangaID = rs.MangaID,
                MangaName = rs.MangaName,
                PhotoPath = rs.PhotoPath
            }).ToList();

            // Map the recommended manga models to DTOs
            var recommendedMangaDtos = manga.RecommendedMangaModels.Select(rm => new MangaModelDTO
            {
                MangaID = rm.MangaID,
                MangaName = rm.MangaName,
                PhotoPath = rm.PhotoPath
            }).ToList();

            // Package and return the response
            return new CurrentMangaRelatedRecommendedSeriesDTO
            {
                RelatedSeries = relatedSeriesDtos,
                RecommendedMangaModels = recommendedMangaDtos
            };
        }

        public async Task<CurrentMangaRelatedRecommendedSeriesDTO> LoadRelatedRecommended(int id)
        {
            var relatedmanga = await context.mangaModels
  .Where(e => e.MangaID == id)
  .Select(e => new CurrentMangaRelatedRecommendedSeriesDTO
  {
      RelatedSeries = e.relatedSeries.Select(rs => new CurrentMangaRelatedSeriesDTO
      {
          MangaID = rs.MangaID,
          MangaName = rs.MangaName,
          PhotoPath = rs.PhotoPath
      }).Take(5).ToList(),
      RecommendedMangaModels = e.RecommendedMangaModels.Select(rm => new MangaModelDTO
      {
          MangaID = rm.MangaID,
          MangaName = rm.MangaName,
          PhotoPath = rm.PhotoPath
      }).Take(5).ToList()
  }).FirstOrDefaultAsync();
            return relatedmanga;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            relatedRecommendedManga = await LoadRelatedRecommended(id);
            if (relatedRecommendedManga == null ||
        relatedRecommendedManga.RecommendedMangaModels == null || relatedRecommendedManga.RecommendedMangaModels.Count == 0 ||
        relatedRecommendedManga.RelatedSeries == null || relatedRecommendedManga.RelatedSeries.Count == 0)

            {
                relatedRecommendedManga = await LoadRelatedAndRecommendedSeries(id);
            }

            CurrentManga = await mangaRepository.GetOneEssentialMangaIncludedAsync(id);
            CurrentManga2 = await mangaRepository.GetOneEssentialMangaDtoIncludedAsync(id);
            return Page();
        }
    }
}