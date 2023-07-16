using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Roles
{
    public class UpdateUserRolesModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<UserModel> userManager;
        public IEnumerable<UserModel> usersList { get; set; }

        public UpdateUserRolesModel(MangaNNovelAuthDBContext context, RoleManager<IdentityRole> roleManager, UserManager<UserModel> userManager)
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