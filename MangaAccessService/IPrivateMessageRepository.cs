using MangaModelService;

namespace MangaAccessService
{
    public interface IPrivateMessageRepository
    {
        Task<PrivateMessage> AddAsync(PrivateMessage message);

        Task<PrivateMessage> DeleteAsync(int messageId);

        Task<PrivateMessage> GetAsync(int messageId);

        Task<IEnumerable<PrivateMessage>> GetAllAsync();

        Task<PrivateMessage> UpdateAsync(PrivateMessage message);

        Task<IEnumerable<PrivateMessage>> GetMessagesByUserAsync(string userId);
    }
}