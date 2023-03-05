using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Updates
{
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
            mangaModelUpdate = mangaRepository.GetOneMangaAllIncluded(mangaModel.MangaID);

            mangaModelUpdate.Description = mangaModel.Description;
            mangaModelUpdate = mangaRepository.Update(mangaModelUpdate);

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