using MangaAccessService;
using MangaAccessService.DTO;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NovelXManga.Pages.Artist
{
    public class CurrentAuthorModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext Context;
        private readonly IMangaRepository mangaRepository;
        private readonly ICharacterRepsitory characterRepsitory;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<UserModel> userManager;
        private readonly IReviewRepsitory reviewRepsitory;

        public CurrentAuthorModel(MangaNNovelAuthDBContext context, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment, UserManager<UserModel> userManager, IReviewRepsitory reviewRepsitory, ICharacterRepsitory characterRepsitory)
        {
            this.Context = context;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.reviewRepsitory = reviewRepsitory;
            this.characterRepsitory = characterRepsitory;
        }

        [BindProperty]
        public AuthorModel CurrentAuthor { get; set; }

        [BindProperty]
        public CreatorGenresAndTagsDto AuthorGenresAndTags { get; set; }

        public async Task<CreatorGenresAndTagsDto> GetArtistGenresAndTagsAsync(int authorid)
        {
            var artistWithGenresAndTags = await Context.authorModels
                .Where(a => a.AuthorID == authorid)
                .Select(a => new CreatorGenresAndTagsDto
                {
                    CreatorId = a.AuthorID,

                    Genres = a.MangaModels.SelectMany(m => m.GenresModels.Select(g => g.GenreName)).Distinct(),
                    Tags = a.MangaModels.SelectMany(m => m.TagsModels.Select(t => t.TagName)).Distinct(),
                }).FirstOrDefaultAsync();

            return artistWithGenresAndTags;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            CurrentAuthor = await Context.authorModels
               .Include(c => c.MangaModels)
               .FirstOrDefaultAsync(c => c.AuthorID == id);

            if (CurrentAuthor == null)
            {
                return NotFound();
            }
            AuthorGenresAndTags = await GetArtistGenresAndTagsAsync(id);
            var mangaIds = CurrentAuthor.MangaModels.Select(m => m.MangaID).ToList();
            //CurrentMangas = await characterRepsitory.GetMangaDtoIncludedAsync(mangaIds);

            return Page();
        }
    }
}