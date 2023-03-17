using MangaModelService;

namespace MangaAccessService
{
    public interface IChapterModelRepsitory
    {
        IEnumerable<ChapterModel> Search(string searchTerm);

        IEnumerable<ChapterModel> GetAllModels();

        ChapterModel GetModel(int id);

        ChapterModel Update(ChapterModel UpdateModel);

        ChapterModel Add(ChapterModel addNewModel);

        ChapterModel Delete(int id);

        Task<IEnumerable<ChapterModel>> SearchAsync(string searchTermAsync);

        Task<IEnumerable<ChapterModel>> GetAllModelAsync();

        Task<ChapterModel> GetModelAsync(int idAsync);

        Task<ChapterModel> UpdateAsync(ChapterModel UpdateModelAsync);

        Task<ChapterModel> AddAsync(ChapterModel AddAsync);

        Task<ChapterModel> DeleteAsync(int idAsync);
    }
}