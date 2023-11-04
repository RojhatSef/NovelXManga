using MangaModelService;

namespace MangaAccessService
{
    public interface IReportAssignmentRepository
    {
        Task<ReportAssignment> AddAsync(ReportAssignment reportAssignment);

        Task<ReportAssignment> DeleteAsync(int assignmentId);

        Task<ReportAssignment> GetAsync(int assignmentId);

        Task<IEnumerable<ReportAssignment>> GetAllAsync();

        Task<ReportAssignment> UpdateAsync(ReportAssignment reportAssignment);
    }
}