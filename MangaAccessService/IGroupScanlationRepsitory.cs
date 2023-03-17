using MangaModelService;

namespace MangaAccessService
{
    public interface IGroupScanlationRepsitory
    {
        IEnumerable<GroupScanlatingModel> Search(string searchTerm);

        IEnumerable<GroupScanlatingModel> GetAllModels();

        GroupScanlatingModel GetModel(int id);

        GroupScanlatingModel Update(GroupScanlatingModel UpdateModel);

        GroupScanlatingModel Add(GroupScanlatingModel addNewModel);

        GroupScanlatingModel Delete(int id);

        Task<IEnumerable<GroupScanlatingModel>> SearchAsync(string searchTermAsync);

        Task<IEnumerable<GroupScanlatingModel>> GetAllModelAsync();

        Task<GroupScanlatingModel> GetModelAsync(int idAsync);

        Task<GroupScanlatingModel> UpdateAsync(GroupScanlatingModel UpdateModelAsync);

        Task<GroupScanlatingModel> AddAsync(GroupScanlatingModel AddAsync);

        Task<GroupScanlatingModel> DeleteAsync(int idAsync);
    }
}