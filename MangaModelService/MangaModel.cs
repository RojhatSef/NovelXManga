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


        public int BlogModelID { get; set; }
        public BlogModel BlogModel { get; set; }

        public int MasterModelID { get; set; }
        public MasterModel MasterModels { get; set; }
        public ICollection<GenresModel> GenresModels { get; set; }
        public ICollection<TagModel> TagsModels { get; set; }

        //public ICollection<StudioModel> StudioModels { get; set; }
        //public ICollection<ArtistModel> ArtistModels { get; set; }
        //public ICollection<AuthorModel> AuthorModels { get; set; }
        //public ICollection<ChapterModel> ChaptersModels { get; set; }

        //public ICollection<GroupScanlatingModel> GroupScanlatingModels { get; set; }

    }
}
