using MangaModelService;

namespace MangaAccessService
{
    public interface IDroppedBookListPageRepsitory
    {
        IEnumerable<DroppedBookList> GetAllModels();

        DroppedBookList GetModel(int id);

        DroppedBookList Update(DroppedBookList UpdateModel);

        DroppedBookList Add(DroppedBookList addNewModel);

        DroppedBookList Delete(int id);

        Task<IEnumerable<DroppedBookList>> GetAllModelAsync();

        Task<DroppedBookList> GetModelAsync(int idAsync);

        Task<DroppedBookList> UpdateAsync(DroppedBookList UpdateModelAsync);

        Task<DroppedBookList> AddAsync(DroppedBookList AddAsync);

        Task<DroppedBookList> DeleteAsync(int idAsync);
    }
}