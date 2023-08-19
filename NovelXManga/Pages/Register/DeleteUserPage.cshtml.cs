using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace NovelXManga.Pages.Register
{
    [Authorize(Roles = "Owner, AdminControl")]
    public class DeleteUserPageModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext Context;
        private readonly UserManager<UserModel> _userManager;
        private readonly ILogger<DeleteUserPageModel> _logger;

        public DeleteUserPageModel(MangaNNovelAuthDBContext context, UserManager<UserModel> userManager, ILogger<DeleteUserPageModel> logger)
        {
            Context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public class UserToDelete
        {
            public string UserId { get; set; }
        }

        public async Task DeleteUserMethod(string userId)
        {
            var user = Context.UserModels.Include(u => u.Reviews).FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                // Set user as deleted
                user.IsDeleted = true;

                // Generate a unique identifier for the user to obfuscate their details
                var uniqueId = Guid.NewGuid().ToString();

                // Obfuscate primary user details
                user.UserName = "DeletedUser" + uniqueId;
                user.Email = "DeletedUser" + uniqueId + "@deleted.com";
                user.NormalizedEmail = user.Email.ToUpper();
                user.NormalizedUserName = user.UserName.ToUpper();
                // Set a complex random password
                var randomPassword = GenerateRandomPassword(6, 2);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, randomPassword);

                // Obfuscate other user data

                user.ForumName = "Deleted User";
                user.Allias = "N/A";

                user.ForgottPasswordFavoritAnimal = "N/A";
                user.ForgottPasswordFavActor = "N/A";
                user.ForgottPasswordFavoritPlace = "N/A";
                user.userPhotoPath = "N/A";
                user.nameInNativeLanguage = "N/A";
                user.placeOfBirth = "N/A";
                user.Zodiac = "N/A";
                user.Description = "N/A";
                user.Twitter = "N/A";
                user.IsShadowBanned = true;
                user.IsDeleted = true;
                // ... [Set other PII to null or generic values]

                // Modify user's reviews
                if (user.Reviews != null && user.Reviews.Any())
                {
                    foreach (var review in user.Reviews)
                    {
                        review.Title = "DeletedTitle";
                        review.Content = "DeletedContent";
                    }
                }
                // Replace comments with a generic message
                if (user.PostModel != null && user.PostModel.Any())
                {
                    foreach (var post in user.PostModel)
                    {
                        post.postComment = "This comment has been removed";
                    }
                }
                if (!await _userManager.IsInRoleAsync(user, "ShadowBanned"))
                {
                    await _userManager.AddToRoleAsync(user, "ShadowBanned");
                }

                Context.SaveChanges();
            }
        }

        public async Task<IActionResult> OnGet()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            // Check if the user is authenticated
            if (currentUser == null) return RedirectToPage("/Login");  // Adjust the redirection as needed

            // Check if the user is an Owner or AdminControl
            if (await _userManager.IsInRoleAsync(currentUser, "Owner") ||
                await _userManager.IsInRoleAsync(currentUser, "AdminControl"))
            {
                return Page(); // They can access this page
            }

            // Otherwise, they can't access the page
            return Forbid();
        }

        public async Task<IActionResult> OnGetSearchUsers(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return new JsonResult(new List<UserModel>());
            }

            searchTerm = searchTerm.ToLower();

            var users = await _userManager.Users
                .Where(u => u.UserName.ToLower().StartsWith(searchTerm) ||
                            u.Email.ToLower().StartsWith(searchTerm))
                .ToListAsync();

            var userRoles = new List<object>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles.Add(new
                {
                    user.UserName,
                    user.Email,
                    user.Id,
                    Roles = roles
                });
            }

            return new JsonResult(userRoles);
        }

        private string GenerateRandomPassword(int minLength = 6, int minUniqueChars = 2)
        {
            if (minUniqueChars > minLength)
                throw new ArgumentException("The number of unique chars can't be greater than the overall password length.");

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();

            string result;
            do
            {
                int length = random.Next(minLength, minLength + 5);
                result = new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            } while (result.Distinct().Count() < minUniqueChars);

            return result;
        }

        public async Task<IActionResult> OnPostDeleteUser([FromBody] UserToDelete request)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var targetUser = await _userManager.FindByIdAsync(request.UserId);

                if (currentUser == null || targetUser == null)
                {
                    return new JsonResult(new { success = false, message = "User not found." });
                }

                if (currentUser.Id == targetUser.Id)
                {
                    return new JsonResult(new { success = false, message = "You cannot delete yourself." });
                }

                // If the target is the owner, only another owner can delete
                if (await _userManager.IsInRoleAsync(targetUser, "Owner"))
                {
                    if (!await _userManager.IsInRoleAsync(currentUser, "Owner"))
                        return new JsonResult(new { success = false, message = "Forbidden, only an owner can delete another owner." });
                }
                // If the target is AdminControl, only an Owner can delete
                else if (await _userManager.IsInRoleAsync(targetUser, "AdminControl"))
                {
                    if (!await _userManager.IsInRoleAsync(currentUser, "Owner"))
                        return new JsonResult(new { success = false, message = "Forbidden, only an owner can delete AdminControl." });
                }
                // Otherwise, it's okay to delete
                else
                {
                    await DeleteUserMethod(request.UserId);
                    return new JsonResult(new { success = true, message = "User deleted successfully." });
                }

                return new JsonResult(new { success = false, message = "Forbidden action." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting user.");

                return new JsonResult(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}

/* things to do,
 Post get whose getting deleted.
Check if the User is allowed to get deleted. Owners can't be deleted.
Get
check the Yuser, is the Yuser even allowed to delete this XUser.

Owners can delete everyone, AdminController can Delete Everyone below.
No one else can delete.
 */