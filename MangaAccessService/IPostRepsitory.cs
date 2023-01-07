using MangaModelService;

namespace MangaAccessService
{
    public interface IPostRepsitory
    {
        IEnumerable<PostModel> Search(string searchTerm);
        IEnumerable<PostModel> GetAllModels();
        PostModel GetModel(string id);

        PostModel Update(PostModel UpdateModel);

        PostModel Add(PostModel addNewModel);

        PostModel Delete(string id);
    }
}
