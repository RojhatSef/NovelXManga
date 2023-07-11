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
        private readonly IReviewRepsitory reviewRepsitory;
        private readonly IBlogRepsitory blogRepsitory;
        private readonly IPostRepsitory postRepsitory;
        private readonly ICharacterRepsitory characterRepsitory;

        [BindProperty]
        public MangaModel CurrentManga { get; set; }

        [BindProperty]
        public BlogModel Blog { get; set; }

        [BindProperty]
        public PostModel OnePost { get; set; }

        public IEnumerable<Review> ReivewModel { get; set; }

        public IEnumerable<PostModel> Posts { get; set; }

        public CurrentMangaModel(UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment, IMangaRepository mangaRepository, MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IBlogRepsitory blogRepsitory, IPostRepsitory postRepsitory, ICharacterRepsitory characterRepsitory, IReviewRepsitory reviewRepsitory)
        {
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.mangaRepository = mangaRepository;
            this.Context = mangaNNovelAuthDBContext;
            this.blogRepsitory = blogRepsitory;
            this.postRepsitory = postRepsitory;
            this.characterRepsitory = characterRepsitory;
            this.reviewRepsitory = reviewRepsitory;
        }

        public IActionResult OnPostCreateReply(MangaModel id, int parentId, string comment)
        {
            var parentPost = Context.PostModels.FirstOrDefault(p => p.PostId == parentId);

            if (parentPost == null)
            {
                return NotFound();
            }
            CurrentManga = mangaRepository.GetOneMangaAllIncluded(id.MangaID);
            Blog = blogRepsitory.GetModel(CurrentManga.BlogModelId);
            Posts = postRepsitory.GetAllModels();
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

        public IActionResult OnPostMangaPage(int MangaID)
        {
            CurrentManga = mangaRepository.GetOneMangaAllIncluded(MangaID);
            if (CurrentManga == null)
            {
                return NotFound();
            }

            Blog = blogRepsitory.GetModel(CurrentManga.BlogModelId);
            Posts = postRepsitory.GetAllModels();
            return Page();
        }

        public IActionResult OnPostMangaPageTagGenre(int MangaID)
        {
            CurrentManga = mangaRepository.GetOneMangaAllIncluded(MangaID);
            if (CurrentManga == null)
            {
                return NotFound();
            }

            Blog = blogRepsitory.GetModel(CurrentManga.BlogModelId);
            Posts = postRepsitory.GetAllModels();
            return Page();
        }

        public IActionResult OnPost()
        {
            return Page();
        }

        public IActionResult OnGet(int id)
        {
            if (id == 0)
            {
                NotFound();
            }
            CurrentManga = mangaRepository.GetOneMangaAllIncluded(id);
            Blog = blogRepsitory.GetModel(CurrentManga.BlogModelId);

            ReivewModel = reviewRepsitory.GetAllModels()
        .Where(r => r.MangaModels != null && r.MangaModels.Any(m => m.MangaID == id));
            Posts = postRepsitory.GetAllModels();
            return Page();
        }
    }
}