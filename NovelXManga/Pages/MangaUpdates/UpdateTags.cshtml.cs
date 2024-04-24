using MangaAccessService;
using MangaAccessService.DTO;
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

        public UpdateTagsModel(IMangaRepository mangaRepository, MangaNNovelAuthDBContext context)
        {
            this.mangaRepository = mangaRepository;
            this.context = context;
        }

        [BindProperty]
        [FromForm(Name = "JsSelectedTags")]
        public List<int> SelectedTags { get; set; }

        [BindProperty]
        [FromForm(Name = "PositiveSelectedGenres")]
        public List<int> PositiveSelectedGenres { get; set; }

        public List<TagModel> Tags { get; set; }
        public List<GenresModel> Genres { get; set; }

        [BindProperty]
        public MangaModel MangaModels { get; set; }

        public async Task<List<MangaDTO>> GetAllMangaMinimalAsync()
        {
            return await context.mangaModels
                .Select(m => new MangaDTO { MangaID = m.MangaID, MangaName = m.MangaName })
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var mangaToUpdate = await context.mangaModels
                                             .Include(m => m.TagsModels)
                                             .Include(m => m.GenresModels)
                                             .FirstOrDefaultAsync(m => m.MangaID == id);

            if (mangaToUpdate == null)
            {
                return NotFound("Manga not found.");
            }

            // Update Tags
            var currentTags = mangaToUpdate.TagsModels.ToList();
            var updatedTags = context.TagModels.Where(t => SelectedTags.Contains(t.TagId)).ToList();
            mangaToUpdate.TagsModels = updatedTags;

            // Update Genres
            var currentGenres = mangaToUpdate.GenresModels.ToList();
            var updatedGenres = context.GenresModels.Where(g => PositiveSelectedGenres.Contains(g.GenresId)).ToList();
            mangaToUpdate.GenresModels = updatedGenres;

            // Save changes
            context.Update(mangaToUpdate);
            await context.SaveChangesAsync();

            return RedirectToPage("/Manga/CurrentManga", new { id = id });
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MangaModels = await context.mangaModels
                                       .Include(m => m.TagsModels)
                                       .Include(m => m.GenresModels)
                                       .FirstOrDefaultAsync(m => m.MangaID == id.Value);

            if (MangaModels == null)
            {
                return NotFound("Specified manga not found.");
            }

            SelectedTags = MangaModels.TagsModels.Select(t => t.TagId).ToList();
            PositiveSelectedGenres = MangaModels.GenresModels.Select(g => g.GenresId).ToList();

            // Load all tags and genres for selection lists
            Tags = await context.TagModels.ToListAsync();
            Genres = await context.GenresModels.ToListAsync();

            return Page();
        }
    }
}