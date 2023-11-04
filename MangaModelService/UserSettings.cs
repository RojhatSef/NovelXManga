using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class UserSettings
    {
        [Key, ForeignKey("UserModel")]
        public string UserModelId { get; set; }

        public bool ShowMatureContent { get; set; }

        // Additional settings properties here
        public bool DarkModeEnabled { get; set; }

        public int FontSize { get; set; }
        public virtual ICollection<Languages>? PreferredLanguages { get; set; }
        public int ItemsPerPage { get; set; }
        public virtual ICollection<UserBlock> BlockedUsers { get; set; }
        public MangaReadingDirection ReadingDirection { get; set; }

        public virtual UserModel UserModel { get; set; }
    }

    public enum MangaReadingDirection
    {
        LeftToRight,
        RightToLeft,

        Webtoon,
        TwoPageSpread
    }
}