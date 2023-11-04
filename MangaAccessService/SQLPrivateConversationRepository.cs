using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLPrivateConversationRepository : IPrivateConversationRepository
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLPrivateConversationRepository(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public async Task<PrivateConversation> AddAsync(PrivateConversation conversation)
        {
            await context.PrivateConversations.AddAsync(conversation);
            await context.SaveChangesAsync();
            return conversation;
        }

        public async Task<PrivateConversation> DeleteAsync(int conversationId)
        {
            var conversationToDelete = await context.PrivateConversations.FindAsync(conversationId);
            if (conversationToDelete != null)
            {
                context.PrivateConversations.Remove(conversationToDelete);
                await context.SaveChangesAsync();
            }
            return conversationToDelete;
        }

        public async Task<PrivateConversation> GetAsync(int conversationId)
        {
            return await context.PrivateConversations
                                .Include(c => c.UserOne)
                                .Include(c => c.UserTwo)
                                .Include(c => c.PrivateMessages)
                                .FirstOrDefaultAsync(c => c.Id == conversationId);
        }

        public async Task<IEnumerable<PrivateConversation>> GetAllAsync()
        {
            return await context.PrivateConversations
                                .Include(c => c.UserOne)
                                .Include(c => c.UserTwo)
                                .Include(c => c.PrivateMessages)
                                .ToListAsync();
        }

        public async Task<PrivateConversation> UpdateAsync(PrivateConversation conversation)
        {
            var conversationToUpdate = context.PrivateConversations.Attach(conversation);
            conversationToUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return conversation;
        }

        public async Task<IEnumerable<PrivateConversation>> GetConversationsByUserIdAsync(string userId)
        {
            return await context.PrivateConversations
                                .Include(c => c.UserOne)
                                .Include(c => c.UserTwo)
                                .Include(c => c.PrivateMessages)
                                .Where(c => c.UserOneId == userId || c.UserTwoId == userId)
                                .ToListAsync();
        }
    }
}