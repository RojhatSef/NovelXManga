using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class MangaModel
    {
        [Key]
        public int MangaID { get; set; }

        [Required(ErrorMessage = "Name is requried"),
        MinLength(2, ErrorMessage = "Name must contain at least 2 characters")]

        public string MangaName { get; set; }
        public string AssociatedNames { get; set; }
        public string Type { get; set; }

        public string? PhotoPath { get; set; }

        public string Description { get; set; }
        public DateTime ReleaseYear { get; set; }
        public GenresModel? Genres { get; set; }
        public ThemeModel? Theme { get; set; }

        public ICollection<MasterModel> MasterModels { get; set; }

        //ICollection<StudioModel> StudioModels { get; set; }
        //ICollection<ArtistModel> ArtistModels { get; set; }
        //ICollection<AuthorModel> AuthorModels { get; set; }
        //ICollection<ChapterModel> ChaptersModels { get; set; }
        //ICollection<PostModel> PostsModels { get; set; }
        //ICollection<GroupScanlatingModel> GroupScanlatingModels { get; set; }

    }
}
