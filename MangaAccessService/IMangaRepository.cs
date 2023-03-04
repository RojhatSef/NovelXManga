using MangaModelService;

namespace MangaAccessService
{
    public interface IMangaRepository
    {
        IEnumerable<MangaModel> Search(string searchTerm);

        IEnumerable<MangaModel> GetAllManga();

        MangaModel GetManga(int id);

        MangaModel GetOneMangaAllIncluded(int id);

        MangaModel Update(MangaModel updatedManga);

        MangaModel Add(MangaModel NewManga);

        MangaModel Delete(int id);
    }
}