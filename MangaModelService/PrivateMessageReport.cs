using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class PrivateMessageReport
    {
        [Key]
        public int ReportId { get; set; }

        public string ReportedById { get; set; }
        public int PrivateMessageId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string ReportReason { get; set; }

        public DateTime ReportTime { get; set; }

        public ReportStatus Status { get; set; }
        public bool Reviewed { get; set; }

        public virtual ICollection<ReportAssignment> Assignments { get; set; }

        public virtual UserModel ReportedBy { get; set; }
        public virtual PrivateMessage PrivateMessage { get; set; }
    }

    public enum ReportStatus
    {
        Untouched,  // New report, no action taken
        UnderReview, // Report is being looked into
        ActionTaken, // Some action has been taken
        Resolved    // The report has been resolved
    }
}