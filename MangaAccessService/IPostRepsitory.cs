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

        Task<IEnumerable<PostModel>> SearchAsync(string searchTermAsync);

        Task<IEnumerable<PostModel>> GetAllModelAsync();

        Task<PostModel> GetModelAsync(int idAsync);

        Task<PostModel> UpdateAsync(PostModel UpdateModelAsync);

        Task<PostModel> AddAsync(PostModel AddAsync);

        Task<PostModel> DeleteAsync(int idAsync);
    }
}