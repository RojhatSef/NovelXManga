using MangaAccessService;
using MangaAccessService.DTO;
using MangaModelService;
using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

//Things to do:
/*
Fix the styling on the page for larger screens and check for mobile with width only. Height should be okay. for these containers,
but check for extreme small width if possible. , The Photo , title AddToLibrary container, The tags and Star/reading container.
The 3 containers should be in 1 container, so it's horizontally aligned.
Bottom page needs an entire restyling.
DRAG OUT THE styling names to a new CSS we don't want stylings to be from one page to another.

*/

namespace NovelXManga.Pages.Manga
{
    [ValidateAntiForgeryToken]
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
        private readonly IMemoryCache _cache;

        [BindProperty]
        public MangaModel CurrentManga { get; set; }

        [BindProperty]
        public BlogModel Blog { get; set; }

        [BindProperty]
        public PostModel OnePost { get; set; }

        [BindProperty]
        public Review AddReview { get; set; }

        [BindProperty]
        public CurrentMangaDto CurrentManga2 { get; set; }

        [BindProperty]
        public ViewReview _ViewReview { get; set; } = new ViewReview();

        public IEnumerable<Review> ReivewModel { get; set; }
        public IEnumerable<PostModel> Posts { get; set; }
        public int ReadingUsersCount { get; set; }
        [BindProperty]
        public bool IsInReadingList { get; set; }
        public IDictionary<int, int> ScoreDistribution { get; set; }

        public CurrentMangaModel(UserManager<UserModel> userManager, IWebHostEnvironment webHostEnvironment, IMangaRepository mangaRepository, MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IBlogRepsitory blogRepsitory, IPostRepsitory postRepsitory, ICharacterRepsitory characterRepsitory, IReviewRepsitory reviewRepsitory, IMemoryCache cache)
        {
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.mangaRepository = mangaRepository;
            this.Context = mangaNNovelAuthDBContext;
            this.blogRepsitory = blogRepsitory;
            this.postRepsitory = postRepsitory;
            this.characterRepsitory = characterRepsitory;
            this.reviewRepsitory = reviewRepsitory;
            _cache = cache;
        }

        public async Task<IActionResult> OnPostMangaPage(int MangaID)
        {
            CurrentManga = await mangaRepository.GetOneMangaAllIncludedAsync(MangaID);
            if (CurrentManga == null)
            {
                return NotFound();
            }

            Blog = blogRepsitory.GetModel(CurrentManga.BlogModelId);
            Posts = await postRepsitory.GetAllModelAsync();
            return Page();
        }

        private async Task ManageReadingListAndStatusAsync(string userId, int mangaId, string readingListName)
        {
            // Fetch the specific reading list by name and user ID.
            var userReadingList = await Context.readingLists
                                                .Include(rl => rl.ReadingMangaList)
                                                .FirstOrDefaultAsync(rl => rl.UserId == userId && rl.ReadingListName == readingListName);

            if (userReadingList != null)
            {
                // Check if the manga is already in the reading list.
                var mangaInList = userReadingList.ReadingMangaList.FirstOrDefault(m => m.MangaID == mangaId);

                if (mangaInList == null)
                {
                    // Add the manga to the reading list if it's not already there.
                    var mangaToAdd = await Context.mangaModels.FindAsync(mangaId);
                    if (mangaToAdd != null)
                    {
                        userReadingList.ReadingMangaList.Add(mangaToAdd);
                    }
                }
                else
                {
                    // Remove the manga from the reading list if it's already there.
                    userReadingList.ReadingMangaList.Remove(mangaInList);
                }

                // Save the changes to the database.

                await Context.SaveChangesAsync();
                IsInReadingList = await CheckIfMangaInReadingListAsync(userId, mangaId);
            }
        }

