using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
        public List<MangaModel> CurrentMangas { get; set; } = new List<MangaModel>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            CurrentCharacter = await Context.Characters
           .Include(c => c.MangaModels)
           .FirstOrDefaultAsync(c => c.CharacterId == id);

            if (CurrentCharacter == null)
            {
                return NotFound();
            }

            // If you have multiple mangas associated with the character,
            // you loop through each and fetch all related data
            foreach (var manga in CurrentCharacter.MangaModels)
            {
                var detailedManga = await mangaRepository.GetOneMangaAllIncludedAsync(manga.MangaID);
                if (detailedManga != null)
                {
                    CurrentMangas.Add(detailedManga);
                }
            }

            return Page();
        }
    }
}