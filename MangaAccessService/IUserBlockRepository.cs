using MangaModelService;

namespace MangaAccessService
{
    public interface IUserBlockRepository
    {
        Task<UserBlock> AddAsync(UserBlock userBlock);

        Task<UserBlock> DeleteAsync(int blockId);

        Task<UserBlock> GetAsync(int blockId);

        Task<IEnumerable<UserBlock>> GetAllAsync();

        Task<UserBlock> UpdateAsync(UserBlock userBlock);
    }
}