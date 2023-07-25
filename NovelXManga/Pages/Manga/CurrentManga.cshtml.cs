using MangaAccessService;
using MangaModelService;
using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NovelXManga.Pages.Manga
{
    public class CurrentMangaModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext Context;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<UserModel> userManager;
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

        [BindProperty]
        public Review AddReview { get; set; }

        [BindProperty]
        public ViewReview _ViewReview { get; set; }

        public IEnumerable<Review> ReivewModel { get; set; }

        public IEnumerable<PostModel> Posts { get; set; }
        public int ReadingUsersCount { get; set; }
        public bool IsInReadingList { get; set; }
        public IDictionary<int, int> ScoreDistribution { get; set; }

        public CurrentMangaModel(UserManager<UserModel> userManager, IWebHostEnvironment webHostEnvironment, IMangaRepository mangaRepository, MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IBlogRepsitory blogRepsitory, IPostRepsitory postRepsitory, ICharacterRepsitory characterRepsitory, IReviewRepsitory reviewRepsitory)
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

        public async Task<IActionResult> OnPostReviewManga(MangaModel id)
        {
            var user = await userManager.GetUserAsync(User);
            CurrentManga = mangaRepository.GetOneMangaAllIncluded(id.MangaID);
            Blog = blogRepsitory.GetModel(CurrentManga.BlogModelId);
            Posts = postRepsitory.GetAllModels();
            if (ModelState.IsValid)
            {
                Review review = new Review
                {
                    Title = _ViewReview.Title,
                    Created = DateTime.Now,
                    CharactersScore = _ViewReview.CharactersScore,
                    GrammarScore = _ViewReview.GrammarScore,
                    StoryScore = _ViewReview.StoryScore,
                    StylesScore = _ViewReview.StylesScore,
                    Content = _ViewReview.Content,
                    MangaModels = new List<MangaModel> { CurrentManga },
                    UserModels = new List<UserModel> { user },
                };
                Context.Reviews.Add(review);
                Context.SaveChanges();
            }

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

        public async Task<IActionResult> OnPostAddToReadingListAsync(int id)
        {
            // Get the logged-in user
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                // No user is logged in, return the page without making any changes
                return RedirectToPage(new { id });
            }

            CurrentManga = mangaRepository.GetOneMangaAllIncluded(id);

            // Check if the manga is already in the reading list
            var readingListEntry = await Context.readingLists
                .Where(rl => rl.UserId == user.Id && rl.MangaModelId == id)
                .FirstOrDefaultAsync();

            // If the manga is not in the reading list, add it
            if (readingListEntry == null)
            {
                var newReadingListEntry = new ReadingList
                {
                    UserId = user.Id,
                    MangaModelId = id,
                    IsChecked = true,
                };

                Context.readingLists.Add(newReadingListEntry);
                await Context.SaveChangesAsync();
            }
            else
            {
                // If the manga is in the reading list, remove it
                Context.readingLists.Remove(readingListEntry);
                await Context.SaveChangesAsync();
            }

            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                NotFound();
            }
            CurrentManga = mangaRepository.GetOneMangaAllIncluded(id);
            //Blog = blogRepsitory.GetModel(CurrentManga.BlogModelId);

            ReivewModel = reviewRepsitory.GetAllModels()
        .Where(r => r.MangaModels != null && r.MangaModels.Any(m => m.MangaID == id));
            Posts = postRepsitory.GetAllModels();
            // Get the logged-in user
            var user = await userManager.GetUserAsync(User);

            // If the user is logged in, check if the manga is in their reading list
            if (user != null)
            {
                IsInReadingList = await Context.readingLists
                    .Where(rl => rl.UserId == user.Id && rl.MangaModelId == id)
                    .AnyAsync();
            }
            ReadingUsersCount = await Context.readingLists
        .Where(rl => rl.MangaModelId == id)
        .CountAsync();
            return Page();
        }
    }
}