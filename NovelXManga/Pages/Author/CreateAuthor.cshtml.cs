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
        public List<AuthorModel> Author { get; set; }

        [BindProperty]
        public List<OfficalWebsite> OfficialWebsites { get; set; }

        [BindProperty]
        public List<AssociatedNames> AssociatedNamesList { get; set; }

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
        public async Task<IActionResult> OnPostAsync(List<AuthorModel> Author, int MangaID)
        {
            if (MangaID != 0)
            {
                var manga = await mangaRepository.GetOneMangaAllIncludedAsync(MangaID);
                foreach (var author in Author)
                {
                    // Create a new AuthorModel object
                    var newAuthor = new AuthorModel
                    {
                        FirstName = author.FirstName,
                        LastName = author.LastName,
                        AssociatedNames = new List<AssociatedNames>(),
                        OfficalWebsites = new List<OfficalWebsite>()
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
                    manga.Authormodels = new List<AuthorModel> { author };

                    await context.SaveChangesAsync();
                }
            }
            else
            {
                foreach (var author in Author)
                {
                    // Create a new AuthorModel object
                    var newAuthor = new AuthorModel
                    {
                        FirstName = author.FirstName,
                        LastName = author.LastName,
                        AssociatedNames = new List<AssociatedNames>(),
                        OfficalWebsites = new List<OfficalWebsite>()
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

        public async Task<IActionResult> OnGetAsync(int mangaId)
        {
            MangaID = mangaId;
            mangaModel = await mangaRepository.GetOneMangaAllIncludedAsync(mangaId);

            return Page();
        }
    }
}