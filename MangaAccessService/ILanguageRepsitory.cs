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

        Task<IEnumerable<Languages>> SearchAsync(string searchTermAsync);

        Task<IEnumerable<Languages>> GetAllModelAsync();

        Task<Languages> GetModelAsync(int idAsync);

        Task<Languages> UpdateAsync(Languages UpdateModelAsync);

        Task<Languages> AddAsync(Languages AddAsync);

        Task<Languages> DeleteAsync(int idAsync);
    }
}