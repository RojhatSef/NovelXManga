using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLPrivateMessageRepository : IPrivateMessageRepository
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLPrivateMessageRepository(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public async Task<PrivateMessage> AddAsync(PrivateMessage message)
        {
            await context.PrivateMessages.AddAsync(message);
            await context.SaveChangesAsync();
            return message;
        }

        public async Task<PrivateMessage> DeleteAsync(int messageId)
        {
            var messageToDelete = await context.PrivateMessages.FindAsync(messageId);
            if (messageToDelete != null)
            {
                context.PrivateMessages.Remove(messageToDelete);
                await context.SaveChangesAsync();
            }
            return messageToDelete;
        }

        public async Task<PrivateMessage> GetAsync(int messageId)
        {
            return await context.PrivateMessages
                                .Include(m => m.Sender)
                                .Include(m => m.Receiver)
                                .Include(m => m.PrivateMessageReports)
                                .Include(m => m.Attachments)
                                .FirstOrDefaultAsync(m => m.Id == messageId);
        }

        public async Task<IEnumerable<PrivateMessage>> GetAllAsync()
        {
            return await context.PrivateMessages
                                .Include(m => m.Sender)
                                .Include(m => m.Receiver)
                                .Include(m => m.PrivateMessageReports)
                                .Include(m => m.Attachments)
                                .ToListAsync();
        }

        public async Task<PrivateMessage> UpdateAsync(PrivateMessage message)
        {
            var messageToUpdate = context.PrivateMessages.Attach(message);
            messageToUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return message;
        }

        public async Task<IEnumerable<PrivateMessage>> GetMessagesByUserAsync(string userId)
        {
            return await context.PrivateMessages
                                .Include(m => m.Sender)
                                .Include(m => m.Receiver)
                                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                                .ToListAsync();
        }
    }
}