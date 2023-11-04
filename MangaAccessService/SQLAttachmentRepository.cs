using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLAttachmentRepository : IAttachmentRepository
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLAttachmentRepository(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public async Task<Attachment> AddAsync(Attachment attachment)
        {
            await context.Attachments.AddAsync(attachment);
            await context.SaveChangesAsync();
            return attachment;
        }

        public async Task<Attachment> DeleteAsync(int id)
        {
            var attachmentToDelete = await context.Attachments.FindAsync(id);
            if (attachmentToDelete != null)
            {
                context.Attachments.Remove(attachmentToDelete);
                await context.SaveChangesAsync();
            }
            return attachmentToDelete;
        }

        public async Task<Attachment> GetAsync(int id)
        {
            return await context.Attachments.FindAsync(id);
        }

        public async Task<IEnumerable<Attachment>> GetAllAsync()
        {
            return await context.Attachments.ToListAsync();
        }

        public async Task<Attachment> UpdateAsync(Attachment attachment)
        {
            var attachmentUpdate = context.Attachments.Attach(attachment);
            attachmentUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return attachment;
        }
    }
}