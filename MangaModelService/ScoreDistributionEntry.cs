using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class ScoreDistributionEntry
    {
        [Key]
        public int ScoreDistributionEntryID { get; set; }

        public int Score { get; set; }
        public int Count { get; set; }
        public int MangaModelId { get; set; } // Foreign key to MangaModel
        public MangaModel MangaModel { get; set; }
    }
}