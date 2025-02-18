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
        /* things to do,
 Post get whose getting deleted.
Check if the User is allowed to get deleted. Owners can't be deleted.
Get
check the Yuser, is the Yuser even allowed to delete this XUser.

Owners can delete everyone, AdminController can Delete Everyone below.
No one else can delete.
 */

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
                //user.UserName = "DeletedUser" + uniqueId;
                //user.Email = "DeletedUser" + uniqueId + "@deleted.com";
                user.NormalizedEmail = user.Email.ToUpper();
                user.NormalizedUserName = user.UserName.ToUpper();
                // Set a complex random password
                var randomPassword = GenerateRandomPassword(15, 3);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, randomPassword);

                // Obfuscate other user data
                user.ForumName = "Deleted User";
                user.Allias = "N/A";

                user.userPhotoPath = "N/A";

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
                return new JsonResult(new List<object>()); // Return an empty array if no search term is provided
            }

            searchTerm = searchTerm.ToLower();

            // Fetch users and their roles efficiently
            var users = await _userManager.Users
                .Where(u => u.UserName.ToLower().Contains(searchTerm) ||
                            u.Email.ToLower().Contains(searchTerm) ||
                            (u.Allias != null && u.Allias.ToLower().Contains(searchTerm)))
                .Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.Email,
                    Alias = u.Allias ?? "No Alias",
                })
                .ToListAsync();

            var userRoles = new List<object>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id));
                userRoles.Add(new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    user.Alias,
                    Roles = roles
                });
            }

            return new JsonResult(userRoles);
        }

        private string GenerateRandomPassword(int minLength = 15, int minUniqueChars = 3)
        {
            if (minUniqueChars > minLength)
                throw new ArgumentException("The number of unique chars can't be greater than the overall password length.");

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            var random = new Random();

            string result;
            do
            {
                int length = random.Next(minLength, minLength + 5);
                var randomChars = Enumerable.Repeat(chars, length - minUniqueChars)
                    .Select(s => s[random.Next(s.Length)]).ToArray();
                var randomDigits = Enumerable.Repeat(digits, minUniqueChars)
                    .Select(s => s[random.Next(s.Length)]).ToArray();
                result = new string(randomChars.Concat(randomDigits).OrderBy(s => random.Next()).ToArray());
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