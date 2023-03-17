using MangaModelService;

namespace MangaAccessService
{
    public interface ICompletedBookListPageRepsitory
    {
        IEnumerable<CompletedBookList> GetAllModels();

        CompletedBookList GetModel(int id);

        CompletedBookList Update(CompletedBookList UpdateModel);

        CompletedBookList Add(CompletedBookList addNewModel);

        CompletedBookList Delete(int id);

        Task<IEnumerable<CompletedBookList>> GetAllModelAsync();

        Task<CompletedBookList> GetModelAsync(int idAsync);

        Task<CompletedBookList> UpdateAsync(CompletedBookList UpdateModelAsync);

        Task<CompletedBookList> AddAsync(CompletedBookList AddAsync);

        Task<CompletedBookList> DeleteAsync(int idAsync);
    }
}