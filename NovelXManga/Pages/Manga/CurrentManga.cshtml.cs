using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Channels;

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
        private readonly ICharacterRepsitory characterRepsitory;

        [BindProperty]
        public MangaModel CurrentManga { get; set; }

        [BindProperty]
        public BlogModel Blog { get; set; }

        [BindProperty]
        public PostModel OnePost { get; set; }

        public IEnumerable<PostModel> Posts { get; set; }

        public CurrentMangaModel(UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment, IMangaRepository mangaRepository, MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IBlogRepsitory blogRepsitory, IPostRepsitory postRepsitory, ICharacterRepsitory characterRepsitory)
        {
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.mangaRepository = mangaRepository;
            this.Context = mangaNNovelAuthDBContext;
            this.blogRepsitory = blogRepsitory;
            this.postRepsitory = postRepsitory;
            this.characterRepsitory = characterRepsitory;
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

        // OnPost For characters
        public IActionResult OnPostCharacters(int id, List<int> characters)
        {
            var mangaModelUpdate = mangaRepository.GetOneMangaAllIncluded(id);

            if (mangaModelUpdate == null)
            {
                return NotFound();
            }

            // Update the characters
<<<<<<< HEAD
            //mangaModelUpdate.Characters = await mangaRepository.GetCharactersByIdsAsync(characters);
=======
            mangaModelUpdate.Characters = characterRepsitory.GetCharactersByIds(characters).ToList();
>>>>>>> 0f4c54d64f62debee36ff8b12baf2adc41a4a0ad
            Context.SaveChanges();

            return RedirectToPage("/MangaUpdates/UpdateCharacters", new { id = mangaModelUpdate.MangaID });
        }

        public void OnGet(int id)
        {
            if (id == 0)
            {
                NotFound();
            }
            CurrentManga = mangaRepository.GetOneMangaAllIncluded(id);
            Blog = blogRepsitory.GetModel(CurrentManga.BlogModelId);
            Posts = postRepsitory.GetAllModels();
        }
    }
}