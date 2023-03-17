using MangaModelService;

namespace MangaAccessService
{
    public interface IArtistRepsitory
    {
        IEnumerable<ArtistModel> Search(string searchTerm);

        IEnumerable<ArtistModel> GetAllModels();

        ArtistModel GetModel(int id);

        ArtistModel Update(ArtistModel UpdateModel);

        ArtistModel Add(ArtistModel addNewModel);

        ArtistModel Delete(int id);

        Task<IEnumerable<ArtistModel>> SearchAsync(string searchTermAsync);

        Task<IEnumerable<ArtistModel>> GetAllModelAsync();

        Task<ArtistModel> GetModelAsync(int idAsync);

        Task<ArtistModel> UpdateAsync(ArtistModel UpdateModelAsync);

        Task<ArtistModel> AddAsync(ArtistModel AddAsync);

        Task<ArtistModel> DeleteAsync(int idAsync);
    }
}