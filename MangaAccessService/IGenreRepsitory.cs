using MangaModelService;

namespace MangaAccessService
{
    public interface IGenreRepsitory
    {
        IEnumerable<GenresModel> Search(string searchTerm);

        IEnumerable<GenresModel> GetAllModels();

        GenresModel GetModel(int id);

        GenresModel Update(GenresModel UpdateModel);

        GenresModel Add(GenresModel addNewModel);

        GenresModel Delete(int id);
    }
}