using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class WallPapers
    {
        [Key]
        public int WallpaperID { get; set; }

        public string WallPaperPhotoPath { get; set; }
    }
}