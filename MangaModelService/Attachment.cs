using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }

        public int MessageId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }

        public virtual PrivateMessage PrivateMessage { get; set; }
    }
}