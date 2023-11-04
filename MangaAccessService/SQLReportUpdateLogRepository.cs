using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLReportUpdateLogRepository : IReportUpdateLogRepository
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLReportUpdateLogRepository(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public async Task<ReportUpdateLog> AddAsync(ReportUpdateLog reportUpdateLog)
        {
            await context.ReportUpdateLogs.AddAsync(reportUpdateLog);
            await context.SaveChangesAsync();
            return reportUpdateLog;
        }

        public async Task<ReportUpdateLog> DeleteAsync(int logId)
        {
            var logToDelete = await context.ReportUpdateLogs.FindAsync(logId);
            if (logToDelete != null)
            {
                context.ReportUpdateLogs.Remove(logToDelete);
                await context.SaveChangesAsync();
            }
            return logToDelete;
        }

        public async Task<ReportUpdateLog> GetAsync(int logId)
        {
            return await context.ReportUpdateLogs.FindAsync(logId);
        }

        public async Task<IEnumerable<ReportUpdateLog>> GetAllAsync()
        {
            return await context.ReportUpdateLogs.ToListAsync();
        }

        public async Task<ReportUpdateLog> UpdateAsync(int logId, InteractionType interaction, string updateNotes)
        {
            var logToUpdate = await context.ReportUpdateLogs.FindAsync(logId);
            if (logToUpdate != null)
            {
                logToUpdate.Interaction = interaction;
                logToUpdate.UpdateNotes = updateNotes;
                logToUpdate.UpdateTime = DateTime.Now;
                context.ReportUpdateLogs.Update(logToUpdate);
                await context.SaveChangesAsync();
            }
            return logToUpdate;
        }
    }
}