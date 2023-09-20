using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NovelXManga.Pages.Artist
{
    public class UpdateVoiceModel : PageModel
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly MangaNNovelAuthDBContext _context;
        private readonly IMangaRepository mangaRepository;

        public UpdateVoiceModel(IWebHostEnvironment webHostEnvironment, IMangaRepository mangaRepository, MangaNNovelAuthDBContext context)
        {
            this.webHostEnvironment = webHostEnvironment;

            this.mangaRepository = mangaRepository;
            _context = context;
        }

        [BindProperty(Name = "Photo")]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public MangaModel mangaModelUpdate { get; set; }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images", "AuthorImage");
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

        public async Task<IActionResult> OnPostAsync(MangaModel mangaModel)
        {
            try
            {
                if (mangaModel == null)
                {
                    return NotFound();
                }
                mangaModelUpdate = await mangaRepository.GetOneMangaAllIncludedAsync(mangaModel.MangaID);

                if (mangaModelUpdate.PhotoPath != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "MangaImage", mangaModelUpdate.PhotoPath);
                    if (!filePath.EndsWith("NoPhoto.png"))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                mangaModelUpdate.PhotoPath = ProcessUploadedFile();
                mangaModelUpdate = await mangaRepository.UpdateAsync(mangaModelUpdate);

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                // Log the error or show it to the user.
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            mangaModelUpdate = _context.mangaModels.Include(e => e.VoiceActors).FirstOrDefault(e => e.MangaID == id);

            if (mangaModelUpdate == null)
            {
                return NotFound();
            }
            if (mangaModelUpdate.Authormodels == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}