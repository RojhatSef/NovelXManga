using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class ReportAssignment
    {
        [Key]
        public int AssignmentId { get; set; }

        public int ReportId { get; set; }
        public string AdminId { get; set; } // ID of the admin or owner
        public DateTime AssignmentTime { get; set; }

        public string? Notes { get; set; } // Notes by the admin or owner who reviewed the report
        public virtual ICollection<ReportUpdateLog> UpdateLogs { get; set; }
        public virtual PrivateMessageReport Report { get; set; }
        public virtual UserModel Admin { get; set; }
    }
}