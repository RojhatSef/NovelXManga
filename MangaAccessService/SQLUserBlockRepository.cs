using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLUserBlockRepository : IUserBlockRepository
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLUserBlockRepository(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public async Task<UserBlock> AddAsync(UserBlock userBlock)
        {
            await context.UserBlocks.AddAsync(userBlock);
            await context.SaveChangesAsync();
            return userBlock;
        }

        public async Task<UserBlock> DeleteAsync(int blockId)
        {
            var blockToDelete = await context.UserBlocks.FindAsync(blockId);
            if (blockToDelete != null)
            {
                context.UserBlocks.Remove(blockToDelete);
                await context.SaveChangesAsync();
            }
            return blockToDelete;
        }

        public async Task<UserBlock> GetAsync(int blockId)
        {
            return await context.UserBlocks.FindAsync(blockId);
        }

        public async Task<IEnumerable<UserBlock>> GetAllAsync()
        {
            return await context.UserBlocks.ToListAsync();
        }

        public async Task<UserBlock> UpdateAsync(UserBlock userBlock)
        {
            var blockUpdate = context.UserBlocks.Attach(userBlock);
            blockUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return userBlock;
        }
    }
}