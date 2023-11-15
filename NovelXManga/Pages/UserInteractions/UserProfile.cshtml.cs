using MangaAccessService;
using MangaModelService; // Import the namespace where UserModel is located if it's different
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace NovelXManga.Pages.UserInteractions
{
    public class UserProfileModel : PageModel
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly MangaNNovelAuthDBContext Context;

        public UserProfileModel(UserManager<UserModel> userManager, MangaNNovelAuthDBContext context)
        {
            _userManager = userManager;
            Context = context;
        }

        public UserModel UserProfile { get; set; }
        public bool IsProfileOwner { get; set; }
        public bool IsLoggedIn { get; set; }
        public string UserRole { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await _userManager.GetRolesAsync(user);
            string roleName = roles.Any() ? string.Join(", ", roles) : "No Roles";
            UserProfile = user; // Here we're setting the user directly
            UserRole = roleName;
            IsLoggedIn = User.Identity.IsAuthenticated;
            IsProfileOwner = User.FindFirstValue(ClaimTypes.NameIdentifier) == userId;

            return Page();
        }
    }
}