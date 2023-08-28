using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NovelXManga.Pages.SearchFilter
{
    public class AdvancedSearchModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;
        private readonly IAuthorRepsitory authorRepsitory;
        private readonly IArtistRepsitory artistRepsitory;
        private readonly ITagRepsitory tagRepsitory;
        private readonly IGenreRepsitory genreRepsitory;
        private readonly IChapterModelRepsitory chapterRepsitory;
        private readonly IAssociatedNamesRepsitory associatedNamesRepsitory;
        private readonly ILanguageRepsitory languageRepsitory;
        private readonly IStudioRepsitory studioRepsitory;

        [BindProperty]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SearchReleaseYearStart { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SearchReleaseYearEnd { get; set; }

        [BindProperty]
        public string Search { get; set; }

        [BindProperty]
        public string SearchAuthor { get; set; }

        [BindProperty]
        public string SearchArtist { get; set; }

        [BindProperty]
        public string SearchVoiceActor { get; set; }

        [BindProperty]
        public string SearchCharacter { get; set; }

        [BindProperty]
        public MangaModel _MangaModel { get; set; }

        [BindProperty]
        [FromForm(Name = "selectedTags")]
        public List<int> SelectedTags { get; set; }

        [BindProperty]
        [FromForm(Name = "selectedGenres")]
        public List<int> SelectedGenres { get; set; }

        public List<TagModel> Tags { get; set; }
        public List<GenresModel> Genres { get; set; }
        public IEnumerable<MangaModel> MangaModels { get; set; }
        public string SearchTerm { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public AdvancedSearchModel(MangaNNovelAuthDBContext context, IMangaRepository mangaRepository, IAuthorRepsitory authorRepsitory, IArtistRepsitory artistRepsitory, ITagRepsitory tagRepsitory, IGenreRepsitory genreRepsitory, IChapterModelRepsitory chapterRepsitory, IAssociatedNamesRepsitory associatedNamesRepsitory, ILanguageRepsitory languageRepsitory)
        {
            this.context = context;
            this.mangaRepository = mangaRepository;
            this.authorRepsitory = authorRepsitory;
            this.artistRepsitory = artistRepsitory;
            this.tagRepsitory = tagRepsitory;
            this.genreRepsitory = genreRepsitory;
            this.chapterRepsitory = chapterRepsitory;
            this.associatedNamesRepsitory = associatedNamesRepsitory;
            this.languageRepsitory = languageRepsitory;
            this.MangaModels = new List<MangaModel>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int pageIndex = 1; // You can get this value from the query string or as a parameter
            int pageSize = 8; // Number of books per page
            // Get the selected tag/genres ids

            var selectedGenreIds = SelectedGenres ?? new List<int>();
            var selectedTagIds = SelectedTags ?? new List<int>();
            var query = context.mangaModels
       .Include(m => m.Authormodels)
       .Include(m => m.ArtistModels)
       .Include(m => m.GenresModels)
       .Include(m => m.TagsModels)
       .Include(m => m.Characters)
       .AsNoTracking()
       .AsQueryable().Skip((pageIndex - 1) * pageSize)
    .Take(pageSize);
            CurrentPage = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(await query.CountAsync() / (double)pageSize);
            if (!string.IsNullOrEmpty(Search))
            {
                query = query.Where(m => m.MangaName.Contains(Search));
            }

            if (!string.IsNullOrEmpty(SearchAuthor))
            {
                query = query.Where(m => m.Authormodels.Any(a => a.FirstName.Contains(SearchAuthor) || a.LastName.Contains(SearchAuthor)));
            }
            if (!string.IsNullOrEmpty(SearchArtist))
            {
                query = query.Where(m => m.ArtistModels.Any(a => a.FirstName.Contains(SearchArtist) || a.LastName.Contains(SearchArtist)));
            }
            if (!string.IsNullOrEmpty(SearchVoiceActor))
            {
                query = query.Where(m => m.VoiceActors.Any(a => a.FirstName.Contains(SearchVoiceActor) || a.LastName.Contains(SearchVoiceActor)));
            }

            if (!string.IsNullOrEmpty(SearchCharacter))
            {
                query = query.Where(m => m.Characters.Any(a => a.CharacterName.Contains(SearchCharacter)));
            }
            // Filter by release year
            if (SearchReleaseYearStart.HasValue)
            {
                query = query.Where(m => m.ReleaseYear >= SearchReleaseYearStart.Value);
            }
            if (SearchReleaseYearEnd.HasValue)
            {
                query = query.Where(m => m.ReleaseYear <= SearchReleaseYearEnd.Value);
            }

            // Filter by selected genres
            if (selectedGenreIds.Count > 0)
            {
                // Get all manga that include any of the specified genres
                query = query.Where(m => selectedGenreIds.Any(id => m.GenresModels.Any(g => g.GenresId == id)));
            }

            if (selectedTagIds.Count > 0)
            {
                // Then, filter the list further by checking if any of the manga also includes any of the specified tags
                query = query.Where(m => selectedTagIds.Any(id => m.TagsModels.Any(t => t.TagId == id)));
            }

            // Execute the query
            MangaModels = await query.ToListAsync();

            // Populate tags and genres for the dropdowns
            Tags = await context.TagModels.ToListAsync();
            Genres = await context.GenresModels.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Tags = await context.TagModels.ToListAsync();
            Genres = await context.GenresModels.ToListAsync();
            //MangaModels = await mangaRepository.GetAllModelAsync();

            return Page();
        }
    }
}