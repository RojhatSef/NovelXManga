using MangaModelService;

namespace MangaAccessService
{
    public interface ICharacterRepsitory
    {
        IEnumerable<Character> Search(string searchTerm);

        IEnumerable<Character> GetAllModels();

        Character GetModel(int id);

        IEnumerable<Character> GetCharactersByIds(List<int> characterIds);

        Character Update(Character UpdateModel);

        Character Add(Character addNewModel);

        Character Delete(int id);
    }
}