using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLWishListRepository : IWishListRepository
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLWishListRepository(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public async Task<WishBookList> AddWishListAsync(WishBookList wishList)
        {
            await context.Set<WishBookList>().AddAsync(wishList);
            await context.SaveChangesAsync();
            return wishList;
        }

        public async Task<WishBookList> DeleteWishListAsync(int id)
        {
            var wishList = await context.Set<WishBookList>().FindAsync(id);
            if (wishList != null)
            {
                context.Set<WishBookList>().Remove(wishList);
                await context.SaveChangesAsync();
            }
            return wishList;
        }

        public async Task<IEnumerable<WishBookList>> GetAllWishListsAsync()
        {
            return await context.Set<WishBookList>().ToListAsync();
        }

        public async Task<WishBookList> GetWishListByIdAsync(int id)
        {
            return await context.Set<WishBookList>().FindAsync(id);
        }

        public async Task<WishBookList> UpdateWishListAsync(WishBookList wishList)
        {
            var entity = context.Set<WishBookList>().Attach(wishList);
            entity.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return wishList;
        }
    }
}