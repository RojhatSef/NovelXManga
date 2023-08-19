using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace NovelXManga.Pages.Register
{
    [Authorize(Roles = "Owner, AdminControl")]
    public class DeleteUserPageModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext Context;
        private readonly UserManager<UserModel> _userManager;

        public DeleteUserPageModel(MangaNNovelAuthDBContext context, UserManager<UserModel> userManager)
        {
            Context = context;
            _userManager = userManager;
        }

        public async Task DeleteUser(string userId)
        {
            var user = Context.UserModels.Find(userId);
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
                user.Allias = null;
                user.ForumName = "Deleted User";
                // ... [Set other PII to null or generic values]

                // Replace comments with a generic message
                foreach (var post in user.PostModel)
                {
                    post.postComment = "This comment has been removed";
                }

                Context.SaveChanges();
            }
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

        public IActionResult OnGetSearchUsers(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return new JsonResult(new List<UserModel>());
            }

            searchTerm = searchTerm.ToLower();

            var users = _userManager.Users
                .Where(u => u.UserName.ToLower().StartsWith(searchTerm) ||
                            u.Email.ToLower().StartsWith(searchTerm))
                .Select(u => new
                {
                    u.UserName,
                    u.Email,
                    u.Id
                })
                .ToList();

            return new JsonResult(users);
        }

        //public async Task<IActionResult> OnGetSearchAsync(string searchTerm)
        //{
        //    var currentUser = await _userManager.GetUserAsync(User);

        //    // Check if the user is authenticated
        //    if (currentUser == null) return new JsonResult(new { status = "error", message = "Not authenticated" });

        //    // Check if the user is an Owner or AdminControl
        //    if (!(await _userManager.IsInRoleAsync(currentUser, "Owner") ||
        //          await _userManager.IsInRoleAsync(currentUser, "AdminControl")))
        //    {
        //        return new JsonResult(new { status = "error", message = "Description of the error" });
        //    }

        //    // Fetching users based on the provided searchTerm
        //    var users = await Context.Users.OfType<UserModel>()
        // .Where(u => u.UserName.Contains(searchTerm))
        // .ToListAsync();

        //    var result = new List<object>();
        //    foreach (var user in users)
        //    {
        //        var roles = await _userManager.GetRolesAsync(user);
        //        result.Add(new
        //        {
        //            user.Id,
        //            user.UserName,
        //            RoleName = string.Join(", ", roles)
        //        });
        //    }

        //    return new JsonResult(new { status = "success", data = result });
        //}

        public async Task<IActionResult> OnPostDeleteUser(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var targetUser = await _userManager.FindByIdAsync(userId);

            if (currentUser == null || targetUser == null)
            {
                return NotFound(); // User not found
            }

            if (currentUser.Id == targetUser.Id)
            {
                return Forbid();
            }

            // If the target is the owner, only another owner can delete
            if (await _userManager.IsInRoleAsync(targetUser, "Owner"))
            {
                if (!await _userManager.IsInRoleAsync(currentUser, "Owner"))
                    return Forbid(); // Forbidden, only an owner can delete another owner
            }
            // If the target is AdminControl, only an Owner can delete
            else if (await _userManager.IsInRoleAsync(targetUser, "AdminControl"))
            {
                if (!await _userManager.IsInRoleAsync(currentUser, "Owner"))
                    return Forbid(); // Forbidden, only an owner can delete AdminControl
            }
            // Otherwise, it's okay to delete
            else
            {
                await DeleteUser(userId);
                return RedirectToPage("/Success"); // or whatever redirection you want after deletion
            }

            return Forbid(); // If none of the above conditions are met, forbid the action
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