using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class ReportUpdateLog
    {
        [Key]
        public int LogId { get; set; }

        public int ReportAssignmentId { get; set; }
        public string UpdatedByAdminId { get; set; }
        public DateTime UpdateTime { get; set; }
        public InteractionType Interaction { get; set; } // Indicates if the report was viewed or updated
        public string? UpdateNotes { get; set; } // Notes about the update, if any

        [ForeignKey("ReportAssignmentId")]
        public virtual ReportAssignment ReportAssignment { get; set; }

        [ForeignKey("UpdatedByAdminId")]
        public virtual UserModel UpdatedByAdmin { get; set; }
    }

    public enum InteractionType
    {
        Viewed,    // The report was viewed
        Updated,   // The report was updated
        ActionTaken // Some action was taken
    }
}