using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NovelXManga.Pages.Artist
{
    public class CurrentVoiceModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext Context;
        private readonly IMangaRepository mangaRepository;
        private readonly ICharacterRepsitory characterRepsitory;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<UserModel> userManager;
        private readonly IReviewRepsitory reviewRepsitory;

        public CurrentVoiceModel(MangaNNovelAuthDBContext context, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment, UserManager<UserModel> userManager, IReviewRepsitory reviewRepsitory, ICharacterRepsitory characterRepsitory)
        {
            this.Context = context;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.reviewRepsitory = reviewRepsitory;
            this.characterRepsitory = characterRepsitory;
        }

        [BindProperty]
        public VoiceActorModel CurrentVoiceActor { get; set; }

        //[BindProperty]
        //public List<MangaModel> CurrentMangas { get; set; } = new List<MangaModel>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            CurrentVoiceActor = await Context.voiceActorModels
               .Include(c => c.MangaModels)
               .FirstOrDefaultAsync(c => c.VoiceActorId == id);

            if (CurrentVoiceActor == null)
            {
                return NotFound();
            }

            var mangaIds = CurrentVoiceActor.MangaModels.Select(m => m.MangaID).ToList();
            //CurrentMangas = await characterRepsitory.GetMangaDtoIncludedAsync(mangaIds);

            return Page();
        }
    }
}