using MangaModelService;

namespace MangaAccessService
{
    public interface IPostRepsitory
    {
        IEnumerable<PostModel> Search(string searchTerm);

        IEnumerable<PostModel> GetAllModels();

        PostModel GetModel(int id);

        PostModel Update(PostModel UpdateModel);

        PostModel Add(PostModel addNewModel);

        PostModel Delete(int id);
    }
}