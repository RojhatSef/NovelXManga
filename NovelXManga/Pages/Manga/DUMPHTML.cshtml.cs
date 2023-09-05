using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Manga
{
    public class DUMPHTMLModel : PageModel
    {
        public void OnGet()
        {
        }

        //part of the old code THIS CODE BELONGS TO INDEX IF EVER USED AGAIN
        //public JsonResult OnGetSearchManga(string searchTerm)
        //{
        //    var searchResults = context.mangaModels
        //                        .Where(m => m.MangaName.Contains(searchTerm) || m.ISBN10.Contains(searchTerm) || m.ISBN13.Contains(searchTerm))
        //                        .Select(m => new { m.MangaID, m.MangaName, m.PhotoPath })
        //                        .ToList();
        //    return new JsonResult(searchResults);
        //}
    }
}