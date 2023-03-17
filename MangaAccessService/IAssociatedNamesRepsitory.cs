using MangaModelService;

namespace MangaAccessService
{
    public interface IAssociatedNamesRepsitory
    {
        IEnumerable<AssociatedNames> Search(string searchTerm);

        IEnumerable<AssociatedNames> GetAllModels();

        AssociatedNames GetModel(int id);

        AssociatedNames Update(AssociatedNames UpdateModel);

        AssociatedNames Add(AssociatedNames addNewModel);

        AssociatedNames Delete(int id);

        Task<IEnumerable<AssociatedNames>> SearchAsync(string searchTermAsync);

        Task<IEnumerable<AssociatedNames>> GetAllModelAsync();

        Task<AssociatedNames> GetModelAsync(int idAsync);

        Task<AssociatedNames> UpdateAsync(AssociatedNames UpdateModelAsync);

        Task<AssociatedNames> AddAsync(AssociatedNames AddAsync);

        Task<AssociatedNames> DeleteAsync(int idAsync);
    }
}