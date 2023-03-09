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
        public MangaModel MangaModelUpdate { get; set; }

        public async Task<IActionResult> OnPost(List<Character> updatedCharacters)
        {
            if (MangaModelUpdate == null)
            {
                return RedirectToPage("/Index");
            }

            // iterate through the MangaModel's Characters collection and remove any that are not in the updatedCharacters list
            foreach (Character character in MangaModelUpdate.Characters.ToList())
            {
                if (!updatedCharacters.Any(c => c.CharacterId == character.CharacterId))
                {
                    MangaModelUpdate.Characters.Remove(character);
                }
            }

            // iterate through the updated characters list and update each character in the MangaModel's Characters collection
            foreach (Character character in updatedCharacters)
            {
                Character existingCharacter = MangaModelUpdate.Characters.FirstOrDefault(c => c.CharacterId == character.CharacterId);
                if (existingCharacter != null)
                {
                    existingCharacter.CharacterName = character.CharacterName;
                    existingCharacter.Background = character.Background;
                    // update other character properties as necessary
                }
                else
                {
                    // add the new character to the MangaModel's Characters collection
                    MangaModelUpdate.Characters.Add(character);
                }
            }

            mangaRepository.Update(MangaModelUpdate);
            await context.SaveChangesAsync();

            return RedirectToPage("/Manga/MangaDetails", new { id = MangaModelUpdate.MangaID });
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MangaModelUpdate = mangaRepository.GetOneMangaAllIncluded(id);
            if (MangaModelUpdate == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}