using MangaModelService;

namespace MangaAccessService
{
    public interface ICharacterRepsitory
    {
        IEnumerable<Character> Search(string searchTerm);
        IEnumerable<Character> GetAllModels();
        Character GetModel(string id);

        Character Update(Character UpdateModel);

        Character Add(Character addNewModel);

        Character Delete(string id);
    }
}
