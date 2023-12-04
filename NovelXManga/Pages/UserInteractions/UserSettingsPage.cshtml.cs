using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using MangaAccessService;

namespace NovelXManga.Pages.UserInteractions
{
    public class UserSettingsPageModel : PageModel
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly MangaNNovelAuthDBContext _context;

        public UserSettings UserSettings { get; set; }

        public UserSettingsPageModel(UserManager<UserModel> userManager, MangaNNovelAuthDBContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Index");
            }

            UserSettings = await _context.UserSettings.FindAsync(user.Id);
            if (UserSettings == null)
            {
                // Handle the case where UserSettings is not found
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(UserSettings settings)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Index");
            }

            var currentSettings = await _context.UserSettings.FindAsync(user.Id);
            if (currentSettings == null)
            {
                // Handle the case where UserSettings is not found
            }

            // Update settings
            currentSettings.DarkModeEnabled = UserSettings.DarkModeEnabled;
            currentSettings.ShowMatureContent = settings.ShowMatureContent;
            // ... update other settings ...

            await _context.SaveChangesAsync();

            return RedirectToPage("/UserSettingsPage");
        }
    }
}