using MangaAccessService;
using MangaAccessService.DTO;
using MangaModelService;
using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.RegularExpressions;

//Things to do:

// incorrect, we need to check if the manga even needs an update, if it does not why should we update it.
//fix a bool that checks if it's a need of update.

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

        [BindProperty]
        public int ReadingUsersCount { get; set; }

        [BindProperty]
        public bool IsInReadingList { get; set; }

        [BindProperty]
        public bool IsInCompletedList { get; set; }

        [BindProperty]
        public bool IsInDroppedList { get; set; }

        [BindProperty]
        public bool IsInFavoriteList { get; set; }

        [BindProperty]
        public bool IsInWishList { get; set; }

        [BindProperty]
        public CurrentMangaRelatedRecommendedSeriesDTO relatedRecommendedManga { get; set; }

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

        private async Task AddMangaToUserAsync(string userId, int mangaId)
        {
            var user = await Context.UserModels
                                    .Include(u => u.MangaModels)
                                    .FirstOrDefaultAsync(u => u.Id == userId);
            var manga = await Context.mangaModels.FindAsync(mangaId);

            if (user != null && manga != null && !user.MangaModels.Contains(manga))
            {
                user.MangaModels.Add(manga);
                await Context.SaveChangesAsync();
            }
        }

        private async Task RemoveMangaFromUserAsync(string userId, int mangaId)
        {
            var user = await Context.UserModels
                                    .Include(u => u.MangaModels)
                                    .FirstOrDefaultAsync(u => u.Id == userId);
            var manga = user?.MangaModels.FirstOrDefault(m => m.MangaID == mangaId);

            if (manga != null && !await IsMangaInAnyUserListAsync(userId, mangaId))
            {
                user.MangaModels.Remove(manga);
                await Context.SaveChangesAsync();
            }
        }

        private async Task<bool> IsMangaInAnyUserListAsync(string userId, int mangaId)
        {
            var user = await Context.UserModels
                .Include(u => u.ReadingList).ThenInclude(rl => rl.ReadingMangaList)
                .Include(u => u.FavoritList).ThenInclude(fl => fl.FavoritBooks)
                .Include(u => u.WishList).ThenInclude(wl => wl.WishBooks)
                .Include(u => u.CompletedList).ThenInclude(cl => cl.CompleteBookList)

                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            bool isInReadingList = user.ReadingList.Any(rl => rl.ReadingMangaList.Any(m => m.MangaID == mangaId));
            bool isInFavoritList = user.FavoritList.Any(fl => fl.FavoritBooks.Any(m => m.MangaID == mangaId));
            bool isInWishList = user.WishList.Any(wl => wl.WishBooks.Any(m => m.MangaID == mangaId));
            bool isInCompletedList = user.CompletedList.Any(cl => cl.CompleteBookList.Any(m => m.MangaID == mangaId));

            return isInReadingList || isInFavoritList || isInWishList || isInCompletedList;
        }

        #region Add/Remove WishList

        private async Task<bool> CheckIfMangaInWishListAsync(string userId, int mangaId)
        {
            var userWishList = await Context.wishBookLists
                                            .Include(wbl => wbl.WishBooks)
                                            .FirstOrDefaultAsync(wbl => wbl.UserId == userId);

            return userWishList?.WishBooks.Any(m => m.MangaID == mangaId) ?? false;
        }

        private async Task ManageWishListAndStatusAsync(string userId, int mangaId)
        {
            var userWishList = await Context.wishBookLists
                                            .Include(wbl => wbl.WishBooks)
                                            .FirstOrDefaultAsync(wbl => wbl.UserId == userId);
            var isMangaInAnyList = await IsMangaInAnyUserListAsync(userId, mangaId);

            if (userWishList != null)
            {
                var mangaInList = userWishList.WishBooks.FirstOrDefault(m => m.MangaID == mangaId);

                if (mangaInList == null)
                {
                    var mangaToAdd = await Context.mangaModels.FindAsync(mangaId);
                    if (mangaToAdd != null)
                    {
                        userWishList.WishBooks.Add(mangaToAdd);
                        if (!isMangaInAnyList)
                        {
                            await AddMangaToUserAsync(userId, mangaId); // Add the manga only if it's not in any list
                        }
                    }
                }
                else
                {
                    userWishList.WishBooks.Remove(mangaInList);
                    if (!await IsMangaInAnyUserListAsync(userId, mangaId)) // Check if the manga is in any other list
                    {
                        await RemoveMangaFromUserAsync(userId, mangaId); // Remove the manga from the user's general manga collection
                    }
                }

                await Context.SaveChangesAsync();
            }
        }

        public async Task<IActionResult> OnPostToggleWishListAsync(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage(new { id });
            }

            await ManageWishListAndStatusAsync(user.Id, id);

            // Update the IsInWishList property
            IsInWishList = await CheckIfMangaInWishListAsync(user.Id, id);

            return RedirectToPage(new { id });
        }

        #endregion Add/Remove WishList

        #region Add/Remove FavoritBookList

        private async Task<bool> CheckIfMangaInFavoriteListAsync(string userId, int mangaId)
        {
            var userFavoriteList = await Context.favoritBookLists
                                                .Include(fbl => fbl.FavoritBooks)
                                                .FirstOrDefaultAsync(fbl => fbl.UserId == userId);

            return userFavoriteList?.FavoritBooks.Any(m => m.MangaID == mangaId) ?? false;
        }

        private async Task ManageFavoriteListAndStatusAsync(string userId, int mangaId)
        {
            var userFavoriteList = await Context.favoritBookLists
                                                .Include(fbl => fbl.FavoritBooks)
                                                .FirstOrDefaultAsync(fbl => fbl.UserId == userId);
            var isMangaInAnyList = await IsMangaInAnyUserListAsync(userId, mangaId);

            if (userFavoriteList != null)
            {
                var mangaInList = userFavoriteList.FavoritBooks.FirstOrDefault(m => m.MangaID == mangaId);

                if (mangaInList == null)
                {
                    var mangaToAdd = await Context.mangaModels.FindAsync(mangaId);
                    if (mangaToAdd != null)
                    {
                        userFavoriteList.FavoritBooks.Add(mangaToAdd);
                        if (!isMangaInAnyList)
                        {
                            await AddMangaToUserAsync(userId, mangaId); // Add the manga to the user's general manga collection only if it's not in any list
                        }
                    }
                }
                else
                {
                    userFavoriteList.FavoritBooks.Remove(mangaInList);
                    if (!await IsMangaInAnyUserListAsync(userId, mangaId)) // Check if the manga is in any other list
                    {
                        await RemoveMangaFromUserAsync(userId, mangaId); // Remove the manga from the user's general manga collection
                    }
                }

                await Context.SaveChangesAsync();
            }
        }

        public async Task<IActionResult> OnPostToggleFavoriteListAsync(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage(new { id });
            }

            await ManageFavoriteListAndStatusAsync(user.Id, id);

            // Update the IsInFavoriteList property
            IsInFavoriteList = await CheckIfMangaInFavoriteListAsync(user.Id, id);

            return RedirectToPage(new { id });
        }

        #endregion Add/Remove FavoritBookList

        #region Add/Remove DroppedBookList

        private async Task<bool> CheckIfMangaInDroppedListAsync(string userId, int mangaId)
        {
            var userDroppedList = await Context.droppedBookLists
                                               .Include(dbl => dbl._DroppedBooks)
                                               .FirstOrDefaultAsync(dbl => dbl.UserId == userId);

            return userDroppedList?._DroppedBooks.Any(m => m.MangaID == mangaId) ?? false;
        }

        private async Task ManageDroppedListAndStatusAsync(string userId, int mangaId)
        {
            var userDroppedList = await Context.droppedBookLists
                                               .Include(dbl => dbl._DroppedBooks)
                                               .FirstOrDefaultAsync(dbl => dbl.UserId == userId);
            var isMangaInAnyList = await IsMangaInAnyUserListAsync(userId, mangaId);

            if (userDroppedList != null)
            {
                var mangaInList = userDroppedList._DroppedBooks.FirstOrDefault(m => m.MangaID == mangaId);

                if (mangaInList == null)
                {
                    var mangaToAdd = await Context.mangaModels.FindAsync(mangaId);
                    if (mangaToAdd != null)
                    {
                        userDroppedList._DroppedBooks.Add(mangaToAdd);
                        if (!isMangaInAnyList)
                        {
                            await AddMangaToUserAsync(userId, mangaId); // Add the manga only if it's not in any list
                        }
                    }
                }
                else
                {
                    userDroppedList._DroppedBooks.Remove(mangaInList);
                    if (!await IsMangaInAnyUserListAsync(userId, mangaId)) // Check if the manga is in any other list
                    {
                        await RemoveMangaFromUserAsync(userId, mangaId); // Remove the manga if it's not in any list
                    }
                }

                await Context.SaveChangesAsync();
            }
        }

        public async Task<IActionResult> OnPostToggleDroppedListAsync(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage(new { id });
            }

            await ManageDroppedListAndStatusAsync(user.Id, id);

            // Update the IsInDroppedList property
            IsInDroppedList = await CheckIfMangaInDroppedListAsync(user.Id, id);

            return RedirectToPage(new { id });
        }

        #endregion Add/Remove DroppedBookList

        #region Add/Remove CompleteList

        private async Task<bool> CheckIfMangaInCompletedListAsync(string userId, int mangaId)
        {
            var userCompletedList = await Context.completedBookLists
                                                  .Include(cl => cl.CompleteBookList)
                                                  .FirstOrDefaultAsync(cl => cl.UserId == userId);

            return userCompletedList?.CompleteBookList.Any(m => m.MangaID == mangaId) ?? false;
        }

        private async Task ManageCompletedListAndStatusAsync(string userId, int mangaId)
        {
            var userCompletedList = await Context.completedBookLists
                                                 .Include(cl => cl.CompleteBookList)
                                                 .FirstOrDefaultAsync(cl => cl.UserId == userId);
            var isMangaInAnyList = await IsMangaInAnyUserListAsync(userId, mangaId);

            if (userCompletedList != null)
            {
                var mangaInList = userCompletedList.CompleteBookList.FirstOrDefault(m => m.MangaID == mangaId);

                if (mangaInList == null)
                {
                    var mangaToAdd = await Context.mangaModels.FindAsync(mangaId);
                    if (mangaToAdd != null)
                    {
                        userCompletedList.CompleteBookList.Add(mangaToAdd);
                        if (!isMangaInAnyList)
                        {
                            await AddMangaToUserAsync(userId, mangaId); // Add the manga only if it's not in any list
                        }
                    }
                }
                else
                {
                    userCompletedList.CompleteBookList.Remove(mangaInList);
                    if (!await IsMangaInAnyUserListAsync(userId, mangaId)) // Check if the manga is in any other list
                    {
                        await RemoveMangaFromUserAsync(userId, mangaId); // Remove the manga if it's not in any list
                    }
                }

                await Context.SaveChangesAsync();
            }
        }

        public async Task<IActionResult> OnPostToggleCompletedListAsync(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage(new { id });
            }

            await ManageCompletedListAndStatusAsync(user.Id, id);

            // Update the IsInCompletedList property
            IsInCompletedList = await CheckIfMangaInCompletedListAsync(user.Id, id);

            return RedirectToPage(new { id });
        }

        #endregion Add/Remove CompleteList

        #region Add/Remove ReadingList

        private async Task ManageReadingListAndStatusAsync(string userId, int mangaId, string readingListName)
        {
            var userReadingList = await Context.readingLists
                                               .Include(rl => rl.ReadingMangaList)
                                               .FirstOrDefaultAsync(rl => rl.UserId == userId && rl.ReadingListName == readingListName);
            var isMangaInAnyList = await IsMangaInAnyUserListAsync(userId, mangaId);

            if (userReadingList != null)
            {
                var mangaInList = userReadingList.ReadingMangaList.FirstOrDefault(m => m.MangaID == mangaId);

                if (mangaInList == null)
                {
                    var mangaToAdd = await Context.mangaModels.FindAsync(mangaId);
                    if (mangaToAdd != null)
                    {
                        userReadingList.ReadingMangaList.Add(mangaToAdd);
                        if (!isMangaInAnyList)
                        {
                            await AddMangaToUserAsync(userId, mangaId); // Add the manga only if it's not in any list
                        }
                    }
                }
                else
                {
                    userReadingList.ReadingMangaList.Remove(mangaInList);
                    if (!await IsMangaInAnyUserListAsync(userId, mangaId)) // Check if the manga is in any other list
                    {
                        await RemoveMangaFromUserAsync(userId, mangaId); // Remove the manga if it's not in any list
                    }
                }

                await Context.SaveChangesAsync();
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

        private async Task<bool> CheckIfMangaInReadingListAsync(string userId, int mangaId)
        {
            var userReadingList = await Context.readingLists
                                                .Include(rl => rl.ReadingMangaList)
                                                .FirstOrDefaultAsync(rl => rl.UserId == userId && rl.ReadingListName == "Reading List");

            return userReadingList?.ReadingMangaList.Any(m => m.MangaID == mangaId) ?? false;
        }

        #endregion Add/Remove ReadingList

        private async Task LoadAddRemove(string userId, int mangaId)
        {
            IsInReadingList = await CheckIfMangaInReadingListAsync(userId, mangaId);
            IsInCompletedList = await CheckIfMangaInCompletedListAsync(userId, mangaId);
            IsInDroppedList = await CheckIfMangaInDroppedListAsync(userId, mangaId);
            IsInFavoriteList = await CheckIfMangaInFavoriteListAsync(userId, mangaId);
            IsInWishList = await CheckIfMangaInWishListAsync(userId, mangaId);
        }

        public async Task<CurrentMangaRelatedRecommendedSeriesDTO> LoadRelatedRecommended(int id)
        {
            var relatedmanga = await Context.mangaModels
  .Where(e => e.MangaID == id)
  .Select(e => new CurrentMangaRelatedRecommendedSeriesDTO
  {
      RelatedSeries = e.relatedSeries.Select(rs => new CurrentMangaRelatedSeriesDTO
      {
          MangaID = rs.MangaID,
          MangaName = rs.MangaName,
          PhotoPath = rs.PhotoPath
      }).Take(5).ToList(),
      RecommendedMangaModels = e.RecommendedMangaModels.Select(rm => new MangaModelDTO
      {
          MangaID = rm.MangaID,
          MangaName = rm.MangaName,
          PhotoPath = rm.PhotoPath
      }).Take(5).ToList()
  }).FirstOrDefaultAsync();
            return relatedmanga;
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
            relatedRecommendedManga = await LoadRelatedRecommended(id);
            CurrentManga = await mangaRepository.GetOneEssentialMangaIncludedAsync(id);
            CurrentManga2 = await mangaRepository.GetOneEssentialMangaDtoIncludedAsync(id);

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

        private async Task<List<UserListDto>> GetUsersInListAsync(int mangaId, DbSet<MangaModel> mangaDbSet)
        {
            return await mangaDbSet
          .AsNoTracking()
          .Where(m => m.MangaID == mangaId)
          .SelectMany(m => m.userModels) // Use the correct navigation property name
          .Distinct() // Ensure distinct users are selected
          .Select(u => new UserListDto { UserId = u.Id })
          .ToListAsync();
        }

        private async Task<List<UserListDto>> GetFavoriteUsersInListAsync(int mangaId)
        {
            return await Context.favoritBookLists
                .AsNoTracking()
                .Where(fl => fl.MangaModelId == mangaId)
                .SelectMany(fl => fl.UserModels)
                .Distinct()
                .Select(u => new UserListDto { UserId = u.Id })
                .ToListAsync();
        }

        private async Task<List<UserListDto>> GetCompletedUsersInListAsync(int mangaId)
        {
            return await Context.completedBookLists
                .AsNoTracking()
                .Where(fl => fl.MangaModelId == mangaId)
                .SelectMany(fl => fl.UserModels)
                .Distinct()
                .Select(u => new UserListDto { UserId = u.Id })
                .ToListAsync();
        }

        private async Task<List<UserListDto>> GetWishUsersInListAsync(int mangaId)
        {
            return await Context.wishBookLists
                .AsNoTracking()
                .Where(fl => fl.MangaModelId == mangaId)
                .SelectMany(fl => fl.UserModels)
                .Distinct()
                .Select(u => new UserListDto { UserId = u.Id })
                .ToListAsync();
        }

        private async Task RecalculateReadingUsersCount(int mangaId)
        {
            // Fetch user lists associated with the manga
            var readingListUserIds = await GetUsersInListAsync(mangaId, Context.mangaModels); // Adjusted to use the method
            var favoriteListUserIds = await GetFavoriteUsersInListAsync(mangaId); // Specific method for favorite lists
            var completedListUserIds = await GetCompletedUsersInListAsync(mangaId); // Specific method for completed lists
            var wishListUserIds = await GetWishUsersInListAsync(mangaId);

            // Calculate unique user count
            ReadingUsersCount = readingListUserIds
                .Union(favoriteListUserIds)
                .Union(completedListUserIds)
                .Union(wishListUserIds)
                .GroupBy(u => u.UserId)
                .Select(group => group.First())
                .Count();
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            relatedRecommendedManga = await LoadRelatedRecommended(id);
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
                await LoadAddRemove(user.Id, id);
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

            await RecalculateReadingUsersCount(id);

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