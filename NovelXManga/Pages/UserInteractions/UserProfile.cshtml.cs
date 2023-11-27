using MangaAccessService;
using MangaAccessService.DTO.UserProfileMangaDTO;
using MangaModelService;
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

        private readonly UserBookListService _bookListService; // Add this line
        private const string UserIdSessionKey = "UserProfile_UserId";

        // Properties to hold the DTOs
        public IEnumerable<UserMangaImageDto> ReadingListDtos { get; private set; }

        public IEnumerable<UserMangaImageDto> CompletedListDtos { get; private set; }
        public IEnumerable<UserMangaImageDto> DroppedListDtos { get; private set; }
        public IEnumerable<UserMangaImageDto> FavoriteListDtos { get; private set; }
        public IEnumerable<UserMangaImageDto> WishListDtos { get; private set; }

        public UserProfileModel(UserManager<UserModel> userManager, UserBookListService bookListService)
        {
            _userManager = userManager;
            _bookListService = bookListService; // Add this line
        }

        public UserModel UserProfile { get; set; }
        public bool IsProfileOwner { get; set; }
        public bool IsLoggedIn { get; set; }
        public string UserRole { get; set; }

        public async Task<JsonResult> OnGetLoadMoreReadingAsync(string userId, int skip, int take)
        {
            var additionalBooks = await _bookListService.GetReadingListMangaImages(userId, skip, take);
            return new JsonResult(additionalBooks);
        }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            UserProfile = await _userManager.FindByIdAsync(userId);

            if (UserProfile == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetString(UserIdSessionKey, userId);
            var roles = await _userManager.GetRolesAsync(UserProfile);
            UserRole = roles.Any() ? string.Join(", ", roles) : "No Roles";
            IsLoggedIn = User.Identity.IsAuthenticated;
            IsProfileOwner = User.FindFirstValue(ClaimTypes.NameIdentifier) == userId;

            // Use the service to get DTOs for each list
            ReadingListDtos = await _bookListService.GetReadingListMangaImages(userId); // Adjust skip and take as needed

            CompletedListDtos = await _bookListService.GetCompletedListMangaImages(userId);
            DroppedListDtos = await _bookListService.GetDroppedListMangaImages(userId);
            FavoriteListDtos = await _bookListService.GetFavoriteListMangaImages(userId);
            WishListDtos = await _bookListService.GetWishListMangaImages(userId);

            return Page();
        }
    }
}