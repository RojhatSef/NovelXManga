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
    public class UpdateGenreModel : PageModel
    {
        private readonly IMangaRepository mangaRepository;
        private readonly MangaNNovelAuthDBContext context;
        private readonly ITagRepsitory tagRepsitory;

        public UpdateGenreModel(IMangaRepository mangaRepository, MangaNNovelAuthDBContext context, ITagRepsitory tagRepsitory)
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

        public List<GenresModel> Genres { get; set; }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var mangaToUpdate = await context.mangaModels

                                             .Include(m => m.GenresModels)
                                             .FirstOrDefaultAsync(m => m.MangaID == id);
            if (mangaToUpdate == null)
            {
                return RedirectToPage("/Index");
            }
            Genres = await context.GenresModels.ToListAsync();

            // Get the selected tag ids
            var SelectedGenre = SelectedTags ?? new List<int>();

            // Get the existing tag ids
            var exsitingGenre = mangaToUpdate.GenresModels.Select(t => t.GenresId).ToList();

            // Remove tags that are not selected anymore
            var genreToRemove = exsitingGenre.Except(SelectedGenre);
            foreach (var genID in genreToRemove)
            {
                var tagToRemove = mangaModelUpdate.GenresModels.FirstOrDefault(t => t.GenresId == genID);
                if (tagToRemove != null)
                {
                    mangaModelUpdate.GenresModels.Remove(tagToRemove);
                }
            }

            // Add new tags that are selected now
            var tagsToAdd = SelectedGenre.Except(exsitingGenre);
            foreach (var tagId in tagsToAdd)
            {
                var tagToAdd = await context.GenresModels.FindAsync(tagId);
                if (tagToAdd != null)
                {
                    mangaModelUpdate.GenresModels.Add(tagToAdd);
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
            mangaModelUpdate = context.mangaModels.Include(e => e.GenresModels).FirstOrDefault(e => e.MangaID == id);

            if (mangaModelUpdate == null)
            {
                return NotFound();
            }

            if (mangaModelUpdate.GenresModels == null)
            {
                mangaModelUpdate.GenresModels = new List<GenresModel>();
            }

            SelectedTags = mangaModelUpdate.GenresModels.Select(t => t.GenresId).ToList();

            Genres = await context.GenresModels.ToListAsync() ?? new List<GenresModel>();
            return Page();
        }
    }
}