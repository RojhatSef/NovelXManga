using Microsoft.AspNetCore.Identity;

namespace MangaModelService
{
    public class UserModel : IdentityUser
    {

        public string? Allias { get; set; }
        public string? ForumName { get; set; }

        public string? userPhotoPath { get; set; }

        //navigation 
        public int? groupScanlationID { get; set; }
        public ICollection<GroupScanlatingModel>? GroupScanlating { get; set; }
        public int? masterId { get; set; }
        public ICollection<MasterModel>? MasterModel { get; set; }
        public string? postModelID { get; set; }

        public ICollection<PostModel>? PostModel { get; set; }
    }
}
