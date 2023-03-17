using MangaModelService;

namespace MangaAccessService
{
    public interface IOfficalWebsiteRepsitory
    {
        IEnumerable<OfficalWebsite> Search(string searchTerm);

        IEnumerable<OfficalWebsite> GetAllModels();

        OfficalWebsite GetModel(int id);

        OfficalWebsite Update(OfficalWebsite UpdateModel);

        OfficalWebsite Add(OfficalWebsite addNewModel);

        OfficalWebsite Delete(int id);

        Task<IEnumerable<OfficalWebsite>> SearchAsync(string searchTermAsync);

        Task<IEnumerable<OfficalWebsite>> GetAllModelAsync();

        Task<OfficalWebsite> GetModelAsync(int idAsync);

        Task<OfficalWebsite> UpdateAsync(OfficalWebsite UpdateModelAsync);

        Task<OfficalWebsite> AddAsync(OfficalWebsite AddAsync);

        Task<OfficalWebsite> DeleteAsync(int idAsync);
    }
}