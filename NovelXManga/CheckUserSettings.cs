using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace NovelXManga
{
    public class CheckUserSettings
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly MangaNNovelAuthDBContext _context;

        public CheckUserSettings(UserManager<UserModel> userManager, MangaNNovelAuthDBContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<UserSettings> GetUserSettingsAsync(ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                // Handle the case when the user is not logged in or user is not found
                return null;
            }

            var userSettings = await _context.UserSettings.FindAsync(user.Id);

            if (userSettings == null)
            {
                // Handle the case where UserSettings are not found
                return null;
            }

            return userSettings;
        }
    }
}