        public async Task<IActionResult> OnPostAddToReadingListAsync(int id, string readingListName)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage(new { id });
            }

            await ManageReadingListAndStatusAsync(user.Id, id, readingListName);

            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnPostPostTotalScoreAsync(int id)
        {
            bool validScore = (
    new[] { 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5 }.Contains(_ViewReview.GrammarScore)
    && new[] { 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5 }.Contains(_ViewReview.StoryScore)
    && new[] { 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5 }.Contains(_ViewReview.StylesScore)
    && new[] { 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5 }.Contains(_ViewReview.CharactersScore)
);

            if (!validScore)
            {
                ModelState.AddModelError(string.Empty, "Invalid input or review details. Please try again.");
                await OnGetAsync(id);
                return Page();
            }
            if (!string.IsNullOrEmpty(_ViewReview.Title))
            {
                string sanitizedTitle = WebUtility.HtmlEncode(Regex.Replace(_ViewReview.Title, "<.*?>", string.Empty));
                if (sanitizedTitle != _ViewReview.Title)
                {
                    ModelState.AddModelError(string.Empty, "Harmful content detected in Title.");
                    await OnGetAsync(id);

                    TempData["HarmfulContent"] = true;
                    return Page();
                }
            }

            if (!string.IsNullOrEmpty(_ViewReview.Content))
            {
                string sanitizedContent = WebUtility.HtmlEncode(Regex.Replace(_ViewReview.Content, "<.*?>", string.Empty));
                if (sanitizedContent != _ViewReview.Content)
                {
                    ModelState.AddModelError(string.Empty, "Harmful content detected in Content.");
                    await OnGetAsync(id);

                    TempData["HarmfulContent"] = true;
                    return Page();
                }
            }
            if (string.IsNullOrEmpty(_ViewReview.Title)
           || string.IsNullOrEmpty(_ViewReview.Content)
           || _ViewReview.GrammarScore < 0.5
           || _ViewReview.StoryScore < 0.5
           || _ViewReview.StylesScore < 0.5
           || _ViewReview.CharactersScore < 0.5)
            {
                OnGetAsync(id);
                return Page();
            }
            CurrentManga = await mangaRepository.GetOneMangaAllIncludedAsync(id);
            var user = await userManager.GetUserAsync(User);
            if (user.UserActivityTimer.HasValue && (DateTime.Now - user.UserActivityTimer.Value < TimeSpan.FromSeconds(5)))
            {
                return RedirectToPage(new { id });  // or some error message page.
            }

            // CHECK IF USER REVIEWED RECENTLY , IF USER REVIEWED WAIT 30 Sec.
            var existingReview = CurrentManga?.reviews?.FirstOrDefault(r => r.UserModels.Contains(user));
            if (existingReview != null)
            {
                // User has already reviewed this manga, update the review
                existingReview.Title = _ViewReview.Title;
                existingReview.Content = _ViewReview.Content;
                existingReview.StylesScore = _ViewReview.StylesScore;
                existingReview.StoryScore = _ViewReview.StoryScore;
                existingReview.GrammarScore = _ViewReview.GrammarScore;
                existingReview.CharactersScore = _ViewReview.CharactersScore;
                existingReview.LastUpdated = DateTime.Now;
                // Now update the review in the database
                await reviewRepsitory.UpdateAsync(existingReview);
            }
            else
            {
                // New review
                Review newReview = new Review
                {
                    Title = _ViewReview.Title,
                    Content = _ViewReview.Content,
                    StylesScore = _ViewReview.StylesScore,
                    StoryScore = _ViewReview.StoryScore,
                    GrammarScore = _ViewReview.GrammarScore,
                    CharactersScore = _ViewReview.CharactersScore,
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now
                    // set other properties if needed
                };
                if (CurrentManga != null)
                {
                    if (CurrentManga.reviews == null) CurrentManga.reviews = new List<Review>();
                    CurrentManga.reviews.Add(newReview);
                }

                if (user != null)
                {
                    if (user.Reviews == null) user.Reviews = new List<Review>();
                    user.Reviews.Add(newReview);
                }
            }
            // Save changes
            user.UserActivityTimer = DateTime.Now;
            await Context.SaveChangesAsync();
            return RedirectToPage(new { id });
        }

        public async Task<JsonResult> OnGetLoadMoreCharactersAsync(int mangaId, int skip, int take)
        {
            var characters = await Context.Characters
                .Where(c => c.MangaModels.Any(m => m.MangaID == mangaId))
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return new JsonResult(characters);
        }

        private async Task<bool> CheckIfMangaInReadingListAsync(string userId, int mangaId)
        {
            var userReadingList = await Context.readingLists
                                                .Include(rl => rl.ReadingMangaList)
                                                .FirstOrDefaultAsync(rl => rl.UserId == userId && rl.ReadingListName == "Reading List");

            return userReadingList?.ReadingMangaList.Any(m => m.MangaID == mangaId) ?? false;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var totalStopwatch = new Stopwatch();
            totalStopwatch.Start();
            var stopwatch = new Stopwatch();

            if (id == 0)
            {
                return NotFound();
            }

            CurrentManga = await mangaRepository.GetOneEssentialMangaIncludedAsync(id);
            CurrentManga2 = await mangaRepository.GetOneEssentialMangaDtoIncludedAsync(id);

            // Fetch all reviews and posts
            var allReviews = await reviewRepsitory.GetAllModelAsync();
            ReivewModel = allReviews.Where(r => r.MangaModels != null && r.MangaModels.Any(m => m.MangaID == id));
            Posts = await postRepsitory.GetAllModelAsync();

            // Get the logged-in user
            var user = await userManager.GetUserAsync(User);

            // If the user is logged in, prepare lists for the dropdown
            if (user != null)
            {
                IsInReadingList = await CheckIfMangaInReadingListAsync(user.Id, id);

                var userWithReviews = await userManager.Users
       .Include(u => u.Reviews)
       .SingleOrDefaultAsync(u => u.Id == user.Id);
                if (CurrentManga == null)
                {
                    throw new Exception("CurrentManga is null");
                }
                if (CurrentManga.reviews == null)
                {
                    throw new Exception("reviews in CurrentManga is null");
                }
                foreach (var review in CurrentManga.reviews)
                {
                    if (review.UserModels == null)
                    {
                        throw new Exception("UserModels in a review is null");
                    }
                }
                var userReview = CurrentManga?.reviews?.FirstOrDefault(r => r.UserModels?.Contains(user) ?? false);
                if (userReview != null)
                {
                    _ViewReview.Title = userReview.Title;
                    _ViewReview.Content = userReview.Content;
                    _ViewReview.StylesScore = userReview.StylesScore;
                    _ViewReview.StoryScore = userReview.StoryScore;
                    _ViewReview.GrammarScore = userReview.GrammarScore;
                    _ViewReview.CharactersScore = userReview.CharactersScore;
                }

            }
            ReadingUsersCount = await Context.readingLists
        .Where(rl => rl.MangaModelId == id)
        .CountAsync();
            string cacheKey = $"DailyRead_{id}";
            if (!_cache.TryGetValue<int>(cacheKey, out int dailyRead))
            {
                dailyRead = CurrentManga?.DailyRead ?? 0;
                _cache.Set<int>(cacheKey, dailyRead + 1, TimeSpan.FromDays(1));
            }
            else
            {
                dailyRead++;
                _cache.Set<int>(cacheKey, dailyRead, TimeSpan.FromDays(1));
            }
            CurrentManga.DailyRead = dailyRead;

            await mangaRepository.UpdateAsync(CurrentManga);

            return Page();
        }
    }
}