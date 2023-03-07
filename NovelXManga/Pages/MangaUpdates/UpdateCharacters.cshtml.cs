using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.MangaUpdates
{
    public class UpdateCharactersModel : PageModel
    {
        private readonly IMangaRepository mangaRepository;
        private readonly MangaNNovelAuthDBContext context;

        public UpdateCharactersModel(IMangaRepository mangaRepository, MangaNNovelAuthDBContext context)
        {
            this.mangaRepository = mangaRepository;
            this.context = context;
        }

        [BindProperty]
        public MangaModel mangaModelUpdate { get; set; }

        public async Task<IActionResult> OnPostAsync(MangaModel mangaModel)
        {
            if (mangaModel == null)
            {
                return RedirectToPage("/Index");
            }
            mangaModelUpdate = mangaRepository.GetOneMangaAllIncluded(mangaModel.MangaID);

            await context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetAsync(int id, List<Character> updatedCharacters)
        {
            if (id == null)
            {
                return NotFound();
            }

            mangaModelUpdate = mangaRepository.GetOneMangaAllIncluded(id);
            if (mangaModelUpdate == null)
            {
                return NotFound();
            }

            // iterate through the updated characters list and update each character in the manga model
            foreach (Character character in updatedCharacters)
            {
                Character existingCharacter = mangaModelUpdate.Characters.FirstOrDefault(c => c.CharacterId == character.CharacterId);
                if (existingCharacter != null)
                {
                    existingCharacter.CharacterName = character.CharacterName;
                    existingCharacter.Background = character.Background;
                    // update other character properties as necessary
                }
            }

            mangaRepository.Update(mangaModelUpdate);
            return RedirectToPage("/Manga/MangaDetails", new { id = mangaModelUpdate.MangaID });
        }
    }
}