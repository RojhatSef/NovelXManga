using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Manga
{
    public class CurrentMangaModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext Context;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBlogRepsitory blogRepsitory;
        private readonly IPostRepsitory postRepsitory;

        [BindProperty]
        public MangaModel CurrentManga { get; set; }

        [BindProperty]
        public BlogModel Blog { get; set; }

        [BindProperty]
        public PostModel Post { get; set; }

        public CurrentMangaModel(UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment, IMangaRepository mangaRepository, MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IBlogRepsitory blogRepsitory, IPostRepsitory postRepsitory)
        {
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.mangaRepository = mangaRepository;
            this.Context = mangaNNovelAuthDBContext;
            this.blogRepsitory = blogRepsitory;
            this.postRepsitory = postRepsitory;
        }

        public IActionResult OnPostCreateReply(int parentId, string comment)
        {
            var parentPost = Context.PostModels.FirstOrDefault(p => p.PostId == parentId);

            if (parentPost == null)
            {
                return NotFound();
            }

            var reply = new PostModel
            {
                Title = "Reply to " + parentPost.Title,
                postComment = comment,
                CommentPostedTime = DateTime.Now,
                ParentPostId = parentPost.PostId
            };

            Context.PostModels.Add(reply);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public void OnGet(int id)
        {
            if (id == 0)
            {
                NotFound();
            }
            CurrentManga = mangaRepository.GetOneMangaAllIncluded(id);
            Blog = blogRepsitory.GetModel(CurrentManga.BlogModelId);
        }
    }
}