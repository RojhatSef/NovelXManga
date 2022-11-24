using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Register
{
    public class CreateRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public CreateRoleModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [BindProperty]
        public CreateRoleViewModel roleViewModel { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = roleViewModel.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return new RedirectToPageResult("/Index");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return Page();
        }
    }
}
