using MangaModelService;

namespace MangaAccessService
{
    public interface IStudioRepsitory
    {
        IEnumerable<StudioModel> Search(string searchTerm);

        IEnumerable<StudioModel> GetAllModels();

        StudioModel GetModel(int id);

        StudioModel Update(StudioModel UpdateModel);

        StudioModel Add(StudioModel addNewModel);

        StudioModel Delete(int id);

        Task<IEnumerable<StudioModel>> SearchAsync(string searchTermAsync);

        Task<IEnumerable<StudioModel>> GetAllModelAsync();

        Task<StudioModel> GetModelAsync(int idAsync);

        Task<StudioModel> UpdateAsync(StudioModel UpdateModelAsync);

        Task<StudioModel> AddAsync(StudioModel AddAsync);

        Task<StudioModel> DeleteAsync(int idAsync);
    }
}