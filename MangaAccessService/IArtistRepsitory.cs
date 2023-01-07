using MangaModelService;


namespace MangaAccessService
{
    public interface IArtistRepsitory
    {
        IEnumerable<ArtistModel> Search(string searchTerm);
        IEnumerable<ArtistModel> GetAllModels();
        ArtistModel GetModel(string id);

        ArtistModel Update(ArtistModel UpdateModel);

        ArtistModel Add(ArtistModel addNewModel);

        ArtistModel Delete(string id);
    }
}
