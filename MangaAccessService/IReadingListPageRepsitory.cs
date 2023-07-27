using MangaModelService;

namespace MangaAccessService
{
    public interface IReadingListPageRepsitory
    {
        IEnumerable<ReadingList> GetAllModels();

        ReadingList GetModel(int id);

        ReadingList Update(ReadingList UpdateModel);

        ReadingList Add(ReadingList addNewModel);

        ReadingList Delete(int id);

        Task<IEnumerable<ReadingList>> GetAllModelAsync();

        Task<ReadingList> GetModelAsync(int idAsync);

        Task<ReadingList> UpdateAsync(ReadingList UpdateModelAsync);

        Task<ReadingList> AddAsync(ReadingList AddAsync);

        Task<ReadingList> DeleteAsync(int idAsync);

        Task<int> GetReadingListCountForMangaAsync(int mangaId);
    }
}