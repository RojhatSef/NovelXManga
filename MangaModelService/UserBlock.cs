using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class UserBlock
    {
        [Key]
        public int BlockId { get; set; }

        public string BlockerId { get; set; }
        public string BlockedUserId { get; set; }

        // Foreign key for UserSettings
        public string UserSettingsId { get; set; }

        public virtual UserModel Blocker { get; set; }
        public virtual UserModel BlockedUser { get; set; }
    }
}