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
    }
}