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

        Task<IEnumerable<MangaModel>> SearchAsync(string searchTerm);

        Task<IEnumerable<MangaModel>> GetAllModelAsync();

        Task<IEnumerable<MangaModel>> Get10MangaModelAsync();

        Task<MangaModel> GetModelAsync(int id);

        Task<MangaModel> GetOneMangaAllIncludedAsync(int id);

        Task<MangaModel> UpdateAsync(MangaModel updatedManga);

        Task<MangaModel> AddAsync(MangaModel NewManga);

        Task<MangaModel> DeleteAsync(int id);

        Task<IEnumerable<MangaModel>> GetTopRankedMangaAsync(int count);

        Task<IEnumerable<MangaModel>> GetMostPopularMangaAsync(int count);

        Task UpdateMangaRankingAsync(MangaModel manga, int rank);
    }
}