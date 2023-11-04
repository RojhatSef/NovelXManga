using MangaModelService;

namespace MangaAccessService
{
    public interface IPrivateConversationRepository
    {
        Task<PrivateConversation> AddAsync(PrivateConversation conversation);

        Task<PrivateConversation> DeleteAsync(int conversationId);

        Task<PrivateConversation> GetAsync(int conversationId);

        Task<IEnumerable<PrivateConversation>> GetAllAsync();

        Task<PrivateConversation> UpdateAsync(PrivateConversation conversation);

        Task<IEnumerable<PrivateConversation>> GetConversationsByUserIdAsync(string userId);
    }
}