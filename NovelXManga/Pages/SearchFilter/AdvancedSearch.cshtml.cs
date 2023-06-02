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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        public DateTime? SearchReleaseYearStart { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        public DateTime? SearchReleaseYearEnd { get; set; }

        [BindProperty]
        public string Search { get; set; }

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
        }

        public async Task<IActionResult> OnPostAsync()
        {
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
       .AsQueryable();

            if (!string.IsNullOrEmpty(Search))
            {
                query = query.Where(m => m.MangaName.Contains(Search) || m.ArtistModels.Any(at => at.FirstName.Contains(Search) || at.LastName.Contains(Search)) ||
                                         m.Authormodels.Any(a => a.FirstName.Contains(Search) || a.LastName.Contains(Search)) || m.Characters.Any(a => a.CharacterName.Contains(Search)) ||
                                         m.VoiceActors.Any(a => a.FirstName.Contains(Search) || a.LastName.Contains(Search)));
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
                query = query.Where(m => m.GenresModels.Any(g => selectedGenreIds.Contains(g.GenresId)));
            }

            // Filter by selected tags
            if (selectedTagIds.Count > 0)
            {
                query = query.Where(m => m.TagsModels.Any(t => selectedTagIds.Contains(t.TagId)));
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
            MangaModels = await mangaRepository.GetAllModelAsync();

            return Page();
        }
    }
}