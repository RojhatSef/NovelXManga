using MangaAccessService;
using MangaAccessService.DTO;

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

        [BindProperty]
        public Character CurrentCharacter { get; set; } // Adjusted to CharacterDTO

        public CharacterDTO CurrentCharacterDTO { get; set; } // Adjusted to CharacterDTO

        // Ensure you declare these properties to hold your DTO objects
        [BindProperty]
        public List<MangaModelDTO> CurrentMangas { get; set; } = new List<MangaModelDTO>();

        [BindProperty]
        public List<CharacterDTO> FamilyMembers { get; set; } = new List<CharacterDTO>();

        public CurrentCharacterModel(MangaNNovelAuthDBContext context, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment, UserManager<UserModel> userManager, IReviewRepsitory reviewRepsitory, ICharacterRepsitory characterRepsitory)
        {
            this.Context = context;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.reviewRepsitory = reviewRepsitory;
            this.characterRepsitory = characterRepsitory;
        }

        //[BindProperty]
        //public List<MangaModel> CurrentMangas { get; set; } = new List<MangaModel>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            // Project the main character data along with specific fields for Family and MangaModels
            var characterProjection = await Context.Characters
                .Where(c => c.CharacterId == id)
                .Select(c => new
                {
                    Character = c,
                    MangaModels = c.MangaModels.Take(5).Select(m => new MangaModelDTO
                    {
                        MangaID = m.MangaID,
                        MangaName = m.MangaName,
                        PhotoPath = m.PhotoPath
                    }).ToList(),
                    Family = c.Family.Take(5).Select(f => new CharacterDTO
                    {
                        CharacterId = f.CharacterId,
                        CharacterName = f.CharacterName,
                        PhotoPath = f.PhotoPath
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (characterProjection == null)
            {
                return NotFound();
            }

            // Map the projected data to the appropriate properties
            CurrentCharacter = characterProjection.Character;
            CurrentMangas = characterProjection.MangaModels;
            FamilyMembers = characterProjection.Family;

            // If you still need the CurrentCharacterDTO for any reason, you can fill it like this:
            CurrentCharacterDTO = new CharacterDTO
            {
                CharacterId = CurrentCharacter.CharacterId,
                CharacterName = CurrentCharacter.CharacterName,
                PhotoPath = CurrentCharacter.PhotoPath
            };

            return Page();
        }
    }
}