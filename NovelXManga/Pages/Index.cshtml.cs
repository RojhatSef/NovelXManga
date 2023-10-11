﻿using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NovelXManga.Pages.SearchFilter;
using System.Text.Json;
using System.Web;

namespace NovelXManga.Pages
{
    //LAYOUTPAGE needs it's own CSS, Index needs it's own CSS.
    public class IndexModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        [TempData]
        public string SucessFulManga { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment, MangaNNovelAuthDBContext context)
        {
            _logger = logger;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
        }

        // Part of the Search for the _Layout page. This code is for the Search bar
        //New code
        public JsonResult OnGetSearchManga(string searchTerm)
        {
            searchTerm = HttpUtility.HtmlEncode(searchTerm);
            int maxResults = 30;

            // Fetching Results
            var mangaResults = context.mangaModels
                .Where(m => m.MangaName.Contains(searchTerm) || m.ISBN10.Contains(searchTerm) || m.ISBN13.Contains(searchTerm))
                .Select(m => new SearchResult { Id = m.MangaID, Name = m.MangaName, PhotoPath = m.PhotoPath, Type = "Manga" })
                .ToList();

            var authorResults = context.authorModels
                .Where(a => a.FirstName.Contains(searchTerm) || a.LastName.Contains(searchTerm))
                .Select(a => new SearchResult { Id = a.AuthorID, Name = $"{a.FirstName} {a.LastName}", Type = "Author" })
                .ToList();

            var artistResults = context.artistModels
                .Where(a => a.FirstName.Contains(searchTerm) || a.LastName.Contains(searchTerm))
                .Select(a => new SearchResult { Id = a.ArtistId, Name = $"{a.FirstName} {a.LastName}", Type = "Artist" })
                .ToList();

            var voiceActorResults = context.voiceActorModels
                .Where(v => v.FirstName.Contains(searchTerm) || v.LastName.Contains(searchTerm))
                .Select(v => new SearchResult { Id = v.VoiceActorId, Name = $"{v.FirstName} {v.LastName}", Type = "VoiceActor" })
                .ToList();

            // Prioritizing and limiting results
            List<SearchResult> finalResults = new List<SearchResult>();

            if (mangaResults.Count >= maxResults)
            {
                finalResults.AddRange(mangaResults.Take(maxResults));
            }
            else
            {
                finalResults.AddRange(mangaResults);
                int remaining = maxResults - mangaResults.Count;

                // Add authors if there is remaining space
                var authorsToAdd = Math.Min(remaining, authorResults.Count);
                finalResults.AddRange(authorResults.Take(authorsToAdd));
                remaining -= authorsToAdd;

                // Add artists if there is remaining space
                var artistsToAdd = Math.Min(remaining, artistResults.Count);
                finalResults.AddRange(artistResults.Take(artistsToAdd));
                remaining -= artistsToAdd;

                // Add voice actors if there is remaining space
                var voiceActorsToAdd = Math.Min(remaining, voiceActorResults.Count);
                finalResults.AddRange(voiceActorResults.Take(voiceActorsToAdd));
            }

            // Grouping the final results by type
            var groupedResults = finalResults.GroupBy(r => r.Type)
                                             .ToDictionary(g => g.Key, g => g.ToList());

            return new JsonResult(groupedResults, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        //  public JsonResult OnGetSearchManga(string searchTerm)
        //  {
        //      searchTerm = HttpUtility.HtmlEncode(searchTerm);
        //
        //      var mangaResults = context.mangaModels
        //.Where(m => m.MangaName.Contains(searchTerm) || m.ISBN10.Contains(searchTerm) || m.ISBN13.Contains(searchTerm))
        //.Select(m => new SearchResult { Id = m.MangaID, Name = m.MangaName, PhotoPath = m.PhotoPath, Type = "Manga" })
        //.ToList();

        //      var authorResults = context.authorModels
        //          .Where(a => a.FirstName.Contains(searchTerm) || a.LastName.Contains(searchTerm))
        //          .Select(a => new SearchResult { Id = a.AuthorID, Name = $"{a.FirstName} {a.LastName}", Type = "Author" })
        //          .ToList();

        //      var artistResults = context.artistModels
        //          .Where(a => a.FirstName.Contains(searchTerm) || a.LastName.Contains(searchTerm))
        //          .Select(a => new SearchResult { Id = a.ArtistId, Name = $"{a.FirstName} {a.LastName}", Type = "Artist" })
        //          .ToList();

        //      var voiceActorResults = context.voiceActorModels
        //          .Where(v => v.FirstName.Contains(searchTerm) || v.LastName.Contains(searchTerm))
        //          .Select(v => new SearchResult { Id = v.VoiceActorId, Name = $"{v.FirstName} {v.LastName}", Type = "VoiceActor" })
        //          .ToList();

        //      var searchResults = new List<SearchResult>();
        //      searchResults.AddRange(mangaResults);
        //      searchResults.AddRange(authorResults);
        //      searchResults.AddRange(artistResults);
        //      searchResults.AddRange(voiceActorResults);

        //      var groupedResults = searchResults.GroupBy(r => r.Type)
        //                                   .ToDictionary(g => g.Key, g => g.ToList());

        //      return new JsonResult(groupedResults, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        //  }

        public List<MangaModel> AllBooksList { get; set; }
        public IEnumerable<AssociatedNames> associatedNames { get; set; }
        public IEnumerable<MangaModel> GetAllBooks { get; set; }
        public IEnumerable<WallPapers> WallPapers { get; set; }
        public List<WallPapers> WallpaperList { get; set; }

        public void OnGet()
        {
            GetAllBooks = context.mangaModels.Include(e => e.GenresModels).Include(e => e.TagsModels).Include(e => e.ArtistModels).Include(e => e.Authormodels);
            AllBooksList = GetAllBooks.ToList();
            WallPapers = context.WallPapers;
            WallpaperList = WallPapers.ToList();
        }
    }
}