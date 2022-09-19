using Microsoft.AspNetCore.Identity;

namespace MangaModelService
{
    public class UserModel : IdentityUser
    {

        public string? Allias { get; set; }
        public string? ForumName { get; set; }



        //navigation 
        public ICollection<GroupScanlatingModel>? GroupScanlating { get; set; }
        public ICollection<MasterModel>? MasterModel { get; set; }
        public ICollection<PostModel>? PostModel { get; set; }
    }
}
