using MangaModelService;

namespace MangaAccessService
{
    public interface IPrivateMessageReportRepository
    {
        Task<PrivateMessageReport> AddAsync(PrivateMessageReport report);

        Task<PrivateMessageReport> DeleteAsync(int reportId);

        Task<PrivateMessageReport> GetAsync(int reportId);

        Task<IEnumerable<PrivateMessageReport>> GetAllAsync();

        Task<PrivateMessageReport> UpdateAsync(PrivateMessageReport report);

        Task<IEnumerable<PrivateMessageReport>> GetByStatusAsync(ReportStatus status);
    }
}