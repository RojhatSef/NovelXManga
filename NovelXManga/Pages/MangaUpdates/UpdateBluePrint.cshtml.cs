using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NovelXManga.Pages.MangaUpdates
{
    [Authorize(Roles = "Owner, Admin, Updater")]
    public class UpdateBluePrintModel : PageModel
    {
        private readonly IMangaRepository mangaRepository;
        private readonly MangaNNovelAuthDBContext context;

        public UpdateBluePrintModel(IMangaRepository mangaRepository, MangaNNovelAuthDBContext context)
        {
            this.mangaRepository = mangaRepository;
            this.context = context;
        }

        [BindProperty]
        public MangaModel mangaModelUpdate { get; set; }

        public async Task<IActionResult> OnPostAsync(MangaModel mangaModel)
        {
            if (mangaModel == null)
            {
                return RedirectToPage("/Index");
            }
            mangaModelUpdate = mangaRepository.GetOneMangaAllIncluded(mangaModel.MangaID);

            await context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            mangaModelUpdate = context.mangaModels.Include(e => e.TagsModels).FirstOrDefault(e => e.MangaID == id);
            if (mangaModelUpdate == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}