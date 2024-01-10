using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class UserSettings
    {
        [Key]
        public string UserModelId { get; set; }

        public bool showAdultContent { get; set; }
        public bool ShowMatureContent { get; set; }

        // Additional settings properties here
        public bool DarkModeEnabled { get; set; }

        public int FontSize { get; set; }
        public virtual ICollection<Languages>? PreferredLanguages { get; set; }
        public int ItemsPerPage { get; set; }

        public MangaReadingDirection ReadingDirection { get; set; }

        public virtual UserModel UserModel { get; set; }
        public bool ShowReadingList { get; set; } = true;
        public bool ShowCompletedList { get; set; } = true;
        public bool ShowDroppedList { get; set; } = true;
        public bool ShowWishList { get; set; } = true;
        public bool ShowFavoritList { get; set; } = true;
        public bool ShowReviews { get; set; } = true;
    }

    public enum MangaReadingDirection
    {
        LeftToRight,
        RightToLeft,

        Webtoon,
        TwoPageSpread
    }
}