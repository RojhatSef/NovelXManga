using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Author
{
    public class CreateAuthorModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IAuthorRepsitory authorRepsitory;
        private readonly IAssociatedNamesRepsitory associatedNamesRepsitory;
        private readonly IOfficalWebsiteRepsitory officalWebsiteRepsitory;

        [BindProperty]
        public MangaModel mangaModel { get; set; }

        [BindProperty]
        public List<AuthorModel> Authors_ { get; set; }

        [BindProperty]
        public List<OfficalWebsite> OfficialWebsites { get; set; }

        [BindProperty]
        public List<AssociatedNames> AssociatedNamesList { get; set; }

        public IFormFile? Photo_Uppload { get; set; }

        public int MangaID { get; set; }

        public CreateAuthorModel(MangaNNovelAuthDBContext context, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment, IAssociatedNamesRepsitory associatedNamesRepsitory, IOfficalWebsiteRepsitory officalWebsiteRepsitory)
        {
            this.context = context;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.associatedNamesRepsitory = associatedNamesRepsitory;
            this.officalWebsiteRepsitory = officalWebsiteRepsitory;
        }

        //public async Task<IActionResult> OnPostAuthorAsync(List<AuthorModel> model)
        public async Task<IActionResult> OnPostAsync([FromForm] List<AuthorModel> Author, int MangaID, [FromForm] IList<IFormFile> Photo)
        {
            if (MangaID != 0)
            {
                var manga = await mangaRepository.GetOneMangaAllIncludedAsync(MangaID);
                for (int i = 0; i < Author.Count; i++)
                {
                    var author = Author[i];
                    Photo_Uppload = Photo.FirstOrDefault(p =>
                    {
                        var parts = p.Name.Split("_");
                        return parts.Length > 1 && int.TryParse(parts[1], out int index) && index == i;
                    });

                    var uploadsDirectory = Path.Combine(webHostEnvironment.WebRootPath, "Images", "AuthorImage");
                    if (!Directory.Exists(uploadsDirectory))
                    {
                        Directory.CreateDirectory(uploadsDirectory);
                    }

                    // Process uploaded file and get the photo path, if there's no photo photopath is null.
                    string photoPath = Photo_Uppload != null ? ProcessUploadedFile() : null;
                    // Create a new AuthorModel object
                    var newAuthor = new AuthorModel
                    {
                        FirstName = author.FirstName,
                        LastName = author.LastName,
                        AssociatedNames = new List<AssociatedNames>(),
                        OfficalWebsites = new List<OfficalWebsite>(),
                        PhotoPath = photoPath,
                        AuthorDeath = author.AuthorDeath,
                        AuthorBorn = author.AuthorBorn,
                        WorkingAt = author.WorkingAt,
                        Gender = author.Gender,
                        Biography = author.Biography,
                        Contact = author.Contact,
                        MangaModels = new List<MangaModel> { manga },
                    };

                    // Add the new author to the database
                    await context.authorModels.AddAsync(newAuthor);
                    await context.SaveChangesAsync();

                    // Add the associated names and official websites to the database
                    if (author.AssociatedNames != null)
                    {
                        foreach (var associatedName in author.AssociatedNames)
                        {
                            if (!string.IsNullOrEmpty(associatedName.nameString))
                            {
                                var newAssociatedName = new AssociatedNames
                                {
                                    nameString = associatedName.nameString,
                                    AuthorID = newAuthor.AuthorID,
                                    AuthorModel = newAuthor
                                };
                                await associatedNamesRepsitory.AddAsync(newAssociatedName);
                            }
                        }
                    }

                    if (author.OfficalWebsites != null)
                    {
                        foreach (var website in author.OfficalWebsites)
                        {
                            if (!string.IsNullOrEmpty(website.OfficalWebsiteName))
                            {
                                var newOfficalWebsite = new OfficalWebsite
                                {
                                    OfficalWebsiteName = website.OfficalWebsiteName,
                                    OfficalWebsiteString = website.OfficalWebsiteString,
                                    AuthorID = newAuthor.AuthorID,
                                    AuthorModel = newAuthor
                                };
                                await officalWebsiteRepsitory.AddAsync(newOfficalWebsite);
                            }
                        }
                    }
                    manga.Authormodels.Add(author);
                    // Add the new author to the manga's authors

                    Authors_.Add(author);

                    await context.SaveChangesAsync();
                }
                //manga.Authormodels = new List<AuthorModel> { Authors_ };
            }
            else
            {
                for (int i = 0; i < Author.Count; i++)
                {
                    var author = Author[i];

                    Photo_Uppload = Photo[i];
                    foreach (var p in Photo)
                    {
                        System.Diagnostics.Debug.WriteLine($"File Name: {p.Name}");
                    }
                    var uploadsDirectory = Path.Combine(webHostEnvironment.WebRootPath, "Images", "AuthorImage");
                    if (!Directory.Exists(uploadsDirectory))
                    {
                        Directory.CreateDirectory(uploadsDirectory);
                    }

                    // Process uploaded file and get the photo path, if there's no photo photopath is null.
                    string photoPath = Photo_Uppload != null ? ProcessUploadedFile() : null;
                    // Create a new AuthorModel object
                    var newAuthor = new AuthorModel
                    {
                        FirstName = author.FirstName,
                        LastName = author.LastName,
                        AssociatedNames = new List<AssociatedNames>(),
                        OfficalWebsites = new List<OfficalWebsite>(),
                        PhotoPath = photoPath,
                        AuthorDeath = author.AuthorDeath,
                        AuthorBorn = author.AuthorBorn,
                        WorkingAt = author.WorkingAt,
                        Gender = author.Gender,
                        Biography = author.Biography,
                        Contact = author.Contact,
                    };

                    // Add the new author to the database
                    await context.authorModels.AddAsync(newAuthor);
                    await context.SaveChangesAsync();

                    // Add the associated names and official websites to the database
                    if (author.AssociatedNames != null)
                    {
                        foreach (var associatedName in author.AssociatedNames)
                        {
                            if (!string.IsNullOrEmpty(associatedName.nameString))
                            {
                                var newAssociatedName = new AssociatedNames
                                {
                                    nameString = associatedName.nameString,
                                    AuthorID = newAuthor.AuthorID,
                                    AuthorModel = newAuthor
                                };
                                await associatedNamesRepsitory.AddAsync(newAssociatedName);
                                newAuthor.AssociatedNames.Add(newAssociatedName);
                            }
                        }
                    }

                    if (author.OfficalWebsites != null)
                    {
                        foreach (var website in author.OfficalWebsites)
                        {
                            if (!string.IsNullOrEmpty(website.OfficalWebsiteName))
                            {
                                var newOfficalWebsite = new OfficalWebsite
                                {
                                    OfficalWebsiteName = website.OfficalWebsiteName,
                                    OfficalWebsiteString = website.OfficalWebsiteString,
                                    AuthorID = newAuthor.AuthorID,
                                    AuthorModel = newAuthor
                                };
                                await officalWebsiteRepsitory.AddAsync(newOfficalWebsite);
                                newAuthor.OfficalWebsites.Add(newOfficalWebsite);
                            }
                        }
                    }

                    await context.SaveChangesAsync();
                }
            }

            return RedirectToPage("/Index");
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo_Uppload != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images", "AuthorImage");
                string extension = Path.GetExtension(Photo_Uppload.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo_Uppload.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public async Task<IActionResult> OnGetAsync(int mangaId)
        {
            MangaID = mangaId;
            mangaModel = await mangaRepository.GetOneMangaAllIncludedAsync(mangaId);

            return Page();
        }
    }
}