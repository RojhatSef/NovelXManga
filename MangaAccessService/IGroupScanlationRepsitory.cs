using MangaModelService;

namespace MangaAccessService
{
    public interface IGroupScanlationRepsitory
    {
        IEnumerable<GroupScanlatingModel> Search(string searchTerm);
        IEnumerable<GroupScanlatingModel> GetAllModels();
        GroupScanlatingModel GetModel(string id);

        GroupScanlatingModel Update(GroupScanlatingModel UpdateModel);

        GroupScanlatingModel Add(GroupScanlatingModel addNewModel);

        GroupScanlatingModel Delete(string id);
    }
}
