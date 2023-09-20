using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace NovelXManga.Pages.MangaUpdates
{
    [Authorize(Roles = "Owner, Admin, Updater")]
    public class UpdateCharactersModel : PageModel
    {
        private readonly IMangaRepository mangaRepository;

        public UpdateCharactersModel(IMangaRepository mangaRepository)
        {
            this.mangaRepository = mangaRepository;
        }

        [BindProperty]
        public MangaModel MangaModelUpdate { get; set; }

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var manga = mangaRepository.GetOneMangaAllIncluded(MangaModelUpdate.MangaID);

            if (manga == null)
            {
                return NotFound();
            }

            // update existing characters
            foreach (var character in MangaModelUpdate.Characters)
            {
                var existingCharacter = manga.Characters.FirstOrDefault(c => c.CharacterId == character.CharacterId);

                if (existingCharacter != null)
                {
                    existingCharacter.CharacterName = character.CharacterName;
                    existingCharacter.Background = character.Background;
                    // update other character properties as necessary
                }
            }

            // add new characters
            foreach (var character in MangaModelUpdate.Characters.Where(c => c.CharacterId == 0))
            {
                manga.Characters.Add(character);
            }

            // remove deleted characters
            foreach (var character in manga.Characters.Where(c => !MangaModelUpdate.Characters.Any(m => m.CharacterId == c.CharacterId)))
            {
                manga.Characters.Remove(character);
            }

            await mangaRepository.UpdateAsync(manga);

            return RedirectToPage("/Manga/MangaDetails", new { id = manga.MangaID });
        }
    }
}