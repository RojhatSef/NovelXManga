using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NovelXManga.Pages.UserInteractions
{
    public class UserSettingsPageModel : PageModel
    {
        private readonly CheckUserSettings _checkUserSettings;
        private readonly MangaNNovelAuthDBContext _context;

        [BindProperty]
        public UserSettings UserSettings { get; set; }

        public UserSettingsPageModel(CheckUserSettings checkUserSettings, MangaNNovelAuthDBContext context)
        {
            _checkUserSettings = checkUserSettings;
            _context = context;
        }

        [BindProperty]
        public IEnumerable<SelectListItem> FontSizes
        {
            get
            {
                for (int i = 10; i <= 30; i++)
                {
                    yield return new SelectListItem(i.ToString(), i.ToString());
                }
            }
        }

        [BindProperty]
        public IEnumerable<SelectListItem> ItemsPerPageOptions
        {
            get
            {
                for (int i = 4; i <= 100; i += 4)
                {
                    yield return new SelectListItem(i.ToString(), i.ToString());
                }
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            UserSettings = await _checkUserSettings.GetUserSettingsAsync(User);

            if (UserSettings == null)
            {
                var referer = Request.Headers["Referer"].ToString();
                return !string.IsNullOrEmpty(referer) ? Redirect(referer) : RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var existingSettings = await _checkUserSettings.GetUserSettingsAsync(User);

            if (existingSettings == null)
            {
                var referer = Request.Headers["Referer"].ToString();
                return !string.IsNullOrEmpty(referer) ? Redirect(referer) : RedirectToPage("/Index");
            }

            // Update the properties of the existing settings
            existingSettings.DarkModeEnabled = UserSettings.DarkModeEnabled;
            existingSettings.showAdultContent = UserSettings.showAdultContent;
            existingSettings.ShowMatureContent = UserSettings.ShowMatureContent;
            existingSettings.ShowCompletedList = UserSettings.ShowCompletedList;
            existingSettings.ShowReviews = UserSettings.ShowReviews;
            existingSettings.ShowWishList = UserSettings.ShowWishList;
            existingSettings.ShowReadingList = UserSettings.ShowReadingList;
            existingSettings.ShowDroppedList = UserSettings.ShowDroppedList;
            existingSettings.ShowFavoritList = UserSettings.ShowFavoritList;
            existingSettings.FontSize = UserSettings.FontSize;
            existingSettings.ItemsPerPage = UserSettings.ItemsPerPage;

            // ... update other properties as needed ...

            _context.UserSettings.Update(existingSettings); // Explicitly mark the entity as modified
            await _context.SaveChangesAsync(); // Save the changes

            return Page(); // Redirect to the same page or another page as desired
        }
    }
}