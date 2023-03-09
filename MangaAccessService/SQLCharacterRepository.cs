using MangaModelService;

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
    }
}