using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class MasterModel
    {
        [Key]
        public int MasterID { get; set; }
        //mangamodel / 
        public int? GroupScanlatingID { get; set; }
        public virtual ICollection<GroupScanlatingModel>? GroupScanlating { get; set; }
        public string? userId { get; set; }
        public virtual ICollection<UserModel>? userModels { get; set; }



        [Required]
        public MangaModel? MangaModels { get; set; }

        //[Key]
        //public int StudioModelID { get; set; }
        //public StudioModel StudioModel { get; set; }
        //[Key]
        //public int GroupsSanlatingId { get; set; }
        //public GroupScanlatingModel GroupScanlatingModel { get; set; }
        //[Key]
        //public int ChapterModelId { get; set; }
        //public ChapterModel chapterModel { get; set; }
        //[Key]
        //public int AuthorModelID { get; set; }
        //public AuthorModel AuthorModel { get; set; }
        //[Key]
        //public int ArtistModelID { get; set; }
        //public ArtistModel ArtistModel { get; set; }
        //[Key]
        //public int MangaModelId { get; set; }
        //public MangaModel mangaModel { get; set; }
    }
}
