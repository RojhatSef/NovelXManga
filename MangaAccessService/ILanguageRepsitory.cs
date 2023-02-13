using MangaModelService;

namespace MangaAccessService
{
    public interface ILanguageRepsitory
    {
        IEnumerable<Languages> Search(string searchTerm);

        IEnumerable<Languages> GetAllModels();

        Languages GetModel(int id);

        Languages Update(Languages UpdateModel);

        Languages Add(Languages addNewModel);

        Languages Delete(int id);
    }
}