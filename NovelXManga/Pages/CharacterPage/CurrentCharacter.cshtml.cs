using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.CharacterPage
{
    public class CurrentCharacterModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext Context;
        private readonly IMangaRepository mangaRepository;
        private readonly ICharacterRepsitory characterRepsitory;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<UserModel> userManager;
        private readonly IReviewRepsitory reviewRepsitory;


        public CurrentCharacterModel(MangaNNovelAuthDBContext context, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment, UserManager<UserModel> userManager, IReviewRepsitory reviewRepsitory, ICharacterRepsitory characterRepsitory)
        {
            this.Context = context;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.reviewRepsitory = reviewRepsitory;
            this.characterRepsitory = characterRepsitory;
        }

        [BindProperty]
        public Character CurrentCharacter { get; set; }

        [BindProperty]
        public MangaModel CurrentManga { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                NotFound();
            }
            CurrentCharacter = await characterRepsitory.GetModelAsync(id);
            CurrentManga = CurrentCharacter.MangaModels.FirstOrDefault(e => e.MangaID == CurrentManga.MangaID);
            return Page();
        }
    }
}