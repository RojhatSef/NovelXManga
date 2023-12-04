using MangaAccessService;
using MangaAccessService.DTO.UserProfileMangaDTO;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace NovelXManga.Pages.UserInteractions
{
    public class UserProfileModel : PageModel
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly MangaNNovelAuthDBContext Context;

        private readonly UserBookListService _bookListService; // Add this line
        private const string UserIdSessionKey = "UserProfile_UserId";
        public UserSettings UserSettings { get; set; }

        #region Properties to hold the DTOs

        public IEnumerable<UserMangaImageDto> ReadingListDtos { get; private set; }

        public IEnumerable<UserMangaImageDto> CompletedListDtos { get; private set; }
        public IEnumerable<UserMangaImageDto> DroppedListDtos { get; private set; }
        public IEnumerable<UserMangaImageDto> FavoriteListDtos { get; private set; }
        public IEnumerable<UserMangaImageDto> WishListDtos { get; private set; }

        #endregion Properties to hold the DTOs

        public UserProfileModel(UserManager<UserModel> userManager, UserBookListService bookListService, MangaNNovelAuthDBContext context)
        {
            _userManager = userManager;
            _bookListService = bookListService; // Add this line
            Context = context;
        }

        [BindProperty]
        public UserModel UserProfile { get; set; }

        public bool IsProfileOwner { get; set; }
        public bool IsLoggedIn { get; set; }
        public string UserRole { get; set; }

        #region OnGetLoadMore Asyncs for Reading, Wish, Dropped, Complete, & Favorite

        //Reading
        public async Task<JsonResult> OnGetLoadMoreReadingAsync(string userId, int skip, int take)
        {
            var additionalBooks = await _bookListService.GetReadingListMangaImages(userId, skip, take);
            return new JsonResult(additionalBooks);
        }

        //complete
        public async Task<JsonResult> OnGetLoadMoreCompletedAsync(string userId, int skip, int take)
        {
            var additionalBooks = await _bookListService.GetCompletedListMangaImages(userId, skip, take);
            return new JsonResult(additionalBooks);
        }

        //Dropped
        public async Task<JsonResult> OnGetLoadMoreDroppedAsync(string userId, int skip, int take)
        {
            var additionalBooks = await _bookListService.GetDroppedListMangaImages(userId, skip, take);
            return new JsonResult(additionalBooks);
        }

        //Wish
        public async Task<JsonResult> OnGetLoadMoreWishAsync(string userId, int skip, int take)
        {
            var additionalBooks = await _bookListService.GetWishListMangaImages(userId, skip, take);
            return new JsonResult(additionalBooks);
        }

        //Favorite
        public async Task<JsonResult> OnGetLoadMoreFavoriteAsync(string userId, int skip, int take)
        {
            var additionalBooks = await _bookListService.GetFavoriteListMangaImages(userId, skip, take);
            return new JsonResult(additionalBooks);
        }

        #endregion OnGetLoadMore Asyncs for Reading, Wish, Dropped, Complete, & Favorite

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            UserProfile = await _userManager.Users
                       .FirstOrDefaultAsync(u => u.Id == userId);

            if (UserProfile == null)
            {
                return NotFound();
            }
            UserSettings = await Context.UserSettings
                               .Include(us => us.UserModel)
                               .FirstOrDefaultAsync(us => us.UserModelId == UserProfile.Id);

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