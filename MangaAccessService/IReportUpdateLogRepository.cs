using MangaModelService;

namespace MangaAccessService
{
    public interface IReportUpdateLogRepository
    {
        Task<ReportUpdateLog> AddAsync(ReportUpdateLog reportUpdateLog);

        Task<ReportUpdateLog> DeleteAsync(int logId);

        Task<ReportUpdateLog> GetAsync(int logId);

        Task<IEnumerable<ReportUpdateLog>> GetAllAsync();

        Task<ReportUpdateLog> UpdateAsync(int logId, InteractionType interaction, string updateNotes);
    }
}