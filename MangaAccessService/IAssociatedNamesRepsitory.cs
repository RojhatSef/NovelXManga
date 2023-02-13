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
    }
}