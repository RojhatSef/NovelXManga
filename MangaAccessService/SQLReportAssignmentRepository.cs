using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLReportAssignmentRepository : IReportAssignmentRepository
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLReportAssignmentRepository(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public async Task<ReportAssignment> AddAsync(ReportAssignment reportAssignment)
        {
            await context.ReportAssignments.AddAsync(reportAssignment);
            await context.SaveChangesAsync();
            return reportAssignment;
        }

        public async Task<ReportAssignment> DeleteAsync(int assignmentId)
        {
            var assignmentToDelete = await context.ReportAssignments.FindAsync(assignmentId);
            if (assignmentToDelete != null)
            {
                context.ReportAssignments.Remove(assignmentToDelete);
                await context.SaveChangesAsync();
            }
            return assignmentToDelete;
        }

        public async Task<ReportAssignment> GetAsync(int assignmentId)
        {
            return await context.ReportAssignments.Include(ra => ra.UpdateLogs).FirstOrDefaultAsync(ra => ra.AssignmentId == assignmentId);
        }

        public async Task<IEnumerable<ReportAssignment>> GetAllAsync()
        {
            return await context.ReportAssignments.Include(ra => ra.UpdateLogs).ToListAsync();
        }

        public async Task<ReportAssignment> UpdateAsync(ReportAssignment reportAssignment)
        {
            context.ReportAssignments.Update(reportAssignment);
            await context.SaveChangesAsync();
            return reportAssignment;
        }
    }
}