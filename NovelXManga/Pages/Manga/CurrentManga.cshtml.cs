using MangaAccessService;
using MangaModelService;
using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net;
using System.Text.RegularExpressions;

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
        public ViewReview _ViewReview { get; set; } = new ViewReview();

        public IEnumerable<Review> ReivewModel { get; set; }

        public IEnumerable<PostModel> Posts { get; set; }
        public int ReadingUsersCount { get; set; }
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
            CurrentManga = mangaRepository.GetOneMangaAllIncluded(id);
            var user = await userManager.GetUserAsync(User);
            var existingReview = CurrentManga?.reviews?.FirstOrDefault(r => r.UserModels.Contains(user));
            if (existingReview != null)
            {
                // To reduce spam, and overflow of information to database.
                if (DateTime.Now - existingReview.LastUpdated < TimeSpan.FromSeconds(30))
                {
                    // Reject the update, user must wait
                    // You can return an appropriate message or redirect to a different page
                    return RedirectToPage(new { id });
                }
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
            await Context.SaveChangesAsync();
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
                IsInReadingList = await Context.readingLists
                    .Where(rl => rl.UserId == user.Id && rl.MangaModelId == id)
                    .AnyAsync();
            }
            ReadingUsersCount = await Context.readingLists
        .Where(rl => rl.MangaModelId == id)
        .CountAsync();

            string cacheKey = $"DailyRead_{id}";
            if (!_cache.TryGetValue<int>(cacheKey, out int dailyRead))
            {
                dailyRead = CurrentManga.DailyRead ?? 0;
                // Set cache entry to expire after 24 hours
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