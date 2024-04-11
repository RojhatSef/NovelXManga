using MangaAccessService;
using MangaAccessService.DTO;
using MangaModelService;
using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace NovelXManga.Pages.Manga
{
    [Authorize(Roles = "Owner, Admin, Updater, Publisher, Artist, Author, Tester, AdminControl, Scanlators, BackEndDeveloper, SupportGroups")]
    public class Create_new_MangaModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ITagRepsitory tagRepsitory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [BindProperty]
        public MangaModel _MangaModel { get; set; }

        [BindProperty]
        public AuthorViewModel authorViewModel { get; set; }

        [BindProperty]
        public IFormFile? Photo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [BindProperty]
        public DateTime ReleaseYear { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [BindProperty]
        public DateTime? EndingYear { get; set; } = null;

        [BindProperty]
        [FromForm(Name = "JsSelectedTags")]
        public List<int> SelectedTags { get; set; }

        [BindProperty]
        [FromForm(Name = "selectedGenres")]
        public List<int> SelectedGenres { get; set; }

        [BindProperty]
        [FromForm(Name = "PositiveSelectedGenres")]
        public List<int> PositiveSelectedGenres { get; set; }

        public List<TagModel> Tags { get; set; }
        public List<GenresModel> Genres { get; set; }
        public IEnumerable<MangaModel> MangaModels { get; set; }
        public IEnumerable<MangaDTO> RelatedMangaOptions { get; set; } = new List<MangaDTO>();
        public IEnumerable<MangaDTO> RecommendedMangaOptions { get; set; } = new List<MangaDTO>();

        [BindProperty]
        public int[] SelectedRelatedMangaIds { get; set; }

        [BindProperty]
        public int[] SelectedRecommendedMangaIds { get; set; }

        [TempData]
        public string SucessFulManga { get; set; }

        public Create_new_MangaModel(MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment, ITagRepsitory tagRepsitory, ILogger<IndexModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.context = mangaNNovelAuthDBContext;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.tagRepsitory = tagRepsitory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<MangaDTO>> GetAllMangaMinimalAsync()
        {
            return await context.mangaModels
                .Select(m => new MangaDTO { MangaID = m.MangaID, MangaName = m.MangaName })
                .ToListAsync();
        }

        private void DeserializeAndRetrieveSessionData()
        {
            var selectedTagsSerialized = _httpContextAccessor.HttpContext.Session.GetString("SelectedTags");
            SelectedTags = string.IsNullOrEmpty(selectedTagsSerialized) ? new List<int>() : JsonSerializer.Deserialize<List<int>>(selectedTagsSerialized);

            var selectedGenresSerialized = _httpContextAccessor.HttpContext.Session.GetString("PositiveSelectedGenres");
            PositiveSelectedGenres = string.IsNullOrEmpty(selectedGenresSerialized) ? new List<int>() : JsonSerializer.Deserialize<List<int>>(selectedGenresSerialized);
            var selectedRelatedMangaIdsSerialized = _httpContextAccessor.HttpContext.Session.GetString("SelectedRelatedMangaIds");
            SelectedRelatedMangaIds = string.IsNullOrEmpty(selectedRelatedMangaIdsSerialized) ? new int[0] : JsonSerializer.Deserialize<int[]>(selectedRelatedMangaIdsSerialized);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Check if the uploads directory exists, and create it if it does not

            var uploadsDirectory = Path.Combine(webHostEnvironment.WebRootPath, "Images", "MangaImage");
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }
            InitializeSettings();
            SerializeAndStoreSessionData();

            // Process uploaded file and get the photo path, if there's no photo photopath is null.

            _MangaModel.PhotoPath = ProcessUploadedFile(Photo);

            // create a new list of TagModel objects
            if (SelectedRelatedMangaIds != null)
            {
                _MangaModel.relatedSeries = await context.mangaModels
                    .Where(m => SelectedRelatedMangaIds.Contains(m.MangaID))
                    .ToListAsync();
            }
            _MangaModel.TagsModels = await context.TagModels.Where(tag => SelectedTags.Contains(tag.TagId)).ToListAsync();
            _MangaModel.GenresModels = await context.GenresModels.Where(genre => PositiveSelectedGenres.Contains(genre.GenresId)).ToListAsync();

            #region If Something == null

            if (_MangaModel.MangaName == null)
            {
                SucessFulManga = "Book failed to create!";
                return RedirectToPage("/Index");
            }

            #endregion If Something == null

            MangaModel MangaModels = new MangaModel
            {
                MangaName = _MangaModel.MangaName,
                PhotoPath = _MangaModel.PhotoPath,
                BlogModel = new BlogModel { mangaName = _MangaModel.MangaName },
                futureEvents = _MangaModel.futureEvents,

                StatusInCountryOfOrigin = _MangaModel.StatusInCountryOfOrigin,
                ISBN10 = _MangaModel.ISBN10,
                ISBN13 = _MangaModel.ISBN13,
                Description = _MangaModel.Description,
                ReleaseYear = _MangaModel.ReleaseYear,
                EndingYear = EndingYear,
                Type = _MangaModel.Type,
                OfficalLanguage = _MangaModel.OfficalLanguage,
                OriginalPublisher = _MangaModel.orignalWebtoon,
                CompletelyTranslated = _MangaModel.CompletelyTranslated,

                orignalWebtoon = _MangaModel.orignalWebtoon,
                TagsModels = _MangaModel.TagsModels,
                GenresModels = _MangaModel.GenresModels,
                BookAddedToDB = DateTime.UtcNow,
            };

            await context.mangaModels.AddAsync(MangaModels);
            await context.SaveChangesAsync();
            int newMangaId = MangaModels.MangaID;
            SucessFulManga = "Manga has been created successfully";
            return RedirectToPage("/Author/CreateAuthor", new { mangaId = newMangaId });
            //return RedirectToPage("/Author/CreateAuthor", new { mangaId = newMangaId });
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Tags = await context.TagModels.ToListAsync();
            Genres = await context.GenresModels.ToListAsync();
            RelatedMangaOptions = await GetAllMangaMinimalAsync();
            RecommendedMangaOptions = await GetAllMangaMinimalAsync();
            DeserializeAndRetrieveSessionData();
            MangaModels = await mangaRepository.GetAllModelAsync();
            var selectedTagsSession = _httpContextAccessor.HttpContext.Session.GetString("SelectedTags");
            var selectedGenresSession = _httpContextAccessor.HttpContext.Session.GetString("SelectedGenres");
            if (!string.IsNullOrEmpty(selectedTagsSession))
            {
                SelectedTags = JsonSerializer.Deserialize<List<int>>(selectedTagsSession);
            }

            if (!string.IsNullOrEmpty(selectedGenresSession))
            {
                PositiveSelectedGenres = JsonSerializer.Deserialize<List<int>>(selectedGenresSession);
            }
            return Page();
        }

        private void SerializeAndStoreSessionData()
        {
            var selectedTagsSerialized = JsonSerializer.Serialize(SelectedTags);
            var selectedGenresSerialized = JsonSerializer.Serialize(PositiveSelectedGenres);
            var selectedRelatedMangaIdsSerialized = JsonSerializer.Serialize(SelectedRelatedMangaIds);
            _httpContextAccessor.HttpContext.Session.SetString("SelectedRelatedMangaIds", selectedRelatedMangaIdsSerialized);
        }

        private void InitializeSettings()
        {
            PositiveSelectedGenres ??= new List<int>();

            SelectedTags ??= new List<int>();
        }

        private string ProcessUploadedFile(IFormFile uploadedFile)
        {
            // Path for the default image
            string defaultImagePath = "NoPhoto.png";

            if (uploadedFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images", "GeneratedMangaImage");
                Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists

                string extension = Path.GetExtension(uploadedFile.FileName);
                string uniqueFileName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    uploadedFile.CopyTo(fileStream);
                }

                // Return only the file name for uploaded images, assuming all access is prefixed in the frontend
                return uniqueFileName;
            }

            return defaultImagePath;
        }
    }
}