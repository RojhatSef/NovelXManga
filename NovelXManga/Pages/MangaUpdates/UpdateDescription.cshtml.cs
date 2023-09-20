using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

            mangaModelUpdate = await mangaRepository.GetOneMangaAllIncludedAsync(mangaModel.MangaID);

            mangaModelUpdate.Description = mangaModel.Description;
            mangaModelUpdate.ISBN10 = mangaModel.ISBN10;
            mangaModelUpdate.ISBN13 = mangaModel.ISBN13;
            mangaModelUpdate.futureEvents = mangaModel.futureEvents;
            mangaModelUpdate.StatusInCountryOfOrigin = mangaModel.StatusInCountryOfOrigin;
            mangaModelUpdate.CompletelyTranslated = mangaModel.CompletelyTranslated;
            mangaModelUpdate.orignalWebtoon = mangaModel.orignalWebtoon;
            mangaModelUpdate.OriginalPublisher = mangaModel.OriginalPublisher;
            mangaModelUpdate.OfficalLanguage = mangaModel.OfficalLanguage;

            mangaModelUpdate = await mangaRepository.UpdateAsync(mangaModelUpdate);

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            mangaModelUpdate = context.mangaModels.FirstOrDefault(e => e.MangaID == id);

            if (mangaModelUpdate == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}