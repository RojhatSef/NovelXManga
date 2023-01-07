using MangaModelService;


namespace MangaAccessService
{
    public interface IAssociatedNamesRepsitory
    {
        IEnumerable<AssociatedNames> Search(string searchTerm);
        IEnumerable<AssociatedNames> GetAllModels();
        AssociatedNames GetModel(string id);

        AssociatedNames Update(AssociatedNames UpdateModel);

        AssociatedNames Add(AssociatedNames addNewModel);

        AssociatedNames Delete(string id);
    }
}
