using MangaAccessService.DTO;
using MangaAccessService.DTO.IndexDto;
using MangaAccessService.DTO.LoginRegiForgetDto;
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

        Task<IEnumerable<MangaModel>> GetAllUserModelAsync(UserModel currentUser);

        Task<IEnumerable<MangaModel>> Get10MangaModelAsync();

        Task<MangaModel> GetModelAsync(int id);

        Task<MangaModel> GetOneMangaAllIncludedAsync(int id);

        Task<MangaModel> UpdateAsync(MangaModel updatedManga);

        Task<MangaModel> GetOneEssentialMangaIncludedAsync(int id);

        Task<CurrentMangaDto> GetOneEssentialMangaDtoIncludedAsync(int id);

        Task<CurrentMangaDto> GetAddtionalEssentialMangaDtoIncludedAsync(int id);

        Task<MangaModel> GetLimitedEssentialMangaIncludedAsync(int id);

        Task<IEnumerable<LoginRegiForgetCombineDto>> Get10MangaEssentialMangaDtoIncludedAsync();

        Task<IEnumerable<IndexDtoManga>> IndexMangaDtoIncludedAsync();

        Task<MangaModel> AddAsync(MangaModel NewManga);

        Task<MangaModel> DeleteAsync(int id);

        Task<IEnumerable<MangaModel>> GetPaginatedMangaModelsAsync(int pageNumber, int pageSize);

        Task<IEnumerable<MangaModel>> GetTopRankedMangaAsync(int count);

        Task<IEnumerable<MangaModel>> GetMostPopularMangaAsync(int count);

        Task UpdateMangaRankingAsync(MangaModel manga, int rank);
    }
}