using MangaAccessService;
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

            var mangaIds = CurrentArtist.MangaModels.Select(m => m.MangaID).ToList();
            //CurrentMangas = await characterRepsitory.GetMangaDtoIncludedAsync(mangaIds);

            return Page();
        }
    }
}