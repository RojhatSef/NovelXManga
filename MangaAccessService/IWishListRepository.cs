using MangaModelService;

namespace MangaAccessService
{
    public interface IWishListRepository
    {
        Task<IEnumerable<WishBookList>> GetAllWishListsAsync();

        Task<WishBookList> GetWishListByIdAsync(int id);

        Task<WishBookList> AddWishListAsync(WishBookList wishList);

        Task<WishBookList> UpdateWishListAsync(WishBookList wishList);

        Task<WishBookList> DeleteWishListAsync(int id);
    }
}