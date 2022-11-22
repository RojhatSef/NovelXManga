using Microsoft.AspNetCore.Identity;

namespace MangaModelService
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}
