using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLPrivateMessageReportRepository : IPrivateMessageReportRepository
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLPrivateMessageReportRepository(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public async Task<PrivateMessageReport> AddAsync(PrivateMessageReport report)
        {
            await context.PrivateMessageReports.AddAsync(report);
            await context.SaveChangesAsync();
            return report;
        }

        public async Task<PrivateMessageReport> DeleteAsync(int reportId)
        {
            var reportToDelete = await context.PrivateMessageReports.FindAsync(reportId);
            if (reportToDelete != null)
            {
                context.PrivateMessageReports.Remove(reportToDelete);
                await context.SaveChangesAsync();
            }
            return reportToDelete;
        }

        public async Task<PrivateMessageReport> GetAsync(int reportId)
        {
            return await context.PrivateMessageReports
                                .Include(r => r.Assignments)
                                .FirstOrDefaultAsync(r => r.ReportId == reportId);
        }

        public async Task<IEnumerable<PrivateMessageReport>> GetAllAsync()
        {
            return await context.PrivateMessageReports
                                .Include(r => r.Assignments)
                                .ToListAsync();
        }

        public async Task<PrivateMessageReport> UpdateAsync(PrivateMessageReport report)
        {
            var reportUpdate = context.PrivateMessageReports.Attach(report);
            reportUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<PrivateMessageReport>> GetByStatusAsync(ReportStatus status)
        {
            return await context.PrivateMessageReports
                                .Include(r => r.Assignments)
                                .Where(r => r.Status == status)
                                .ToListAsync();
        }
    }
}