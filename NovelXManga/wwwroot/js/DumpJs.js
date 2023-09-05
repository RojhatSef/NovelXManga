//part of the old code Should be in the site.js IF EVER USED AGAIn
//public JsonResult OnGetSearchManga(string searchTerm)
//{
//    var searchResults = context.mangaModels
//                        .Where(m => m.MangaName.Contains(searchTerm) || m.ISBN10.Contains(searchTerm) || m.ISBN13.Contains(searchTerm))
//                        .Select(m => new { m.MangaID, m.MangaName, m.PhotoPath })
//                        .ToList();
//    return new JsonResult(searchResults);
//}