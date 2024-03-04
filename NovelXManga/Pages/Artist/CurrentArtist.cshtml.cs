using MangaAccessService;
using MangaAccessService.DTO;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NovelXManga.Pages.Artist
{
    public class CurrentArtistModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext Context;
        private readonly IMangaRepository mangaRepository;
        private readonly ICharacterRepsitory characterRepsitory;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<UserModel> userManager;
        private readonly IReviewRepsitory reviewRepsitory;

        public CurrentArtistModel(MangaNNovelAuthDBContext context, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment, UserManager<UserModel> userManager, IReviewRepsitory reviewRepsitory, ICharacterRepsitory characterRepsitory)
        {
            this.Context = context;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.reviewRepsitory = reviewRepsitory;
            this.characterRepsitory = characterRepsitory;
        }

        [BindProperty]
        public ArtistModel CurrentArtist { get; set; }

        [BindProperty]
        public CreatorGenresAndTagsDto ArtistGenresAndTags { get; set; }

        public async Task<CreatorGenresAndTagsDto> GetArtistGenresAndTagsAsync(int artistId)
        {
            var artistWithGenresAndTags = await Context.artistModels
                .Where(a => a.ArtistId == artistId)
                .Select(a => new CreatorGenresAndTagsDto
                {
                    CreatorId = a.ArtistId,

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

            CurrentArtist = await Context.artistModels
               .Include(c => c.MangaModels)
               .FirstOrDefaultAsync(c => c.ArtistId == id);

            if (CurrentArtist == null)
            {
                return NotFound();
            }
            ArtistGenresAndTags = await GetArtistGenresAndTagsAsync(id);
            var mangaIds = CurrentArtist.MangaModels.Select(m => m.MangaID).ToList();
            //CurrentMangas = await characterRepsitory.GetMangaDtoIncludedAsync(mangaIds);

            return Page();
        }
    }
}