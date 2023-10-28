using MangaAccessService.DTO;
using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLCharacterRepository : ICharacterRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLCharacterRepository(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public Character Add(Character addNewModel)
        {
            context.Characters.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public Character Delete(int id)
        {
            Character modelToDelete = context.Characters.Find(id);
            if (modelToDelete != null)
            {
                context.Characters.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<Character> GetCharactersByIds(List<int> characterIds)
        {
            return context.Characters.Where(c => characterIds.Contains(c.CharacterId));
        }

        public IEnumerable<Character> GetAllModels()
        {
            return context.Characters;
        }

        public Character GetModel(int id)
        {
            return context.Characters.Find(id);
        }

        public IEnumerable<Character> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Characters;
            }
            return context.Characters.Where(e => e.CharacterName.Contains(searchTerm) || e.FamousQuote.Contains(searchTerm) || e.specie.Contains(searchTerm));
        }

        public Character Update(Character UpdateModel)
        {
            var currentModel = context.Characters.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<Character> AddAsync(Character AddAsync)
        {
            await context.Characters.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<CurrentCharacterDto> GetMangaDtoIncludedAsync(int id)
        {
            return await context.Characters
                .Where(c => c.CharacterId == id)
                .Select(c => new CurrentCharacterDto
                {
                    CharacterId = c.CharacterId,
                    MangaList = c.MangaModels.Select(m => new CurrentCharacterMangaDto
                    {
                        MangaID = m.MangaID,
                        MangaName = m.MangaName,
                        PhotoPath = m.PhotoPath
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Character> DeleteAsync(int idAsync)
        {
            Character modelToDelete = await context.Characters.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.Characters.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<Character>> GetAllModelAsync()
        {
            return await context.Characters.ToListAsync();
        }

        public async Task<Character> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.Characters.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<Character> UpdateAsync(Character UpdateModelAsync)
        {
            var modelUpdate = context.Characters.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<IEnumerable<Character>> SearchAsync(string searchTermAsync)
        {
            if (string.IsNullOrEmpty(searchTermAsync))
            {
                return await context.Characters.ToListAsync();
            }
            return await context.Characters.Where(e => e.CharacterName.Contains(searchTermAsync) || e.FamousQuote.Contains(searchTermAsync) || e.specie.Contains(searchTermAsync)).ToListAsync();
        }
    }
}