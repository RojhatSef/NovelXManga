using MangaAccessService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Roles
{
    public class UpdateUserRolesModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        public IEnumerable<IdentityUser> usersList { get; set; }


        public UpdateUserRolesModel(MangaNNovelAuthDBContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public void OnGet()
        {
            usersList = userManager.Users;


        }
        //public async Task<IActionResult> OnPostAsync()
        //{

        //    return
        //}
    }
}
