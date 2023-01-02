//using MangaAccessService;
//using MangaModelService;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace NovelXManga.Pages.Register
//{
//    [Authorize]
//    public class ALlAuthorsModel : PageModel
//    {
//        private readonly MangaNNovelAuthDBContext mangaNNovelAuthDBContext;
//        private readonly UserManager<IdentityUser> userManager;
//        public IEnumerable<AuthorModel> authorModel { get; set; }
//        public ALlAuthorsModel(MangaNNovelAuthDBContext mangaNNovelAuthDBContext, UserManager<IdentityUser> userManager)
//        {
//            this.mangaNNovelAuthDBContext = mangaNNovelAuthDBContext;
//            this.userManager = userManager;
//        }
//        public void OnGet()
//        {
//            authorModel = mangaNNovelAuthDBContext.authorModels;
//        }
//        //public IActionResult OnGet()
//        //{
//        //    var customers = .AspNetUsers
//        //            .Where(u => u.AspNetRoles.Any(r => r.Name == "Customer") &&
//        //                        u.IsActivated == true && u.IsClosed == false &&
//        //                        u.IsPaused == false && u.IsSuspended == false)
//        //            .ToList();
//        //}
//        //public IActionResult OnGet(string id)
//        //{
//        //    authorModel = userManager.GetUserAsync(id);
//        //    authorModel = mangaNNovelAuthDBContext.authorModels;
//        //}

//    }
//}
