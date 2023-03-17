using MangaModelService;

namespace MangaAccessService
{
    public interface IBlogRepsitory
    {
        IEnumerable<BlogModel> GetAllModels();

        BlogModel GetModel(int id);

        BlogModel Update(BlogModel UpdateModel);

        BlogModel Add(BlogModel addNewModel);

        BlogModel Delete(int id);

        Task<IEnumerable<BlogModel>> SearchAsync(string searchTermAsync);

        Task<IEnumerable<BlogModel>> GetAllModelAsync();

        Task<BlogModel> GetModelAsync(int idAsync);

        Task<BlogModel> UpdateAsync(BlogModel UpdateModelAsync);

        Task<BlogModel> AddAsync(BlogModel AddAsync);

        Task<BlogModel> DeleteAsync(int idAsync);
    }
}