using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NovelXManga.Pages.MangaUpdates
{
    [Authorize(Roles = "Owner, Admin, Updater")]
    public class UpdateDescriptionModel : PageModel
    {
        private readonly IMangaRepository mangaRepository;
        private readonly MangaNNovelAuthDBContext context;
        private readonly ITagRepsitory tagRepsitory;

        public UpdateDescriptionModel(IMangaRepository mangaRepository, MangaNNovelAuthDBContext context, ITagRepsitory tagRepsitory)
        {
            this.mangaRepository = mangaRepository;
            this.context = context;
            this.tagRepsitory = tagRepsitory;
        }

        [BindProperty]
        public MangaModel mangaModelUpdate { get; set; }

        public List<TagModel> Tags { get; set; }

        public async Task<IActionResult> OnPostAsync(MangaModel mangaModel)
        {
            if (mangaModel == null)
            {
                return RedirectToPage("/Index");
            }

            // Retrieve the existing entity from the database to avoid tracking conflicts.
            MangaModel existingEntity = await context.mangaModels
                .FirstOrDefaultAsync(e => e.MangaID == mangaModel.MangaID);

            // If the entity does not exist, return NotFound.
            if (existingEntity == null)
            {
                return NotFound();
            }
            if (mangaModel.EndingYear.HasValue && mangaModel.ReleaseYear.HasValue)
            {
                if (mangaModel.EndingYear < mangaModel.ReleaseYear)
                {
                    ModelState.AddModelError("mangaModel.EndingYear", "Ending year cannot be earlier than the release year.");
                }
            }

            // Update the existing entity with the values from the form
            existingEntity.BookUpdated = DateTime.UtcNow;
            existingEntity.EndingYear = mangaModel.EndingYear;
            existingEntity.ReleaseYear = mangaModel.ReleaseYear;
            existingEntity.Description = mangaModel.Description;
            existingEntity.ISBN10 = mangaModel.ISBN10;
            existingEntity.ISBN13 = mangaModel.ISBN13;
            existingEntity.futureEvents = mangaModel.futureEvents;
            existingEntity.StatusInCountryOfOrigin = mangaModel.StatusInCountryOfOrigin;
            existingEntity.CompletelyTranslated = mangaModel.CompletelyTranslated;
            existingEntity.orignalWebtoon = mangaModel.orignalWebtoon;
            existingEntity.OriginalPublisher = mangaModel.OriginalPublisher;
            existingEntity.OfficalLanguage = mangaModel.OfficalLanguage;
            existingEntity.Type = mangaModel.Type;

            // Since the entity is already tracked, simply call SaveChangesAsync.
            await context.SaveChangesAsync();

            return RedirectToPage("/Manga/CurrentManga", new { id = mangaModel.MangaID });
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            mangaModelUpdate = await context.mangaModels.FirstOrDefaultAsync(e => e.MangaID == id);

            if (mangaModelUpdate == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}