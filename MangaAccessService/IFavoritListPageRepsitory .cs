using MangaModelService;

namespace MangaAccessService
{
    public interface IFavoritListPageRepsitory
    {
        IEnumerable<FavoritBookList> GetAllModels();

        FavoritBookList GetModel(int id);

        FavoritBookList Update(FavoritBookList UpdateModel);

        FavoritBookList Add(FavoritBookList addNewModel);

        FavoritBookList Delete(int id);

        Task<IEnumerable<FavoritBookList>> GetAllModelAsync();

        Task<FavoritBookList> GetModelAsync(int idAsync);

        Task<FavoritBookList> UpdateAsync(FavoritBookList UpdateModelAsync);

        Task<FavoritBookList> AddAsync(FavoritBookList AddAsync);

        Task<FavoritBookList> DeleteAsync(int idAsync);
    }
}