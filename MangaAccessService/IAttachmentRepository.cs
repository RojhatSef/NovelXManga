using MangaModelService;

namespace MangaAccessService
{
    public interface IAttachmentRepository
    {
        Task<Attachment> AddAsync(Attachment attachment);

        Task<Attachment> DeleteAsync(int id);

        Task<Attachment> GetAsync(int id);

        Task<IEnumerable<Attachment>> GetAllAsync();

        Task<Attachment> UpdateAsync(Attachment attachment);
    }
}