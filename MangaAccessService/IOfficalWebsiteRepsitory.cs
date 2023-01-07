using MangaModelService;

namespace MangaAccessService
{
    public interface IOfficalWebsiteRepsitory
    {
        IEnumerable<OfficalWebsite> Search(string searchTerm);
        IEnumerable<OfficalWebsite> GetAllModels();
        OfficalWebsite GetModel(string id);

        OfficalWebsite Update(OfficalWebsite UpdateModel);

        OfficalWebsite Add(OfficalWebsite addNewModel);

        OfficalWebsite Delete(string id);
    }
}
