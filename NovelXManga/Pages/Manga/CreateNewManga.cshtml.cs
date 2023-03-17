using MangaAccessService;
using MangaModelService;
using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace NovelXManga.Pages.Manga
{
    [Authorize]
    public class Create_new_MangaModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ITagRepsitory tagRepsitory;

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
        [FromForm(Name = "selectedTags")]
        public List<int> SelectedTags { get; set; }

        [BindProperty]
        [FromForm(Name = "selectedGenres")]
        public List<int> SelectedGenres { get; set; }

        public List<TagModel> Tags { get; set; }
        public List<GenresModel> Genres { get; set; }
        public IEnumerable<MangaModel> MangaModels { get; set; }

        [TempData]
        public string SucessFulManga { get; set; }

        public Create_new_MangaModel(MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment, ITagRepsitory tagRepsitory, ILogger<IndexModel> logger)
        {
            this.context = mangaNNovelAuthDBContext;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.tagRepsitory = tagRepsitory;
        }

        public void OnPostAuthor(List<AuthorModel> model)
        {
            //do your stuff...
        }

        public IActionResult OnPostAsync()
        {
            // Check if the uploads directory exists, and create it if it does not

            var uploadsDirectory = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }
            string photoPath;
            if (Photo != null)
            {
                photoPath = ProcessUploadedFile();
            }
            else
            {
                photoPath = null;
            }

            Tags = context.TagModels.ToList();

            // create a new list of TagModel objects
            var selectedTags = context.TagModels.Where(tag => SelectedTags.Contains(tag.TagId)).ToList();
            var selectedGenres = context.GenresModels.Where(tag => SelectedGenres.Contains(tag.GenresId)).ToList();

            _MangaModel.TagsModels = selectedTags;
            var newMangaModel = context.mangaModels.FirstOrDefault(mm => mm.MangaName == _MangaModel.MangaName);

            #region If Something == null

            if (_MangaModel.MangaName == null)
            {
                return RedirectToPage("/Index");
            }

            if (_MangaModel.Description == null)
            {
                _MangaModel.Description = null;
            }

            if (_MangaModel.AssociatedNames == null)
            {
                _MangaModel.AssociatedNames = null;
            }

            if (_MangaModel.StatusInCountryOfOrigin == null)
            {
                _MangaModel.StatusInCountryOfOrigin = null;
            }

            if (_MangaModel.ISBN10 == null)
            {
                _MangaModel.ISBN10 = null;
            }

            if (_MangaModel.ISBN13 == null)
            {
                _MangaModel.ISBN13 = null;
            }

            if (_MangaModel.score == null)
            {
                _MangaModel.score = null;
            }
            if (_MangaModel.AssociatedNames == null)
            {
                _MangaModel.AssociatedNames = null;
            }

            if (_MangaModel.BlogModel == null)
            {
                _MangaModel.BlogModel = null;
            }

            if (_MangaModel.CompletelyTranslated == null)
            {
                _MangaModel.CompletelyTranslated = null;
            }

            if (_MangaModel.EndingYear == null)
            {
                _MangaModel.EndingYear = null;
            }

            if (_MangaModel.futureEvents == null)
            {
                _MangaModel.futureEvents = null;
            }

            if (_MangaModel.GroupScanlating == null)
            {
                _MangaModel.GroupScanlating = null;
            }

            if (_MangaModel.ISBN10 == null)
            {
                _MangaModel.ISBN10 = null;
            }

            if (_MangaModel.ISBN13 == null)
            {
                _MangaModel.ISBN13 = null;
            }

            if (_MangaModel.OfficalLanguage == null)
            {
                _MangaModel.OfficalLanguage = null;
            }

            if (_MangaModel.orignalWebtoon == null)
            {
                _MangaModel.orignalWebtoon = null;
            }

            if (_MangaModel.OriginalPublisher == null)
            {
                _MangaModel.OriginalPublisher = null;
            }

            if (_MangaModel.ReleaseYear == null)
            {
                _MangaModel.ReleaseYear = null;
            }

            if (selectedTags == null)
            {
                selectedTags = null;
            }
            if (selectedGenres == null)
            {
                selectedGenres = null;
            }

            if (_MangaModel.StatusInCountryOfOrigin == null)
            {
                _MangaModel.StatusInCountryOfOrigin = null;
            }

            if (_MangaModel.Type == null)
            {
                _MangaModel.Type = null;
            }

            #endregion If Something == null

            if (newMangaModel == null)
            {
                MangaModel MangaModels = new MangaModel
                {
                    MangaName = _MangaModel.MangaName,

                    PhotoPath = photoPath,

                    BlogModel = new BlogModel { mangaName = _MangaModel.MangaName },

                    futureEvents = _MangaModel.futureEvents,

                    score = 10,
                    StatusInCountryOfOrigin = _MangaModel.StatusInCountryOfOrigin,

                    ISBN10 = _MangaModel.ISBN10,
                    ISBN13 = _MangaModel.ISBN13,

                    Description = _MangaModel.Description,
                    ReleaseYear = ReleaseYear,
                    EndingYear = EndingYear,
                    Type = _MangaModel.Type,
                    OfficalLanguage = _MangaModel.OfficalLanguage,
                    OriginalPublisher = _MangaModel.orignalWebtoon,
                    CompletelyTranslated = _MangaModel.CompletelyTranslated,
                    WeekRead = 0,
                    MonthRead = 0,
                    YearRead = 0,

                    orignalWebtoon = _MangaModel.orignalWebtoon,
                    TagsModels = selectedTags,

                    GenresModels = selectedGenres,
                    BookAddedToDB = DateTime.UtcNow,
                };

                context.mangaModels.Add(MangaModels);
                context.SaveChanges();
                SucessFulManga = "Manga has been created successfully";
                return RedirectToPage("/Index");
            }

            return new RedirectToPageResult("/Index");
        }

        public IActionResult OnGet()
        {
            Tags = context.TagModels.ToList();
            MangaModels = mangaRepository.GetAllManga();

            return Page();
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images", "MangaImage");
                string extension = Path.GetExtension(Photo.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}

#region TestOnGet

//public IActionResult OnGet(int? id)
//{
//    if (id.HasValue)
//    {
//        mangaModel = mangaRepository.GetManga(id.Value);
//    }
//    else
//    {
//        var newManga = new MangaModel
//        {
//            MangaName = mangaModel.MangaName,
//            AssociatedNames = mangaModel.AssociatedNames,
//            MangaID = mangaModel.MangaID,
//            MasterID = mangaModel.MasterModelID,

//        };

//    }

//    if (mangaModel == null)
//    {
//        return RedirectToPage("/Index");
//    }
//    return Page();
//}

#endregion TestOnGet