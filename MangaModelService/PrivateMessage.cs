using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class PrivateMessage
    {
        [Key]
        public int Id { get; set; }

        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }

        public DateTime SentTime { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsArchived { get; set; }
        public bool IsImportant { get; set; }

        public virtual UserModel Sender { get; set; }
        public virtual UserModel Receiver { get; set; }
        public virtual ICollection<PrivateMessageReport> PrivateMessageReports { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}