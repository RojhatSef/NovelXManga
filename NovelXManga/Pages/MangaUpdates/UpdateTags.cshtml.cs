using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace NovelXManga.Pages.MangaUpdates
{
    [Authorize(Roles = "Owner, Admin, Updater")]
    public class UpdateTagsModel : PageModel
    {
        private readonly IMangaRepository mangaRepository;
        private readonly MangaNNovelAuthDBContext context;
        private readonly ITagRepsitory tagRepsitory;

        public UpdateTagsModel(IMangaRepository mangaRepository, MangaNNovelAuthDBContext context, ITagRepsitory tagRepsitory)
        {
            this.mangaRepository = mangaRepository;
            this.context = context;
            this.tagRepsitory = tagRepsitory;
        }

        [BindProperty]
        [FromForm(Name = "selectedTags")]
        public List<int> SelectedTags { get; set; }

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
            Tags = await context.TagModels.ToListAsync();

            // Get the selected tag ids
            var selectedTagIds = SelectedTags ?? new List<int>();

            // Get the existing tag ids
            var existingTagIds = mangaModelUpdate.TagsModels.Select(t => t.TagId).ToList();

            // Remove tags that are not selected anymore
            var tagsToRemove = existingTagIds.Except(selectedTagIds);
            foreach (var tagId in tagsToRemove)
            {
                var tagToRemove = mangaModelUpdate.TagsModels.FirstOrDefault(t => t.TagId == tagId);
                if (tagToRemove != null)
                {
                    mangaModelUpdate.TagsModels.Remove(tagToRemove);
                }
            }

            // Add new tags that are selected now
            var tagsToAdd = selectedTagIds.Except(existingTagIds);
            foreach (var tagId in tagsToAdd)
            {
                var tagToAdd = await context.TagModels.FindAsync(tagId);
                if (tagToAdd != null)
                {
                    mangaModelUpdate.TagsModels.Add(tagToAdd);
                }
            }

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

            if (mangaModelUpdate.TagsModels == null)
            {
                mangaModelUpdate.TagsModels = new List<TagModel>();
            }

            SelectedTags = mangaModelUpdate.TagsModels.Select(t => t.TagId).ToList();

            Tags = await context.TagModels.ToListAsync() ?? new List<TagModel>();
            return Page();
        }
    }
}