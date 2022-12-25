using Microsoft.AspNetCore.Identity;

namespace MangaModelService
{
    public class UserModel : IdentityUser
    {

        public string? Allias { get; set; }
        public string? ForumName { get; set; }

        public string? userPhotoPath { get; set; }
        public string? nameInNativeLanguage { get; set; }
        public string? placeOfBirth { get; set; }
        public string? Zodiac { get; set; }
        public string? Description { get; set; }

        public string? Twitter { get; set; }

        public virtual ICollection<BlogModel>? UserBlogModel { get; set; }
        //navigation 
        public int? groupScanlationID { get; set; }
        public virtual ICollection<GroupScanlatingModel>? GroupScanlating { get; set; }
        public int? masterId { get; set; }
        public virtual ICollection<MasterModel>? MasterModel { get; set; }
        public string? postModelID { get; set; }

        public virtual ICollection<PostModel>? PostModel { get; set; }
    }
}
