using MangaModelService;

namespace MangaAccessService
{
    public interface ITagRepsitory
    {
        IEnumerable<TagModel> Search(string searchTerm);

        IEnumerable<TagModel> GetAllModels();

        TagModel GetModel(int id);

        TagModel Update(TagModel UpdateModel);

        TagModel Add(TagModel addNewModel);

        TagModel Delete(int id);

        Task<IEnumerable<TagModel>> SearchAsync(string searchTermAsync);

        Task<IEnumerable<TagModel>> GetAllModelAsync();

        Task<TagModel> GetModelAsync(int idAsync);

        Task<TagModel> UpdateAsync(TagModel UpdateModelAsync);

        Task<TagModel> AddAsync(TagModel AddAsync);

        Task<TagModel> DeleteAsync(int idAsync);
    }
}