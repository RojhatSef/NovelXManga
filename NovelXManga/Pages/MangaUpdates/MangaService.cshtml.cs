using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.MangaUpdates
{
    public class MangaServiceModel : PageModel
    {
        private readonly IMangaRepository _mangaRepository;
        private readonly MangaRankingService _mangaRankingService;

        public MangaServiceModel(IMangaRepository mangaRepository, MangaRankingService mangaRankingService)
        {
            _mangaRepository = mangaRepository;
            _mangaRankingService = mangaRankingService;
        }

        public IEnumerable<MangaModel> GetAllBooks { get; set; }
        public IEnumerable<MangaModel> AllBooksList { get; set; }

        public async Task OnGetAsync()
        {
            GetAllBooks = await _mangaRepository.GetAllModelAsync();
            AllBooksList = GetAllBooks;
        }

        public async Task<IActionResult> OnPostUpdateRankingsAsync()
        {
            try
            {
                await _mangaRankingService.UpdateRankingsAsync();
                return new JsonResult("Rankings updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}